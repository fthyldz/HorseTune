using Application.Abstractions.Data.Common;
using Domain.Genres;

namespace Application.Abstractions.Data.Repositories;

public interface IGenreRepository : IGenericRepository<Genre, GenreId>
{
}