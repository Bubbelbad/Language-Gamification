using Domain.Entities;
using MediatR; 

namespace Application.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQuery() : IRequest<IEnumerable<WeatherForecast>>
    {
        // Implement querys with params here
    }
}
