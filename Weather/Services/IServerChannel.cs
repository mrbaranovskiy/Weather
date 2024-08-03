using Microsoft.AspNetCore.Hosting.Server;

namespace Weather.Actions;

public interface IServerChannel<ProtoType>
{
    ProtoType Channel { get; }
    Task StartAsync();
    Task StopAsync();
    Task RestartTask();
}

internal class GrpcChannelProxy : IServerChannel<Server>
{
    public Server Channel { get; }

    public GrpcChannelProxy(Server channel)
    {
        Channel = channel ?? throw new ArgumentNullException(nameof(channel));
    }
    
    public Task StartAsync()
    {
        this.Channel.Start();
    }

    public async Task StopAsync()
    {
        await this.Channel.ShutdownAsync();
    }

    public Task RestartTask()
    {
        throw new NotImplementedException();
    }
}