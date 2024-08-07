using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Queries.V1.Albums.GetAlbumById;

public class GetAlbumByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAlbumByIdQuery, GetAlbumByIdQueryResponse>
{
    public async Task<GetAlbumByIdQueryResponse> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var album = await unitOfWork.Albums.GetAlbumsWithArtistIdAndAlbumIdAsync(request.ArtistId, request.AlbumId, cancellationToken);
        
        if (album is null)
            throw new NotFoundException(Messages.RecordNotFound);
        
        return mapper.Map<GetAlbumByIdQueryResponse>(album);
    }
}