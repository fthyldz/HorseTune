using AutoMapper;
using Domain.Events;
using MassTransit;
using MediatR;

namespace Infrastructure.MessageBuses.Consumers;

public class ArtistImageProcessedConsumer(IMapper mapper, IMediator sender) : IConsumer<ArtistImageProcessedEvent>
{

    public async Task Consume(ConsumeContext<ArtistImageProcessedEvent> context)
    {
        Console.WriteLine("ArtistImageProcessedEvent consumed");
    }
}