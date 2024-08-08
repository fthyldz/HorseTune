using Application.Abstractions.Infrastructure;
using Domain.Events;
using MediatR;

namespace Application.Commands.V1.Artists.EventHandlers;

public class ArtistImageUploadedEventHandler(IMessageBus messageBus) : INotificationHandler<ArtistImageUploadedEvent>
{
    public async Task Handle(ArtistImageUploadedEvent notification, CancellationToken cancellationToken)
    {
        await messageBus.PublishAsync(notification, cancellationToken);
    }
}