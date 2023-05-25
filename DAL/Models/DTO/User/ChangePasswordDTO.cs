using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTO;

public class ChangePasswordDTO
{
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$")]
    public string ActualPassword { get; set; }
    
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$")]
    
    public string NewPassword { get; set;}
    
    [Required]
    [Compare("NewPassword")]
    public string NewPasswordConfirmation { get; set; }
    
}