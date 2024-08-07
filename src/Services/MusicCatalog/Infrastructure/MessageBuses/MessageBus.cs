using Application.Abstractions.Infrastructure;
using MassTransit;

namespace Infrastructure.MessageBuses;

public class MessageBus(IBus bus) : IMessageBus
{
    public async Task PublishAsync<T>(T message) where T : class
    {
        await bus.Publish(message);
    }
}