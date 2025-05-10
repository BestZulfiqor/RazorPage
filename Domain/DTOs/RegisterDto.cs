using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class RegisterDto
{
    [Required] public string Username { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    public string? Phone { get; set; }
    [Required] public string? Password { get; set; }
    [Required, Compare("Password")] public string? ConfirmPassword { get; set; }
}