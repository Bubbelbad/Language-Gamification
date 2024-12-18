using Domain.Entities;
using MediatR;

namespace Application.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecast>>
    {
        public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            IEnumerable<WeatherForecast> forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Task.FromResult(forecast); 
        }
    }
}
