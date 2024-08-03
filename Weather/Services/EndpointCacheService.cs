using System.Collections.Concurrent;

namespace Weather.Actions;

public sealed class EndpointCacheService
{
    private readonly ConcurrentDictionary<int, Server> _endpoints = new();
    public void AddServer(int port, Server server)
    {
        _endpoints.AddOrUpdate(port, server, (key, old) => server);
    }

    public Server? GetServer(int port)
    {
        return _endpoints.TryGetValue(port, out var server) ? server : null;
    }

    public Server RemoveServer(int port)
    {
        _endpoints.Remove(port, out var server);
        return server;
    }

    public List<int> GetServers()
    {
        if (_endpoints.IsEmpty) return new List<int>();
        return _endpoints.Keys.ToList();
    }
}