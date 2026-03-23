using Domain.Common.Entities;
using Domain.Systems.Entities;

namespace Domain.Users.Entities;

public class User : IUser, IEntity<int> {
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? UserName { get; set; }
    public List<SystemTenant> SystemTenants { get; set; }
}

