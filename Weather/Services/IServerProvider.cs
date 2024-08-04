using Weather.Endpoint;

namespace Weather.Actions;

internal interface IServerProvider
{
    public ServerItem CreateServer();
}

internal sealed class ServerItem
{
    private readonly EndpointCreationService _ecService;
    private readonly ServerSettings _settingses;
    private IServerChannel<Server>? _channel;

    //IChannel<WeatherItem> item;
    public ServerItem(EndpointCreationService ecService,
        ServerSettings settingses)
    {
        _ecService = ecService;
        _settingses = settingses;
    }

    public void Start()
    {
        _channel = _ecService.CreateEndpoint(_settingses.Port);
    }

    public async Task Stop()
    {
        await _channel?.StopAsync();
    }

    public void Update(ServerSettings settingses)
    {
    }

}