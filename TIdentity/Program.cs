using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TIdentity.Data;
using TIdentity.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<CustomUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ========================================
// Seed
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var loggerFactory = services.GetRequiredService<ILoggerFactory>();
  try
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<CustomUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await ContextSeed.CreateRolesSeed(roleManager);
  }
  catch (Exception ex)
  {
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred seeding the DB.");
  }
}
// ========================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
