using System.ComponentModel.DataAnnotations;

namespace Application.Users.Dtos;

public class PasswordResetRequestDto {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required] 
    public string Token { get; set; }
}