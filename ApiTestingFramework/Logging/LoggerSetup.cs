using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiTestingFramework.Logging;

public static class LoggerSetup
{
    public static ILoggerFactory CreateLoggerFactory()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        return LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });
    }
}