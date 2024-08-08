using Application.Abstractions.Data.Common;
using AutoMapper;
using Domain.Albums;
using Domain.Artists;
using Domain.Genres;
using MediatR;

namespace Application.Commands.V1.Artists.CreateArtist;

public class CreateArtistCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateArtistCommand, CreateArtistCommandResponse>
{
    public async Task<CreateArtistCommandResponse> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artistRepository = unitOfWork.Repository<Artist, ArtistId>();
        var genreRepository = unitOfWork.Repository<Genre, GenreId>();
        
        try
        {
            var artist = new Artist(new ArtistId(Guid.NewGuid()), request.Name, request.Biography, request.FormationYear);

            await unitOfWork.BeginTransactionAsync(cancellationToken);
        
            await artistRepository.AddAsync(artist, cancellationToken);
            
            /*var genres = await genreRepository.GetAllAsync(cancellationToken);
            
            artist.AddGenre(genres.Select(g => g.Id).FirstOrDefault());*/

            var album = new Album(new AlbumId(Guid.NewGuid()), "Deneme Albüm", 2014);
            
            await unitOfWork.Albums.AddAsync(album, cancellationToken);
            
            artist.AddAlbum(album.Id);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return mapper.Map<CreateArtistCommandResponse>(artist);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
            // return new ErrorResponse<CreateArtistCommandResponse>("Internal Server Error", $"An error occurred: {ex.Message}");
        }
    }
}