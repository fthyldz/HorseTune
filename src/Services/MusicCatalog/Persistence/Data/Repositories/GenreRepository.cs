using Application.Abstractions.Data.Repositories;
using Domain.Genres;
using Persistence.Data.Common;

namespace Persistence.Data.Repositories;

public class GenreRepository(ApplicationDbContext context) : GenericRepository<Genre, GenreId>(context), IGenreRepository
{
}