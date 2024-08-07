using Application.Abstractions.Data.Common;
using Application.Abstractions.Data.Repositories;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Data.Common;

public class UnitOfWork(ApplicationDbContext context, IPublisher publisher, IArtistRepository artistRepository, IAlbumRepository albumRepository, ISongRepository songRepository, IGenreRepository genreRepository) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = [];
    private IDbContextTransaction? _transaction;


    public IArtistRepository Artists { get; } = artistRepository;
    public IAlbumRepository Albums { get; } = albumRepository;
    public ISongRepository Songs { get; } = songRepository;
    public IGenreRepository Genres { get; } = genreRepository;

    public IGenericRepository<TEntity, TEntityId> Repository<TEntity, TEntityId>() where TEntity : BaseEntity<TEntityId> where TEntityId : struct
    {
        var entityType = typeof(TEntity);
        if (_repositories.TryGetValue(entityType, out var repository)) return (IGenericRepository<TEntity, TEntityId>)repository;
        var repositoryType = typeof(GenericRepository<,>);
        var repositoryInstance =
            Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TEntityId)), context);
        _repositories.Add(entityType, repositoryInstance!);
        return (IGenericRepository<TEntity, TEntityId>)_repositories[entityType];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            await _transaction?.CommitAsync(cancellationToken)!;
            
            var events = context.ChangeTracker.Entries<BaseEntity>().SelectMany(x => x.Entity.Events).ToList();
            foreach (var @event in events)
            {
                await publisher.Publish(@event, cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            context.Dispose();
        }
    }
}