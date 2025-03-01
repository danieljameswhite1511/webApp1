using Domain.Entities;
using Domain.Result;

namespace Domain.Users;

public interface IUserManager<TUser, TPrimaryKey> where TUser : IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
{
    Task<IResult<TUser>> CreateUserAsync(TUser user);
    Task<TUser?> GetUserByIdAsync(TPrimaryKey id);
    Task<List<TUser>?> GetUsersAsync();
    Task<IResult<TUser>> ConfirmEmailAsync(TPrimaryKey userId, string code);
    
    Task<IResult<string>> GenerateEmailConfirmationTokenAsync(TPrimaryKey userId);
    
}