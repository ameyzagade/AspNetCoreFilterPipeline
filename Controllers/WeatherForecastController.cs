using AspNetCoreFilterPipeline.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreFilterPipeline.Controllers;

// By default, ControllerActionFilter would have ran before any action filter succeeding it
// (either on controller or on any action methods) here in the before stage of the filter pipeline, 
//however the order has been changed
[ServiceFilter(type: typeof(ControllerActionFilter), Order = 50)]
[ServiceFilter(type: typeof(ControllerAsyncActionFilter))]
[TypeFilter(type: typeof(ControllerActionFilter), Order = 1)]
[LoggingAsyncActionFilter("WeatherForecast")]       // using filter as an attribute
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [ServiceFilter(type: typeof(ActionMethodActionFilter))]
    [ServiceFilter(type: typeof(ActionMethodAsyncActionFilter))]
    [LoggingAsyncActionFilter("GetWeatherForecast")]
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}