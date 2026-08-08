using Domain.Common.Entities;
using Domain.Systems.Entities;

namespace Domain.Users.Entities;

public class User : IUser, IEntity<Guid> {
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? UserName { get; set; }
 
}

