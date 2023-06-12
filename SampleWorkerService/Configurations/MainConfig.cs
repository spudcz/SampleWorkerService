namespace SampleWorkerService.Configurations;

public class MainConfig
{
    public Dictionary<string, string> ConnectionStrings { get; set; } = null!;
    public DateTime ValidFrom { get; set; }
    public bool IsEnabled { get; set; }
    public Dictionary<string, WorkerConfig> Workers { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
}

public class WorkerConfig
{
    public int Timeout { get; set; }
}