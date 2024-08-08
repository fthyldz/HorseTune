using Application.Abstractions.Infrastructure;
using MassTransit;

namespace Infrastructure.MessageBuses;

public class MessageBus(IBus bus) : IMessageBus
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await bus.Publish(message, config =>
        {
            config.SetRoutingKey("artist_image_uploaded");
        }, cancellationToken);
    }
    
    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await bus.Send(message, cancellationToken);
    }
}