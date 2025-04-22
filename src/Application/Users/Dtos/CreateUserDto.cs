using System.ComponentModel.DataAnnotations;

namespace Application.Users.Dtos;

public class CreateUserDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}