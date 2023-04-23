using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTO;

public class ChangeDataDTO
{
    [MaxLength(50)]
    public string Firstname { get; set; }
    
    [MaxLength(50)]
    public string Lastname { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Pseudo { get; set; }
}