using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Users;

public class User : IUser, IEntity<int> {
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }

    public string? UserName { get; set; }
}

