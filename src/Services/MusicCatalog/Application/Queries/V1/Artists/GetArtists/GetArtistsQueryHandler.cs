using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Domain.Artists;
using MediatR;

namespace Application.Queries.V1.Artists.GetArtists;

public class GetArtistsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetArtistsQuery, IEnumerable<GetArtistsQueryResponse>>
{
    public async Task<IEnumerable<GetArtistsQueryResponse>> Handle(GetArtistsQuery request, CancellationToken cancellationToken)
    {
        var artistRepository = unitOfWork.Repository<Artist, ArtistId>();

        var artists = await artistRepository.GetAllAsync(cancellationToken);

        if (artists is null || artists.Count == 0)
            throw new NotFoundException(Messages.RecordNotFound);
        
        return mapper.Map<IEnumerable<GetArtistsQueryResponse>>(artists);
    }
}