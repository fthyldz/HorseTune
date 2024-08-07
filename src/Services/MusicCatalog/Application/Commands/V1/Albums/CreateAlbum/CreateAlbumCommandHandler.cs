using Application.Abstractions.Data.Common;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Domain.Albums;
using MediatR;

namespace Application.Commands.V1.Albums.CreateAlbum;

public class CreateAlbumCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAlbumCommand, CreateAlbumCommandResponse>
{
    public async Task<CreateAlbumCommandResponse> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await unitOfWork.Artists.GetByIdAsync(request.ArtistId, cancellationToken);
            
            if (artist is null)
                throw new NotFoundException(Messages.RecordNotFound);
            
            await unitOfWork.BeginTransactionAsync(cancellationToken);
        
            var album = mapper.Map<Album>(request);

            await unitOfWork.Albums.AddAsync(album, cancellationToken);
            
            album.AddArtist(request.ArtistId);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return mapper.Map<CreateAlbumCommandResponse>(album);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}