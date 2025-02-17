using Domain.Entities;

namespace Domain.Repositories;

public interface IRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey>{
    
    TEntity Get(TPrimaryKey id);
    
}