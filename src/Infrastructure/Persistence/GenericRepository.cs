using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class GenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> 
    where TEntity : class, IEntity<TPrimaryKey> 
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }
    
    public async Task<TEntity?> Get(TPrimaryKey id)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}