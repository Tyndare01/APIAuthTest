using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTO;

public class ChangePhoneNumberDTO
{
    [Required]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }
}