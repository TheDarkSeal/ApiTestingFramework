using ApiTestingFramework.Exceptions;
using Polly;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ApiTestingFramework.Clients;

public abstract class BaseApiClient
{
    protected readonly HttpClient Client;
    private readonly ILogger<BaseApiClient> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

    protected BaseApiClient(HttpClient client, ILogger<BaseApiClient> logger)
    {
        Client = client;
        _logger = logger;

        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r =>
                r.StatusCode == HttpStatusCode.RequestTimeout ||
                r.StatusCode == HttpStatusCode.TooManyRequests ||
                (int)r.StatusCode >= 500)
            .WaitAndRetryAsync(
                3,
                retry => TimeSpan.FromSeconds(retry));
    }

    protected Task<T> GetAsync<T>(string endpoint)
        => SendAsync<T>(HttpMethod.Get, endpoint);

    protected Task<TResponse> PostAsync<TResponse>(
        string endpoint,
        object body)
        => SendAsync<TResponse>(HttpMethod.Post, endpoint, body);

    protected Task<TResponse> PutAsync<TResponse>(
        string endpoint,
        object body,
        Dictionary<string, string>? headers = null)
        => SendAsync<TResponse>(HttpMethod.Put, endpoint, body, headers);

    protected Task DeleteAsync(
        string endpoint,
        Dictionary<string, string>? headers = null)
        => SendWithoutResponseAsync(HttpMethod.Delete, endpoint, headers: headers);

    private async Task<T> SendAsync<T>(
        HttpMethod method,
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        using var response = await SendRequestAsync(method, endpoint, body, headers);

        var content = await response.Content.ReadAsStringAsync();

        _logger.LogInformation(
    "Sending {Method} request to {Endpoint}",
    method,
    endpoint);

        _logger.LogInformation(
    "Response received. StatusCode: {StatusCode}",
    response.StatusCode);

        _logger.LogDebug(
    "Response body: {ResponseBody}",
    content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException(
                response.StatusCode,
                content);
        }

        return JsonSerializer.Deserialize<T>(content, JsonOptions)!;
    }

    private async Task SendWithoutResponseAsync(
        HttpMethod method,
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        using var response = await SendRequestAsync(method, endpoint, body, headers);

        var content = await response.Content.ReadAsStringAsync();

        _logger.LogInformation(
    "Sending {Method} request to {Endpoint}",
    method,
    endpoint);

        _logger.LogInformation(
    "Response received. StatusCode: {StatusCode}",
    response.StatusCode);

        _logger.LogDebug(
    "Response body: {ResponseBody}",
    content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException(
                response.StatusCode,
                content);
        }
    }

    private Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method,
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        return _retryPolicy.ExecuteAsync(async () =>
        {
            using var request = new HttpRequestMessage(method, endpoint);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (body != null)
            {
                var json = JsonSerializer.Serialize(body, JsonOptions);

                _logger.LogDebug(
    "Request body: {RequestBody}",
    json);

                request.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }

            return await Client.SendAsync(request);
        });
    }
}