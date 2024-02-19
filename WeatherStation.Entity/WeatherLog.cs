using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace WeatherStation.Entity
{
    public class WeatherLog
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string WeatherName {  get; set; }
        public string Country { get; set; }
        public string City { get; set; }






    }
}
