using System.Linq.Expressions;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Application.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>?> WhereAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<Guid>> CreateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void UpdateRange(List<TEntity> entities);
    void SoftRemove(TEntity entity);
    void SoftRemoveRange(List<TEntity> entities);

    Task<IEnumerable<TEntity>> Search(
            Expression<Func<TEntity, bool>> filter = null!,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
    Task<TEntity> GetByCondition(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
    Task<int> Count(Expression<Func<TEntity, bool>> filter = null!);

}