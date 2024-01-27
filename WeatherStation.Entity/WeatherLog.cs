
using System.Net;

namespace WeatherStation.Entity
{
    public class WeatherLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Temperature { get; set; }

        
    }
}
