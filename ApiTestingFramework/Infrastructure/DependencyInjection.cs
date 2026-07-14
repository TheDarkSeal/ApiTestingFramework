using ApiTestingFramework.Clients;
using ApiTestingFramework.Configuration;
using ApiTestingFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace ApiTestingFramework.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiFramework(
        this IServiceCollection services)
    {
        var baseUrl =
            ConfigurationManager.Configuration["ApiSettings:BaseUrl"];

        services.AddHttpClient<AuthClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl!);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));
        });


        services.AddHttpClient<BookingClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl!);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));
        });

        services.AddSingleton(
    LoggerSetup.CreateLoggerFactory());

        services.AddLogging();


        return services;
    }
}