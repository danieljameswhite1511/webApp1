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
    
    public async Task<TEntity?> Get(TPrimaryKey id)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}