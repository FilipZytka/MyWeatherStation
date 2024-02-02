using WeatherStationMVC.Models;

using System.Text.Json;


namespace WeatherStationMVC;

public class WeatherConnection
{
    private readonly string apiID = "e980535cb1f23bdf0d32fb1b033e61d8";
    private readonly string apiUrl = "https://api.openweathermap.org/data/2.5/weather";

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
                return weatherData;
            }

            return null;
        }

    }


}
