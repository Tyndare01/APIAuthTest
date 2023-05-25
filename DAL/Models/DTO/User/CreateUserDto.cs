using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTO;

public class CreateUserDto
{
    [Required]
    [MaxLength(50)]
    public string Pseudo { get; set; }
    
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    
    
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$")]
    public string Password { get; set; }
    
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }
}