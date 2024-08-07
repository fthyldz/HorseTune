using Application.Abstractions.Data.Repositories;
using Domain.Primitives;

namespace Application.Abstractions.Data.Common;

public interface IUnitOfWork : IDisposable
{
    IArtistRepository Artists { get; }
    IAlbumRepository Albums { get; }
    ISongRepository Songs { get; }
    IGenreRepository Genres { get; }
    
    IGenericRepository<TEntity, TEntityId> Repository<TEntity, TEntityId>() where TEntity : BaseEntity<TEntityId> where TEntityId : struct;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}