using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Queries.V1.Genres.GetGenreById;

public class GetGenreByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetGenreByIdQuery, GetGenreByIdQueryResponse?>
{
    public async Task<GetGenreByIdQueryResponse?> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(request.GenreId, cancellationToken);
        
        if (genre is null)
            throw new NotFoundException(Messages.RecordNotFound);
        
        return mapper.Map<GetGenreByIdQueryResponse>(genre);
    }
}