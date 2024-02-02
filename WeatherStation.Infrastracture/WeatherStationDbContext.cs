using WeatherStation.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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


