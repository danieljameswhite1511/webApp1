using System.ComponentModel.DataAnnotations;

namespace Application.Users.Dtos;

public class SignInDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}