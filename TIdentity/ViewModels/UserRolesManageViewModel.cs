using System.ComponentModel.DataAnnotations;

namespace TIdentity.ViewModels
{
  public class UserRolesManageViewModel
  {
    [Display(Name = "Id del rol")]
    public string RoleId { get; set; }

    [Display(Name = "Nombre del rol")]
    public string RoleName { get; set; }

    [Display(Name = "Está seleccionado")]
    public bool IsSelected { get; set; }
  }
}
