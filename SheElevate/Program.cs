using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SheElevate.Areas.Identity.Data;
using SheElevate.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SheElevate") ?? throw new InvalidOperationException("Connection string 'SheElevateContextConnection' not found.");

builder.Services.AddDbContext<SheElevateContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<SheElevateUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<SheElevateContext>();

builder.Logging.ClearProviders();  // This will remove all default providers
builder.Logging.AddConsole();      // Enable console logging
builder.Logging.AddDebug();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<SheElevateUser>>();
    await SeedRolesAsync(roleManager, userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, UserManager<SheElevateUser> userManager)
{
    // Define roles
    var roles = new[] { "Mentor", "Mentee", "Admin" };

    foreach (var role in roles)
    {
        // Check if the role already exists, if not create it
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Optional: Seed admin user (if needed)
    var adminEmail = "carenengels01@icloud.com"; 
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var newAdmin = new SheElevateUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(newAdmin, "Caren12345."); 
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}
