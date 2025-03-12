namespace Domain.Users;

public interface IUser {
     int Id { get; set; }
     string? Email { get; set; }
     string? UserName { get; set; }
}