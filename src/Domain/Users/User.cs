using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.Users;

public class User : IEntity<int> {
    public int Id { get; private set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

