using Application.Abstractions.Data.Common;
using Domain.Songs;

namespace Application.Abstractions.Data.Repositories;

public interface ISongRepository : IGenericRepository<Song, SongId>
{
}