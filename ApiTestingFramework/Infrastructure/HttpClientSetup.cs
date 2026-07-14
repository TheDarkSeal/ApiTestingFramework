using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;


namespace ApiTestingFramework.Infrastructure;


public static class HttpClientSetup
{

    public static IServiceCollection AddApiClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddHttpClient(
            "BookingApi",
            client =>
            {
                var baseUrl =
                    configuration["ApiSettings:BaseUrl"];


                client.BaseAddress =
                    new Uri(baseUrl);


                client.DefaultRequestHeaders
                    .Accept
                    .Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
                        "application/json"));
            })
            .AddPolicyHandler(
                GetRetryPolicy());


        return services;
    }



    private static IAsyncPolicy<HttpResponseMessage>
        GetRetryPolicy()
    {

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                3,
                retry =>
                    TimeSpan.FromSeconds(retry));
    }
}