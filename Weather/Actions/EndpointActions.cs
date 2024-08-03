using Microsoft.AspNetCore.Mvc;
using Weather.Endpoint;

namespace Weather.Actions;

public class EndpointActions
{
    public static IResult NewServer(
        EndpointCreationService ecService,
        EndpointStateService stateService,
        EndpointCacheService cacheService, [FromQuery] int port)
    {
        if (cacheService.GetServer(port) is { })
            Results.Problem("Already allocated endpoint");
        
        var endpoint = ecService.BuildServer(port);
        cacheService.AddServer(port, endpoint);
        stateService.AddEndpoint(port, EndpointState.Created);
        
        
        return Results.Ok($"Endpoint allocated {port}");
    }

    public static async Task<IResult> StartServer(
        EndpointCacheService cache,
        EndpointStateService stateService,
        EndpointCreationService ecService,
        ILogger<EndpointActions> logger, [FromQuery] int port)
    {
        logger.LogInformation("StartServer called");

        if (stateService.GetState(port) is EndpointState.Stopped or EndpointState.Failed)
        {
            logger.LogInformation($"Restarting server on port {port}");
            var removed = cache.RemoveServer(port);
            if (removed is { })
            {
                try
                {
                    await removed.ShutdownAsync();
                }
                catch (Exception e) { }
            }
         
            var endpoint = ecService.BuildServer(port);
            cache.AddServer(port, endpoint);
            endpoint.Start();
            logger.LogInformation($"Server started on port {port}");
            stateService.SetState(port, EndpointState.Running);
            
            return Results.Ok($"Server restarted on port {port}");
        }

        else if (cache.GetServer(port) is {} server)
        {
            logger.LogInformation($"Starting server on port {port}");
            server.Start();
            stateService.SetState(port, EndpointState.Running);
            logger.LogInformation("Server started.");
            return Results.Ok($"Server started. {port}");
        }
        else
        {
            return Results.Problem("Server not found.");
        }
    }

    public static IResult ListServers(EndpointCacheService cache, EndpointStateService stateService)
    {
        var lst = cache.GetServers().Select(port=> $"{port} is {stateService.GetState(port)}").ToList();
        return Results.Content(string.Join("\n",lst));
    }

    public static async Task<IResult> StopServer(
        EndpointCacheService cache,
        EndpointStateService stateService,
        ILogger<EndpointActions> logger, [FromQuery] int port)
    {
        logger.LogInformation("StopServer called");

        if (cache.GetServer(port) is { } server)
        {
            stateService.SetState(port, EndpointState.Stopped);

            logger.LogInformation($"Stopping server on port {port}");
            await server.ShutdownAsync();
            logger.LogInformation("Server stopped.");
            return Results.Ok($"Server stopped. {port}");
        }
        
        return Results.Problem("Server not found.");
    }
}