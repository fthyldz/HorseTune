using Domain.Primitives;

namespace Domain.Events;

public record ArtistImageUploadedEvent(string OriginalPath) : IntegrationEvent(Guid.NewGuid());