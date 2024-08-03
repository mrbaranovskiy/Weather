using Weather.Endpoint;

namespace Weather.Actions;

internal sealed class EndpointStateService
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