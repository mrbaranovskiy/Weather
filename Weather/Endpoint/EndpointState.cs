namespace Weather.Endpoint;

public enum EndpointState
{
    NotCreated = 0,
    Stopped = 1,
    Running = 2,
    Failed = 3,
    Created = 4
}