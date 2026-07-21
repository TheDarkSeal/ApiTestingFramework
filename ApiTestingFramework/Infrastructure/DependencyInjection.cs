using ApiTestingFramework.Clients;
using ApiTestingFramework.Configuration;
using ApiTestingFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace ApiTestingFramework.Infrastructure;

public static class DependencyInjection
{
    private const string JsonMediaType = "application/json";

    public static IServiceCollection AddApiFramework(
        this IServiceCollection services)
    {
        var baseUrl = ConfigurationManager.Configuration["ApiSettings:BaseUrl"];

        ValidateBaseUrl(baseUrl);

        services.AddHttpClient<AuthClient>(client =>
            ConfigureClient(client, baseUrl!));

        services.AddHttpClient<BookingClient>(client =>
            ConfigureClient(client, baseUrl!));

        services.AddSingleton(LoggerSetup.CreateLoggerFactory());
        services.AddLogging();

        return services;
    }

    private static void ValidateBaseUrl(string? baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new InvalidOperationException(
                "ApiSettings:BaseUrl is missing or empty in configuration.");
        }

        if (!Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute))
        {
            throw new InvalidOperationException(
                $"ApiSettings:BaseUrl is not a valid absolute URI: {baseUrl}");
        }
    }

    private static void ConfigureClient(HttpClient client, string baseUrl)
    {
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue(JsonMediaType));
    }
}
