using ApiTestingFramework.Clients;
using ApiTestingFramework.Models;
using Microsoft.Extensions.Logging;

namespace ApiTestingFramework.Clients;

public class AuthClient : BaseApiClient
{
    public AuthClient(
    HttpClient client,
    ILogger<BaseApiClient> logger)
    : base(client, logger)
    {
    }

    public Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        => PostAsync<AuthResponse>("auth", request);
}