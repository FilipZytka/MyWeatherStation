using System.ComponentModel.DataAnnotations.Schema;
using WeatherStation.Entity;
using Microsoft.AspNetCore.Identity;

namespace WeatherStation.Entity;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public string FirstName  { get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }
    public List <WeatherLog> WeatherLogs { get; set; }

}
