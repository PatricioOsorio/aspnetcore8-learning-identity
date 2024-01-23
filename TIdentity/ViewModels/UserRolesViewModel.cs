using System.ComponentModel.DataAnnotations;

namespace TIdentity.ViewModels
{
  public class UserRolesViewModel
  {
    [Display(Name = "Id")]
    public string Id { get; set; }

    [Display(Name = "Nombre")]
    public string Nombre { get; set; }
    
    [Display(Name = "Apellido Paterno")]
    public string ApellidoPaterno { get; set; }
    
    [Display(Name = "Apellido Materno")]
    public string ApellidoMaterno { get; set; }

    [Display(Name = "Correo Electronico")]
    public string Email { get; set; }

    [Display(Name = "Roles")]
    public IEnumerable<string> Roles { get; set; }
  }
}
