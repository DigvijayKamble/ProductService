namespace ProductService.Infrastructure.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OccurredOn { get; set; }
    public string EventType { get; set; } = null!;
    public string Payload { get; set; } = null!;
    public bool IsProcessed { get; set; } = false;
} 