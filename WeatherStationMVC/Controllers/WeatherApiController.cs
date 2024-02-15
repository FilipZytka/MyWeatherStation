using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherStation.Entity;
using WeatherStationMVC.Models;

namespace WeatherStationMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherApiController : ControllerBase
    {
    
        private readonly WeatherConnection _weatherConnection;

        public WeatherApiController()
        {
           
            _weatherConnection = new WeatherConnection();
        }
        
        [HttpGet]
        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
        return await _weatherConnection.GetWeatherDataAsync(city);
        }
        
    }
}