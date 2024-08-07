using System.Linq.Expressions;
using Domain.Primitives;

namespace Application.Abstractions.Data.Common;

public interface IGenericRepository<TEntity, TEntityId> where TEntity : BaseEntity<TEntityId> where TEntityId : struct
{
    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<IReadOnlyList<TEntity>> GetPagedResponseAsync(int page, int size, CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetPagedAsync(
        Expression<Func<TEntity, bool>>? predicate,
        int pageNumber,
        int pageSize,
        string sortBy,
        bool sortAscending,
        CancellationToken cancellationToken = default
    );
}