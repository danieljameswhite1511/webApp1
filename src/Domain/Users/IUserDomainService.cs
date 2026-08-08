using Domain.Users.Entities;

namespace Domain.Users;

public interface IUserDomainService {
    Task<User?> GetUserById(Guid userId);
    Task<User?> GetUserByEmail(string email);
    Task<List<User>?> GetUsers();
}