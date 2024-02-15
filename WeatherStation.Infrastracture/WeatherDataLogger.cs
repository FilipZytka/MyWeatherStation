using Microsoft.EntityFrameworkCore;
using WeatherStation.Entity;
using WeatherStationMVC.Data;

namespace WeatherStation.Infrastracture
{
    public class WeatherDataLogger
    {
        private readonly AuthDbContext _dbContext;
        public WeatherDataLogger(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddLogToDbContext(WeatherLog weatherLog)
        {
            _dbContext.Add(weatherLog);
            await _dbContext.SaveChangesAsync();
        }
        public async Task <IEnumerable<WeatherLog>> GetWeatherLogs()
        {
            return await _dbContext.WeatherLogs.ToListAsync();
        }
        public async Task RemoveLogToDbContext(WeatherLog weatherLog)
        {
            _dbContext.Remove(weatherLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
