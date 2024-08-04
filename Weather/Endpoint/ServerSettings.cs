namespace Weather.Endpoint;

 [Serializable]
    public class ServerSettings
    {
        /// <summary>Name for server (partner integration)</summary>
        public string Name { get; set; }

        /// <summary> Port </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sents the time in minutes token to be expired.
        /// </summary>
        public int TokenExpirationTimeSeconds { get; set; }

        /// <summary> Gets or set the token automatic refreshing </summary>
        public bool AllowTokenRefresh { get; set; }

        /// <summary> OAuth port </summary>
        public int OAuthPort { get; set; }

        /// <summary> Use Https </summary>
        public bool UseHttps { get; set; }

        /// <summary>Maximum length of messages the server can receive</summary>
        public int MaxMessageSize { get; set; }

        /// <summary>List of major versions supported by the server</summary>
        public List<int> SupportedMajorVersions { get; } = new();

        /// <summary>Minor version of the server</summary>
        public int? CurrentMinorVersion { get; set; }

        /// <summary> Job request timeout in s </summary>
        public int JobRequestTimeout { get; set; }

        /// <summary> PIC custom name </summary>
        public string PicCustomName { get; set; }

        /// <summary> Udp discoverable </summary>
        public bool IsUdpDiscoverable { get; set; } = true;

        /// <summary> Use cloud authentication </summary>
        public bool UseCloudAuthentication { get; set; }

        /// <summary> Requires policy to connect </summary>
        public bool RequirePolicy { get; set; }

        /// <summary> Pic job simulation behavior settings </summary>
       // public PicJobSimulationBehaviorSettings PicJobSimulationBehaviorSettings { get; set; }

        /// <summary> PIC identifier </summary>
        public string PicIdentifier { get; set; }

        /// <summary> Machine identifiers </summary>
        //public List<SettingsMachineId> MachineIds { get; set; }

        /// <summary> Indicates if server should be launched right after it was created. </summary>
        public bool StartOnCreation { get; set; }

        /// <summary>Needed Data</summary>
        public GetNeededDataResponse NeededData { get; } = new();
    }
    
    /// <summary> Test server settings </summary>
    [Serializable]
    public class TestServerSettings
    {
        /// <summary> Start music always </summary>
        public bool PlayAlways { get; set; } = false;
        /// <summary> Play on start </summary>
        public bool PlayOnStart { get; set; } = true;
    }

    /// <summary>Machine Id</summary>
    public class SettingsMachineId
    {
        /// <summary>ctor</summary>
        public SettingsMachineId() { }

        /// <summary>ctor</summary>
        public SettingsMachineId(string machineModelId, string manufacturerId, string customName = null)
        {
            MachineModelId = machineModelId;
            ManufacturerId = manufacturerId;
            CustomName = customName;
        }

        /// <summary>Machine Model Id</summary>
        public string MachineModelId { get; set; }
        /// <summary>Manufacturer Id</summary>
        public string ManufacturerId { get; set; }
        /// <summary>Custom name</summary>
        public string CustomName { get; set; }
    }

    /// <summary> PicJobSimulationBehaviorSettings </summary>
    public class PicJobSimulationBehaviorSettings
    {
        /// <summary> Send job successfully received via job status protocol </summary>
        public bool SendJobReceivedConfirmation { get; set; } = true;

        /// <summary> Job simulation duration </summary>
        public int JobSimulationDuration { get; set; } = 10;
    }
    }