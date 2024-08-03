using Weather.Endpoint;

namespace Weather.Actions;

internal interface IServerProvider
{
    public ServerItem CreateServer();
}

internal sealed class ServerItem
{
    private readonly EndpointCreationService _ecService;
    private readonly ServerSetting _settings;
    private IServerChannel<Server>? _channel;

    //IChannel<WeatherItem> item;
    public ServerItem(EndpointCreationService ecService,
        ServerSetting settings)
    {
        _ecService = ecService;
        _settings = settings;
    }

    public void Start()
    {
        _channel = _ecService.CreateEndpoint(_settings.Port);
    }

    public async Task Stop()
    {
        await _channel?.StopAsync();
    }

    public void Update(ServerSetting settings)
    {
    }

}