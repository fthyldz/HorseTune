using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Domain.Genres;
using MediatR;

namespace Application.Queries.V1.Genres.GetGenres;

public class GetGenresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetGenresQuery, IEnumerable<GetGenresQueryResponse>>
{
    public async Task<IEnumerable<GetGenresQueryResponse>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {

        if (!(await unitOfWork.Genres.GetAllAsync(cancellationToken)).Any())
        {
            var genresDb = new List<Genre>()
            {
                new Genre(new GenreId(Guid.NewGuid()), "Rock"),
                new Genre(new GenreId(Guid.NewGuid()), "Pop"),
                new Genre(new GenreId(Guid.NewGuid()), "Jazz"),
                new Genre(new GenreId(Guid.NewGuid()), "Blues"),
                new Genre(new GenreId(Guid.NewGuid()), "Hip Hop"),
                new Genre(new GenreId(Guid.NewGuid()), "Rap"),
                new Genre(new GenreId(Guid.NewGuid()), "Country"),
                new Genre(new GenreId(Guid.NewGuid()), "Electronic"),
                new Genre(new GenreId(Guid.NewGuid()), "Classical"),
                new Genre(new GenreId(Guid.NewGuid()), "Reggae"),
                new Genre(new GenreId(Guid.NewGuid()), "Metal"),
                new Genre(new GenreId(Guid.NewGuid()), "Folk")
            };

            foreach (var genre in genresDb)
            {
                await unitOfWork.Genres.AddAsync(genre, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var genres = await unitOfWork.Genres.GetAllAsync(cancellationToken);

        if (genres is null || genres.Count == 0)
            throw new NotFoundException(Messages.RecordNotFound);

        return mapper.Map<IEnumerable<GetGenresQueryResponse>>(genres);
    }
}