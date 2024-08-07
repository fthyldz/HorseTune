using Application.Abstractions.Data.Repositories;
using Domain.Albums;
using Domain.Artists;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Common;

namespace Persistence.Data.Repositories;

public class AlbumRepository(ApplicationDbContext context) : GenericRepository<Album, AlbumId>(context), IAlbumRepository
{
    public async Task<IReadOnlyList<Album>> GetAlbumsWithArtistIdAsync(ArtistId artistId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(a => a.Artists.Any(aa => aa.ArtistId == artistId)).ToListAsync(cancellationToken);
    }

    public async Task<Album?> GetAlbumsWithArtistIdAndAlbumIdAsync(ArtistId artistId, AlbumId albumId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Artists.Any(aa => aa.ArtistId == artistId) && a.Id == albumId, cancellationToken);
    }

}