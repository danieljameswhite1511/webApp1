using Domain.Common.Entities;
using Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class GenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> 
    where TEntity : class, IEntity<TPrimaryKey> 
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    private readonly IdentityDbContext _dbContext;

    public GenericRepository(IdentityDbContext dbContext) {
        _dbContext = dbContext;
    }
    
    public async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public TEntity Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    { 
        _dbContext.Set<TEntity>().Update(entity);
       return  entity;
    }

    public TEntity Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return entity;
    }
}