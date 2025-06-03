using System.Linq.Expressions;
using BloodDonation.Application.Repositories;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Infrastructures.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected DbSet<TEntity> dbSet;
    private readonly IClaimsService claimsService;
    public GenericRepository(IServiceProvider serviceProvider)
    {
        dbSet = serviceProvider.GetRequiredService<AppDbContext>().Set<TEntity>();
        claimsService = serviceProvider.GetRequiredService<IClaimsService>();
    }
    public async Task<Guid> CreateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        entity.CreatedBy = claimsService.CurrentUser;
        await dbSet.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task<List<Guid>> CreateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        List<Guid> guids = [];

        entities.ForEach(x =>
        {
            x.CreatedBy = claimsService.CurrentUser;
            guids.Add(x.Id);
        });
        await dbSet.AddRangeAsync(entities);
        return guids;

    }

    public void SoftRemove(TEntity entity)
    {
        entity.UpdatedBy = claimsService.CurrentUser;
        entity.UpdatedDate = DateTime.Now;
        entity.IsDeleted = true;
        dbSet.Update(entity);
    }

    public void SoftRemoveRange(List<TEntity> entities)
        => entities.ForEach(x =>
        {
            x.UpdatedBy = claimsService.CurrentUser;
            x.UpdatedDate = DateTime.Now;
            x.IsDeleted = true;
            dbSet.Update(x);
        });


    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
            => await includes
                .Aggregate(dbSet.AsQueryable(),
                (entity, property) => entity!.Include(property)).AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync(x => x.IsDeleted == false, cancellationToken);

    public async Task<List<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        => await includes.Aggregate(dbSet.AsQueryable(), (e, p) => e.Include(p))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public void Update(TEntity entity)
    {
        entity.UpdatedBy = claimsService.CurrentUser;
        entity.UpdatedDate = DateTime.Now;
        dbSet.Update(entity);
    }

    public void UpdateRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.UpdatedBy = claimsService.CurrentUser;
            x.UpdatedDate = DateTime.Now;
            dbSet.Update(x);
        });
    }

    public async Task<List<TEntity>?> WhereAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    => await includes.Aggregate(dbSet.AsQueryable(), (e, p) => e.Include(p))
        .AsNoTracking()
        .Where(predicate)
        .Where(x => x.IsDeleted == false)
        .ToListAsync(cancellationToken);

    public virtual async Task<IEnumerable<TEntity>> Search(
           Expression<Func<TEntity, bool>> filter = null!,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
           string includeProperties = "",
           int? pageIndex = null, // Optional parameter for pagination (page number)
           int? pageSize = null)  // Optional parameter for pagination (number of records per page)
    {
        IQueryable<TEntity> query = dbSet;


        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Implementing pagination
        if (pageIndex.HasValue && pageSize.HasValue)
        {
            // Ensure the pageIndex and pageSize are valid
            int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
            int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity> GetByCondition(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.FirstOrDefaultAsync()!;
    }


}