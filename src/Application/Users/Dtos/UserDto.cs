namespace Application.Users.Dtos;

public class UserDto {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public int SystemId { get; set; }
    public int? TenantId { get; set; }
}