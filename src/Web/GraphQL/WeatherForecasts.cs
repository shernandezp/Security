using Security.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace Security.Web.GraphQL;

[AuthorizeQL]
public class WeatherForecasts
{
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts([Service] ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
