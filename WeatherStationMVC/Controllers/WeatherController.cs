using Microsoft.AspNetCore.Mvc;
using WeatherStationMVC.Models;

namespace WeatherStationMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly string ApiID = "e980535cb1f23bdf0d32fb1b033e61d8";
    private readonly WeatherConnection weatherConnection;
    public WeatherController()
    {
        weatherConnection = new WeatherConnection(ApiID);
    }
    [HttpGet]
    public async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        return await weatherConnection.GetWeatherDataAsync(city);
    }
    
    [HttpPost]
    public void Post([FromBody] string value)
    {

    }

 

}
