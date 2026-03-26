using Domain.Common.Entities;

namespace Domain.Common.Repositories;

public interface IRepository<TEntity, TPrimaryKey> where TEntity : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey> {
    
    Task<TEntity?> GetAsync(TPrimaryKey id);
    IQueryable<TEntity> GetAll();
    TEntity Insert(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
    
}