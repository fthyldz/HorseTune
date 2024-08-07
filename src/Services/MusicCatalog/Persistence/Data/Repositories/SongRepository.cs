using Application.Abstractions.Data.Repositories;
using Domain.Songs;
using Persistence.Data.Common;

namespace Persistence.Data.Repositories;

public class SongRepository(ApplicationDbContext context) : GenericRepository<Song, SongId>(context), ISongRepository
{
    
}