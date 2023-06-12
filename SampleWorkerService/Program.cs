using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using SampleWorkerService.Configurations;
using SampleWorkerService.Workers;

// set base directory
Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

// create initial NLog logger
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureLogging((context, logging) => {
            logging.ClearProviders();
            logging.AddNLog(context.Configuration, new NLogProviderOptions());
        })
        .ConfigureAppConfiguration((context, builder) => {
            builder.AddCommandLine(args);
        })
        .ConfigureServices((context, services) =>
        {
            // register configuration
            services.Configure<MainConfig>(context.Configuration);

            var mainConfig = context.Configuration.Get<MainConfig>() ?? throw new InvalidOperationException("Failed to load config.");

            // register workers
            switch (mainConfig.ServiceName)
            {
                case "Worker1":
                    services.AddHostedService<Worker1>();
                    break;
                case "Worker2":
                    services.AddHostedService<Worker2>();
                    break;
                default:
                    services.AddHostedService<Worker1>();
                    services.AddHostedService<Worker2>();
                    break;
            }
        })
        .UseWindowsService()
        .Build();

    await host.RunAsync();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}