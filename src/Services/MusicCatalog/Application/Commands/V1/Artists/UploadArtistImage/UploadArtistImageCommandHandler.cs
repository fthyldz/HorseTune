using Application.Abstractions.Data.Common;
using Application.Abstractions.Infrastructure;
using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Commands.V1.Artists.UploadArtistImage;

public class UploadArtistImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService) : IRequestHandler<UploadArtistImageCommand, UploadArtistImageCommandResponse>
{
    public async Task<UploadArtistImageCommandResponse> Handle(UploadArtistImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await unitOfWork.Artists.GetByIdAsync(request.ArtistId, cancellationToken);
            
            if (artist is null)
            {
                throw new NotFoundException(Messages.RecordNotFound);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
    
            await fileStorageService.UploadFileAsync(request.Image, fileName, cancellationToken);
            
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            
            artist.UploadImage(fileName);
            
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new UploadArtistImageCommandResponse(fileName);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
            // return new ErrorResponse<UploadArtistImageCommandResponse>("Internal Server Error", $"An error occurred: {ex.Message}");
        }
    }
}