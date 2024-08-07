namespace Application.Abstractions.Infrastructure;

public interface IMessageBus
{
    Task PublishAsync<T>(T message) where T : class;
}