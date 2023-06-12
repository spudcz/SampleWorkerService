using Microsoft.Extensions.Options;
using SampleWorkerService.Configurations;

namespace SampleWorkerService.Workers;

public class Worker2 : AbstractWorker
{
    public Worker2(IOptions<MainConfig> mainConfigOptions, ILogger<Worker2> logger) : base(mainConfigOptions, logger) { }
}