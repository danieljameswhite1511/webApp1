namespace Domain.Users;

public interface IUserDomainService
{
    Task<User?> GetUserById(int userId);
}