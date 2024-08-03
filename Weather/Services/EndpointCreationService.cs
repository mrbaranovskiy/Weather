using Microsoft.AspNetCore.Hosting.Server;

namespace Weather.Actions;

internal sealed class EndpointCreationService
{
    private readonly EndpointCacheService _cache;

    public EndpointCreationService(EndpointCacheService cache)
    {
        _cache = cache;
    }
    
    public IServerChannel<Server> CreateEndpoint(int port)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(port);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, 1 << 16, nameof(port));
        
        var server = new Server
        {
            Services = { WeatherService.BindService(new ServerItemService()) },
            Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
        };
        
        _cache.AddServer(port, server);

        return new GrpcChannelProxy(server);
    }
}