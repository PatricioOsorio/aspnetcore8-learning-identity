using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
      var userRolesList = new List<UsersRolesViewModel>();

      foreach (var user in users)
      {
        var userWithRoles = new UsersRolesViewModel()
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

      var model = new List<UsersRolesManageViewModel>();

      foreach (var role in _roleManager.Roles)
      {
        var userRolesViewModel = new UsersRolesManageViewModel
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
    public async Task<IActionResult> EditUserRole(List<UsersRolesManageViewModel> model, string userId)
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

    // ==================================
    // ADMINISTRACION DE USUARIOS
    // ==================================
    // Read[GET] - User
    public async Task<IActionResult> ReadUsers()
    {
      return View(await _userManager.Users.ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
      CustomUser user = await _userManager.FindByIdAsync(id);

      if (user != null)
      {
        IdentityResult result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
          return RedirectToAction("ReadUsers");
        }

        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      return NotFound();
    }

    
    // Mostrar formulario para editar un usuario
    public async Task<IActionResult> EditUser(string id)
    {
      CustomUser user = await _userManager.FindByIdAsync(id);

      string selectedRoleId = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

      if (user != null)
      {
        // Obtener la lista de roles para mostrarla en la vista
        List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

        var model = new UserUpdateViewModel
        {
          Id = user.Id,
          Email = user.Email,
          Nombre = user.Nombre,
          ApellidoPaterno = user.ApellidoPaterno,
          ApellidoMaterno = user.ApellidoMaterno,
          //Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name }),
          //SelectedRoleId = selectedRoleId // Obtener el rol actual del usuario
        };

        return View(model);
      }

      return NotFound();
    }

    // Actualizar un usuario
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> UpdateUser(UpdateUserViewModel viewModel)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    Users user = await _userManager.FindByIdAsync(viewModel.Id);

    //    if (user != null)
    //    {
    //      user.Email = viewModel.Email;
    //      user.UserName = viewModel.Email;
    //      user.Nombre = viewModel.Nombre;
    //      user.ApellidoPaterno = viewModel.ApellidoPaterno;
    //      user.ApellidoMaterno = viewModel.ApellidoMaterno;

    //      // Actualizar el usuario
    //      IdentityResult result = await _userManager.UpdateAsync(user);

    //      if (result.Succeeded)
    //      {
    //        // Asignar el nuevo rol al usuario
    //        IdentityRole role = await _roleManager.FindByIdAsync(viewModel.RoleId);
    //        if (role != null)
    //        {
    //          // Remover los roles existentes del usuario
    //          await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

    //          // Asignar el nuevo rol al usuario
    //          await _userManager.AddToRoleAsync(user, role.Name);
    //        }

    //        return RedirectToAction("ReadUsers");
    //      }

    //      foreach (IdentityError error in result.Errors)
    //      {
    //        ModelState.AddModelError(string.Empty, error.Description);
    //      }
    //    }
    //  }

    //  // Obtener la lista de roles nuevamente para volver a mostrarla en caso de error
    //  List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
    //  viewModel.Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name });

    //  return View(viewModel);
    //}
  }
}
