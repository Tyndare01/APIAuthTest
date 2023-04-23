using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Models.DTO;

public class ChangeRoleDTO
{
    [Required]
    public Roles role { get; set; }
}