using WeatherStationMVC.Models;
using System.Text.Json;
using WeatherStation.Entity;

namespace WeatherStationMVC;

public class WeatherConnection
{
    private readonly string apiID = "e980535cb1f23bdf0d32fb1b033e61d8";
    private readonly string apiUrl = "https://api.openweathermap.org/data/2.5/weather";
    private WeatherData _weatherData;

    public async Task<WeatherData?> GetWeatherDataAsync(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"{apiUrl}?q={city}&appid={apiID}&units=metric";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string weatherApiResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherData>(weatherApiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                _weatherData = weatherData;
                return _weatherData;
            }
            return null;
        }
    }
    public WeatherLog SaveWeatherDataToLog(WeatherLog weatherLog)
    {
        weatherLog.Temperature = _weatherData.Main.Temp;
        weatherLog.Humidity = _weatherData.Main.Humidity;
        weatherLog.Pressure = _weatherData.Main.Pressure;
        weatherLog.WindSpeed = _weatherData.Wind.Speed;
        weatherLog.Country = _weatherData.Sys.Country;
        weatherLog.WeatherName = _weatherData.Weather[0].Main;
        weatherLog.City = _weatherData.Name;
       
        return weatherLog;

    }


}
