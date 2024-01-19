using Microsoft.AspNetCore.Identity;
using TIdentity.Data.Migrations;

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
      //Seed Roles
      await roleManager.CreateAsync(new IdentityRole(Roles.SUPERADMIN.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.MODERADOR.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.BASICO.ToString()));
    }
  }
}
