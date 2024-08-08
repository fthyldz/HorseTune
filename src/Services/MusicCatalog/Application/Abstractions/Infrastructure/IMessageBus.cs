namespace Application.Abstractions.Infrastructure;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}