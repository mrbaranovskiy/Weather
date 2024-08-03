namespace Weather.Services;

public class WeatherCheckService : Weather.WeatherService.WeatherServiceBase
{
    public override Task<WeatherItemResponce> Ping(WeatherItemRequest request, ServerCallContext context)
    {
        return Task.FromResult(new WeatherItemResponce() { Temp = 42 });
    }
}