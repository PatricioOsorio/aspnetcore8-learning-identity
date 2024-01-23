using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TIdentity.Data;
//using TIdentity.Data.Migrations;
using TIdentity.Models;
using TIdentity.ViewModels;

namespace TIdentity.Controllers
{
  [Authorize(Roles = "SUPERADMIN")]
  public class SuperAdminController : Controller
  {
    private readonly UserManager<CustomUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SuperAdminController(UserManager<CustomUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _context = context;
      _roleManager = roleManager;
    }

    // ==================================
    // ADMINISTRACION DE ROLES
    // ==================================
    public async Task<IActionResult> ReadRoles()
    {
      var roles = _roleManager.Roles.ToList();
      return View(roles);
    }


    // ==================================
    // ADMINISTRACION DE ROLES DE USUARIO
    // ==================================
    public async Task<IActionResult> ReadUserRoles()
    {
      var users = await _userManager.Users.ToListAsync();
      var userRolesList = new List<UserRolesViewModel>();

      foreach (var user in users)
      {
        var userWithRoles = new UserRolesViewModel()
        {
          Id = user.Id,
          Email = user.Email,
          Nombre = user.Nombre,
          ApellidoPaterno = user.ApellidoPaterno,
          ApellidoMaterno = user.ApellidoMaterno,
        };

        userWithRoles.Roles = await GetUserRoles(user);

        userRolesList.Add(userWithRoles);
      }
      return View(userRolesList);
    }

    private async Task<List<string>> GetUserRoles(CustomUser user)
    {
      return new List<string>(await _userManager.GetRolesAsync(user));
    }

    public async Task<IActionResult> EditUserRole(string userId)
    {
      ViewBag.userId = userId;

      var user = await _userManager.FindByIdAsync(userId);

      if (user == null)
      {
        ViewBag.ErrorMessage = $"Usuario con el Id = {userId} no se pudo encontrar";
        return View("NotFound");
      }

      ViewBag.UserName = user.UserName;

      var model = new List<UserRolesManageViewModel>();

      foreach (var role in _roleManager.Roles)
      {
        var userRolesViewModel = new UserRolesManageViewModel
        {
          RoleId = role.Id,
          RoleName = role.Name
        };
        if (await _userManager.IsInRoleAsync(user, role.Name))
        {
          userRolesViewModel.IsSelected = true;
        }
        else
        {
          userRolesViewModel.IsSelected = false;
        }
        model.Add(userRolesViewModel);
      }
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserRole(List<UserRolesManageViewModel> model, string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);

      if (user == null)
      {
        return View();
      }

      var roles = await _userManager.GetRolesAsync(user);
      var result = await _userManager.RemoveFromRolesAsync(user, roles);

      if (!result.Succeeded)
      {
        ModelState.AddModelError("", "No se puede remover los roles existentes del usuario");
        return View(model);
      }

      result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

      if (!result.Succeeded)
      {
        ModelState.AddModelError("", "No se pudo agregar roles al usuario");
        return View(model);
      }
      return RedirectToAction("ReadUserRoles");
    }
  }
}
