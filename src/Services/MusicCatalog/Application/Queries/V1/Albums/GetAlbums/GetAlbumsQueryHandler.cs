using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Queries.V1.Albums.GetAlbums;

public class GetAlbumsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAlbumsQuery, IEnumerable<GetAlbumsQueryResponse>>
{
    public async Task<IEnumerable<GetAlbumsQueryResponse>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
    {
        var albums = await unitOfWork.Albums.GetAlbumsWithArtistIdAsync(request.ArtistId, cancellationToken);

        if (albums is null || albums.Count == 0)
            throw new NotFoundException(Messages.RecordNotFound);
        
        return mapper.Map<IEnumerable<GetAlbumsQueryResponse>>(albums);
    }
}