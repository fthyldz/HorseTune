using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Queries.V1.Artists.GetArtistById;

public class GetArtistByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetArtistByIdQuery, GetArtistByIdQueryResponse>
{
    public async Task<GetArtistByIdQueryResponse> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await unitOfWork.Artists.GetByIdAsync(request.ArtistId, cancellationToken);
        
        if (artist is null)
            throw new NotFoundException(Messages.RecordNotFound);
        
        return mapper.Map<GetArtistByIdQueryResponse>(artist);
    }
}