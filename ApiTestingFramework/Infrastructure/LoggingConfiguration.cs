using Serilog;

namespace ApiTestingFramework.Infrastructure;

public static class LoggingConfiguration
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
    }
}