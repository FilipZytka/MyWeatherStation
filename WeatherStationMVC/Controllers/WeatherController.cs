using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherStationMVC.Models;
namespace WeatherStationMVC.Controllers
{ 
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherConnection _weatherConnection;
        private string _name;

        
        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
            _weatherConnection = new WeatherConnection();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string Name)
        {
            _name = Name;
            if (string.IsNullOrEmpty(_name))
            {
                ModelState.AddModelError("Name", "City name is required.");
                return View();
            }
            _name = Name;
            WeatherData weatherData = await _weatherConnection.GetWeatherDataAsync(_name);


            return View("Results", weatherData);
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