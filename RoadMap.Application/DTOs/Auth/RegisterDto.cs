using System.ComponentModel.DataAnnotations;

namespace RoadMap.Application.DTOs.Auth;
public class RegisterDto
{
    [Required]
    [MinLength(5)]
    public string Username { get; set; } =  string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } =  string.Empty;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } =  string.Empty;
}