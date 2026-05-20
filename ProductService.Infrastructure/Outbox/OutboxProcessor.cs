using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Infrastructure.Outbox; 

public sealed class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OutboxProcessor> _logger;

    private const int BatchSize = 10;
    private static readonly TimeSpan Delay = TimeSpan.FromSeconds(5);

    public OutboxProcessor(
        IServiceScopeFactory scopeFactory,
        ILogger<OutboxProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Outbox Processor started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider
                    .GetRequiredService<ProductDbContext>();

                var publisher = scope.ServiceProvider
                    .GetRequiredService<IEventPublisher>();

                var messages = await dbContext.OutboxMessages
                    .Where(x => !x.IsProcessed)
                    .OrderBy(x => x.OccurredOn)
                    .Take(BatchSize)
                    .ToListAsync(stoppingToken);

                if (!messages.Any())
                {
                    await Task.Delay(Delay, stoppingToken);
                    continue;
                }

                foreach (var message in messages)
                {
                    try
                    {
                        await publisher.PublishAsync(
                            message.EventType,
                            message.Payload,
                            stoppingToken);

                        message.IsProcessed = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                            "Failed to publish Outbox message {MessageId}",
                            message.Id);

                        // Do NOT mark as processed → will retry
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,
                    "Outbox Processor crashed. Retrying...");
            }

            await Task.Delay(Delay, stoppingToken);
        }

        _logger.LogInformation("Outbox Processor stopped");
    }
}

