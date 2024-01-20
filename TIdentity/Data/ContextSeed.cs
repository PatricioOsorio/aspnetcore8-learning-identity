using Microsoft.AspNetCore.Identity;
//using TIdentity.Data.Migrations;
using TIdentity.Models;

namespace TIdentity.Data
{
  public enum Roles
  {
    SUPERADMIN,
    ADMIN,
    MODERADOR,
    BASICO,
  }

  public class ContextSeed
  {
    public static async Task CreateRolesSeed(
        RoleManager<IdentityRole> roleManager
      )
    {
      await roleManager.CreateAsync(new IdentityRole(Roles.SUPERADMIN.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.MODERADOR.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.BASICO.ToString()));
    }

    public static async Task CreateUserSuperadmin(
      UserManager<CustomUser> userManager
    )
    {
      //Seed Default User
      var newUser = new CustomUser()
      {
        Nombre = "superadmin",
        ApellidoPaterno = "lastname1",
        ApellidoMaterno = "lastname2",
        UserName = "superadmin@hotmail.com",
        Email = "superadmin@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.SUPERADMIN.ToString());
        }
      }
    }

    public static async Task CreateUserAdmin(
      UserManager<CustomUser> userManager
    )
    {
      //Seed Default User
      var newUser = new CustomUser()
      {
        Nombre = "admin",
        ApellidoPaterno = "lastname1",
        ApellidoMaterno = "lastname2",
        UserName = "admin@hotmail.com",
        Email = "admin@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.ADMIN.ToString());
        }
      }
    }

    public static async Task CreateUserModerador(
      UserManager<CustomUser> userManager
    )
    {
      //Seed Default User
      var newUser = new CustomUser()
      {
        Nombre = "moderador",
        ApellidoPaterno = "lastname1",
        ApellidoMaterno = "lastname2",
        UserName = "moderador@hotmail.com",
        Email = "moderador@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.MODERADOR.ToString());
        }
      }
    }

    public static async Task CreateUserBasico(
      UserManager<CustomUser> userManager
    )
    {
      //Seed Default User
      var newUser = new CustomUser()
      {
        Nombre = "Basico",
        ApellidoPaterno = "lastname1",
        ApellidoMaterno = "lastname2",
        UserName = "basico@hotmail.com",
        Email = "basico@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.BASICO.ToString());
        }
      }
    }
  }
}
