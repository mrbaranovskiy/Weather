namespace Weather.Endpoint;

[Serializable]
public record ServerSetting
{
    public required string Name { get; init; }
    public required int Port { get; init; }
    public IReadOnlyCollection<MachineIdentifier> Machines { get; init; }
        = Array.Empty<MachineIdentifier>();
}