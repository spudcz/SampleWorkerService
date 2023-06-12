using Microsoft.Extensions.Options;
using SampleWorkerService.Configurations;

namespace SampleWorkerService.Workers
{
    public class Worker1 : AbstractWorker
    {
        public Worker1(IOptions<MainConfig> mainConfigOptions, ILogger<Worker1> logger) : base(mainConfigOptions, logger) { }
    }
}