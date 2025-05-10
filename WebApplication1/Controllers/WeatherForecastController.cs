using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILoggingQueue _logQueue) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet(Name = "GetWeather")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();


            _logQueue.Enqueue(new LogEntry
            {
                Message = "Weather data fetched successfully",
                Level = "Info",
                Source = "WeatherForecastController"
            });


            await Task.CompletedTask;

            return result;
        }

    }

}
