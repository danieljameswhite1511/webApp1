namespace Domain.Users.Entities;

public interface IUser {
     Guid Id { get; set; }
     string? Email { get; set; }
     string? UserName { get; set; }
}