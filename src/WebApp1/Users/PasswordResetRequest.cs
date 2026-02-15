using System.ComponentModel.DataAnnotations;

namespace WebApp1.Users;

public class PasswordResetRequest {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required] 
    public string Token { get; set; }
}