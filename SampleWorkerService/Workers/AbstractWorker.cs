using Microsoft.Extensions.Options;
using SampleWorkerService.Configurations;

namespace SampleWorkerService.Workers;

public class AbstractWorker : BackgroundService
{
    private readonly IOptions<MainConfig> _mainConfigOptions;
    private readonly ILogger _logger;

    protected AbstractWorker(IOptions<MainConfig> mainConfigOptions, ILogger logger)
    {
        _mainConfigOptions = mainConfigOptions;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var mainConfig = _mainConfigOptions.Value;
        _logger.LogInformation($"Starting worker {GetType().Name} as service {mainConfig.ServiceName}. Configuration: ValidFrom: {mainConfig.ValidFrom}, IsEnabled: {mainConfig.IsEnabled}");
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Stopping worker {GetType().Name}.");
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Worker {GetType().Name} running at: {DateTimeOffset.Now}");
            await Task.Delay(_mainConfigOptions.Value.Workers[GetType().Name].Timeout, stoppingToken);
        }
    }
}