using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTO;

public class LoginDTO
{
    
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$")]
    public string Password { get; set; }
}