namespace Domain.Users.Entities;

public interface IUser {
     int Id { get; set; }
     string? Email { get; set; }
     string? UserName { get; set; }
}