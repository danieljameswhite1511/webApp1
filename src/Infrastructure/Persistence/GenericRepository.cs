using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence;

public class GenericRepository<TEntity, TPrimaryKey> :IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public TEntity? Get(TPrimaryKey id)
    {
        return _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
    }
}