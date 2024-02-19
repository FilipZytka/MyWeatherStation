using Microsoft.AspNetCore.Mvc;
using WeatherStation.Entity;
using WeatherStationMVC.Data;
using WeatherStation.Infrastracture;
using System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WeatherStationMVC.Pagination;

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
        private PaginationConfig _paginationConfig;
        
        public WeatherController(ILogger<WeatherController> logger, AuthDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _dataLogger = new WeatherDataLogger(_dbContext);
            _logger = logger;
            _weatherConnection = new WeatherConnection();
            _paginationConfig = new PaginationConfig();
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
                ModelState.AddModelError("Name", "Valid town name is required.");
                return View("Create");
            }
            var weatherData = await _weatherConnection.GetWeatherDataAsync(Name);

            if(weatherData != null)
            {
                var userId = _userManager.GetUserId(User);

                weatherLog.UserId = userId;
                
                 _weatherConnection.SaveWeatherDataToLog(weatherLog);
                await _dataLogger.AddLogToDbContext(weatherLog);
                return View("Results", weatherData);
            }
            return View("Create");  
        }
        [HttpGet]
        public async Task<IActionResult> PrintLogsForActiveUser(IEnumerable<WeatherLog> weatherLog, int? pageNumber)
        {
            var currentUser = _userManager.GetUserId(User);
            weatherLog = await _dataLogger.GetWeatherLogs();
            var userLogs = _dbContext.WeatherLogs.Where(ul => ul.UserId == currentUser).OrderByDescending(d => d.Date);

            return View("History", PaginatedList<WeatherLog>
                .Create(userLogs.ToList(), pageNumber ?? 1, _paginationConfig.PageSize));

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