using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TIdentity.Models
{
  public class CustomUser:IdentityUser
  {
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Nombre(s)")]
    [Required]
    public string? Nombre { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido paterno")]
    [Required]
    public string? ApellidoPaterno { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido materno")]
    [Required]
    public string? ApellidoMaterno { get; set; }
  }
}
