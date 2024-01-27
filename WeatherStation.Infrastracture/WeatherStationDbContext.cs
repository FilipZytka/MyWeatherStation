using WeatherStation.Entity;
using Microsoft.EntityFrameworkCore;

namespace WeatherStation.Infrastracture 
{
    public class WeatherStationDbContext : DbContext 
    {
        public WeatherStationDbContext(DbContextOptions<WeatherStationDbContext> options) : base(options)
        {
            
        }
        public DbSet<WeatherLog> WeatherLogs { get; set; }


    }

}


