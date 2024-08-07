using System.Linq.Expressions;
using Application.Abstractions.Data.Common;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Common;

public class GenericRepository<TEntity, TEntityId>(ApplicationDbContext context) : IGenericRepository<TEntity, TEntityId>
    where TEntity : BaseEntity<TEntityId> where TEntityId : struct
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<TEntity>> GetPagedResponseAsync(int page, int size,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
        Expression<Func<TEntity, bool>>? predicate,
        int pageNumber,
        int pageSize,
        string sortBy,
        bool sortAscending,
        CancellationToken cancellationToken = default
    )
    {
        var query = _dbSet.AsQueryable();

        if (predicate != null) query = query.Where(predicate);

        var totalCount = await query.CountAsync(cancellationToken);

        query = sortAscending
            ? query.OrderBy(p => EF.Property<object>(p, sortBy))
            : query.OrderByDescending(p => EF.Property<object>(p, sortBy));

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}