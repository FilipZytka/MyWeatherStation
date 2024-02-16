using Microsoft.AspNetCore.Mvc;
using WeatherStation.Entity;
using WeatherStationMVC.Data;
using WeatherStation.Infrastracture;
using System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WeatherStationMVC.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly AuthDbContext _dbContext;
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherConnection _weatherConnection;
        private readonly UserManager<ApplicationUser> _userManager;
        private WeatherDataLogger _dataLogger;
        
        public WeatherController(ILogger<WeatherController> logger, AuthDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _dataLogger = new WeatherDataLogger(_dbContext);
            _logger = logger;
            _weatherConnection = new WeatherConnection();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string Name, WeatherLog weatherLog)
        {
        
            if (string.IsNullOrEmpty(Name))
            {
                ModelState.AddModelError("Name", "City name is required.");
                return View("Create");
            }
            var weatherData = await _weatherConnection.GetWeatherDataAsync(Name);

            if(weatherData != null)
            {
                var userId = _userManager.GetUserId(User);

                weatherLog.UserId = userId;
                
                 _weatherConnection.SaveWeatherDataToLog(weatherLog);
                await _dataLogger.AddLogToDbContext(weatherLog);
                //return RedirectToAction(nameof(PrintLogsForActiveUser));
                return View("Results", weatherData);
            }
            return View("Create");  
        }
        [HttpGet]
        public async Task<IActionResult> PrintLogsForActiveUser(IEnumerable<WeatherLog> weatherLog, int? pageNumber)
        {
            int pageSize = 5;
            var currentUser = _userManager.GetUserId(User);
            weatherLog = await _dataLogger.GetWeatherLogs();
            var userLogs = weatherLog.Where(ul => ul.UserId == currentUser);

            return View("History", PaginatedList<WeatherLog>
                .Create(_dbContext.WeatherLogs.ToList(), pageNumber ?? 1, pageSize));
           // return View("History", userLogs);
        }
        [HttpPost]
    
        public async Task<IActionResult> Delete(WeatherLog weatherLog)
        {
            var weatherLogToDelete = _dbContext.WeatherLogs
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == weatherLog.Id);

            if (weatherLogToDelete != null)
            {
                _dbContext.WeatherLogs.Remove(weatherLogToDelete);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(PrintLogsForActiveUser));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult Results()
        {
            return View();
        }
    }
}