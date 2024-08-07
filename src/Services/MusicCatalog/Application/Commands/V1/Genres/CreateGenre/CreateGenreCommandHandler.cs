using Application.Abstractions.Data.Common;
using AutoMapper;
using Domain.Genres;
using MediatR;

namespace Application.Commands.V1.Genres.CreateGenre;

public class CreateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateGenreCommand, CreateGenreCommandResponse>
{
    public async Task<CreateGenreCommandResponse> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        try
        { 
            var genre = mapper.Map<Genre>(request);

            await unitOfWork.BeginTransactionAsync(cancellationToken);
        
            await unitOfWork.Genres.AddAsync(genre, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return mapper.Map<CreateGenreCommandResponse>(genre);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            
            throw;
        }
    }
}