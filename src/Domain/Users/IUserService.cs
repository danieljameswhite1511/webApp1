using Domain.Common.Entities;
using Domain.Users.Entities;

namespace Domain.Users;

public interface  IUserService<TUser, TPrimaryKey> where TUser : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey> {
    Task<TUser?> GetUserByIdAsync(TPrimaryKey id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<TUser>?> GetUsersAsync();
}