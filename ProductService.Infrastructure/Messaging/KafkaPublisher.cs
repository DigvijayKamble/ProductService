using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using ProductService.Application.Interfaces;

namespace ProductService.Infrastructure.Messaging;

public class KafkaPublisher : IEventPublisher
{
    private readonly IProducer<Null, string> _producer;

    public KafkaPublisher(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task PublishAsync(string eventType, string payload, CancellationToken ct)
    {
        await _producer.ProduceAsync(eventType,
            new Message<Null, string> { Value = payload });
    } 
}
