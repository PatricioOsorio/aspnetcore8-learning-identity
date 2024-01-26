using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TIdentity.ViewModels
{
  public class UserUpdateViewModel
  {
    public string Id { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Nombre(s)")]
    [Required]
    public string Nombre { get; set; }

    [Display(Name = "Apellido paterno")]
    [Required]
    public string ApellidoPaterno { get; set; }

    [Display(Name = "Apellido materno")]
    [Required]
    public string ApellidoMaterno { get; set; }

    [Display(Name = "Rol")]
    [Required]
    public string SelectedRole { get; set; }

    public SelectList Roles { get; set; }

    public string SelectedRoleId { get; set; }
  }
}
