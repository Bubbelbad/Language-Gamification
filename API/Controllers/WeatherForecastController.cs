using Application.Queries.GetWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator; 
        private readonly ILogger<WeatherForecastController> _logger = logger;


        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var weatherForecast = await _mediator.Send(new GetWeatherForecastQuery());
            return Ok(weatherForecast); 
        }
    }
}
