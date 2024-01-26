// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TIdentity.Data;
using TIdentity.Models;

namespace TIdentity.Areas.Identity.Pages.Account
{
  [Authorize(Roles = "SUPERADMIN")]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<CustomUser> _signInManager;
    private readonly UserManager<CustomUser> _userManager;
    private readonly IUserStore<CustomUser> _userStore;
    private readonly IUserEmailStore<CustomUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterModel(
        UserManager<CustomUser> userManager,
        IUserStore<CustomUser> userStore,
        SignInManager<CustomUser> signInManager,
        ILogger<RegisterModel> logger,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender)
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      _roleManager = roleManager;
      _emailSender = emailSender;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
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

    }


    public async Task OnGetAsync(string returnUrl = null)
    {
      ReturnUrl = returnUrl;
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

      // Obtener la lista de roles y asignarla a la propiedad Roles de InputModel
      var roles = await _roleManager.Roles.ToListAsync();

      // Inicializar la propiedad Roles en InputModel
      Input = new InputModel
      {
        Roles = new SelectList(roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name))
      };
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        //var user = CreateUser();
        var user = new CustomUser { UserName = Input.Email, Nombre = Input.Nombre, ApellidoPaterno = Input.ApellidoPaterno, ApellidoMaterno = Input.ApellidoMaterno };

        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
          var defaultRole = _roleManager.FindByNameAsync("BASICO").Result;

          // Asigna el rol predeterminado al usuario
          if (defaultRole != null)
          {
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, defaultRole.Name);
          }

          // Asigna el rol seleccionado al usuario si se ha seleccionado uno
          if (!string.IsNullOrEmpty(Input.SelectedRole))
          {
            var selectedRole = await _roleManager.FindByNameAsync(Input.SelectedRole);
            if (selectedRole != null)
            {
              await _userManager.AddToRoleAsync(user, selectedRole.Name);
            }
          }

          _logger.LogInformation("User created a new account with password.");

          var userId = await _userManager.GetUserIdAsync(user);
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          var callbackUrl = Url.Page(
              "/Account/ConfirmEmail",
              pageHandler: null,
              values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
              protocol: Request.Scheme);

          await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
              $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          if (_userManager.Options.SignIn.RequireConfirmedAccount)
          {
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
          }
          else
          {
            // Establece el mensaje en TempData
            TempData["SuccessMessage"] = "Your account has been created successfully. Please confirm your email.";

            // No inicia sesión automáticamente, simplemente redirige al returnUrl
            //await _signInManager.SignInAsync(user, isPersistent: false);

            return LocalRedirect(returnUrl);
          }
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    private CustomUser CreateUser()
    {
      try
      {
        return Activator.CreateInstance<CustomUser>();
      }
      catch
      {
        throw new InvalidOperationException($"Can't create an instance of '{nameof(CustomUser)}'. " +
            $"Ensure that '{nameof(CustomUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
      }
    }

    private IUserEmailStore<CustomUser> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<CustomUser>)_userStore;
    }
  }
}
