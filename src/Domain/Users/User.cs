using Domain.Entities;

namespace Domain.Users;

public class User : IEntity<int>
{
    public int Id { get; private set; }
    public string? Name { get; set; }
    
    
}

