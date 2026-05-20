namespace ProductService.Application.Interfaces; 
public interface IEventPublisher
{
    Task PublishAsync(string eventType, string payload, CancellationToken ct);
}
