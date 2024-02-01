using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherStationMVC.Models;

namespace WeatherStationMVC.Controllers;

public class SignUpController : Controller
{
    private readonly ILogger<SignUpController> _logger;

    public SignUpController(ILogger<SignUpController> logger)
    {
        _logger = logger;
    }

 
    public IActionResult SignUp()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}