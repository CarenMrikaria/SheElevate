using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SheElevate.Areas.Identity.Data;
using SheElevate.Models;

namespace SheElevate.Data;

public class SheElevateContext : IdentityDbContext<SheElevateUser>
{
    public DbSet<Mentors> Mentors { get; set; }
    public DbSet<Company> CompanyTable { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Mentee> Mentees { get; set; }

    public SheElevateContext(DbContextOptions<SheElevateContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
