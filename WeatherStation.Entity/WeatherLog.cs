
using System.Net;

namespace WeatherStation.Entity
{
    public class WeatherLog
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now;
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int All { get; set; }
        public int Visibility { get; set; }
        public double WindSpeed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
        


        
    }
}
