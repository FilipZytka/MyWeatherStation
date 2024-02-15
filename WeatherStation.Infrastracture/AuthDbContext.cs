using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherStation.Entity;

namespace WeatherStationMVC.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
   
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
      
    }
  
    public DbSet<WeatherLog> WeatherLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
                   .HasMany(u => u.WeatherLogs)
                   .WithOne(w => w.User)
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);    
    }
}
