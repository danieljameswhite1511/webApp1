using Domain.Common.Entities;

namespace Domain.Common.Repositories;

public interface IRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
{
    
    Task<TEntity?> Get(TPrimaryKey id);
    
}