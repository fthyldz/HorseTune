using Application.Abstractions.Data.Common;
using Domain.Artists;

namespace Application.Abstractions.Data.Repositories;

public interface IArtistRepository : IGenericRepository<Artist, ArtistId>
{
    Task<IReadOnlyList<object>> GetAllWithGenresAsync(CancellationToken cancellationToken = default);
}