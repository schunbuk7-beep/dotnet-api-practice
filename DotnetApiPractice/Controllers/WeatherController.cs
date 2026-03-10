using Microsoft.AspNetCore.Mvc;

namespace DotnetApiPractice.Controllers;

[ApiController]
[Route("[Controller]")]
public class WeatherController : ControllerBase
{
   [HttpGet]
   public IActionResult GetWeather()
   {
    var data = new
    {
        City = "Delhi",
        Temperature = "33°C",
        Condition = "Sunny"
    };
    return Ok(data);
    }

    [HttpPost]
   public IActionResult AddWeather([FromBody] object WeatherData)
{
   return Ok(new {Message = "Weather Data received Successfully",Data = WeatherData});
}
}


