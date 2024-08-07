using Application.Abstractions.Data.Repositories;
using Domain.Artists;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Common;

namespace Persistence.Data.Repositories;

public class ArtistRepository(ApplicationDbContext context) : GenericRepository<Artist, ArtistId>(context), IArtistRepository
{
    public async Task<IReadOnlyList<object>> GetAllWithGenresAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Include(a => a.Genres).ThenInclude(ag => ag.Genre).Select(a => new
        {
            Id = a.Id.Value,
            Name = a.Name,
            Biography = a.Biography,
            FormationYear = a.FormationYear,
            Genres = a.Genres.Select(ag => new
            {
                Id = ag.Genre.Id.Value,
                Name = ag.Genre.Name
            })
        }).ToListAsync(cancellationToken);
    }
}