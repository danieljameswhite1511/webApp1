using Domain.Entities;

namespace Domain.Users;

public interface IUserManager<TUser, TPrimaryKey> where TUser : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
{
    Task<TUser?> GetUserByIdAsync(TPrimaryKey id);
    
    
}