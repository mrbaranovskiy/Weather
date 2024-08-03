using Weather.Endpoint;

namespace Weather.Actions;

public sealed class EndpointCreationService
{
    private readonly EndpointCacheService _cache;

    public EndpointCreationService(EndpointCacheService cache)
    {
        _cache = cache;
    }
    
    public Server BuildServer(int port)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(port);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(port, 1 << 16, nameof(port));
        
        var server = new Server
        {
            Services = { WeatherService.BindService(new WeatherCheckService()) },
            Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
        };
        
        _cache.AddServer(port, server);

        return server;
    }
}

public sealed class EndpointStateService
{
    private readonly Dictionary<int, EndpointState> _endpoints = new();

    public void AddEndpoint(int port, EndpointState state)
    {
        if(_endpoints.ContainsKey(port))
            throw new InvalidOperationException("Endpoint already added");
        
        _endpoints.Add(port, state);
    }

    public EndpointState GetState(int port)
    {
        if (_endpoints.TryGetValue(port, out var state))
        {
            return state;
        }

        return EndpointState.NotCreated;
    }
    
    public void SetState(int port, EndpointState state)
    {
        if(!_endpoints.ContainsKey(port))
            throw new InvalidOperationException("No endpoint available");
        
        _endpoints[port] = state;
    }
}