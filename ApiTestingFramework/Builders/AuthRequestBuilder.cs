
using ApiTestingFramework.Models;

namespace ApiTestingFramework.Builders;

public class AuthRequestBuilder
{
    private readonly AuthRequest _request;

    private AuthRequestBuilder()
    {
        _request = new AuthRequest
        {
            Username = "admin",
            Password = "password123"
        };
    }

    public static AuthRequestBuilder Create()
    {
        return new AuthRequestBuilder();
    }

    public AuthRequestBuilder WithUsername(string username)
    {
        _request.Username = username;
        return this;
    }

    public AuthRequestBuilder WithPassword(string password)
    {
        _request.Password = password;
        return this;
    }

    public AuthRequestBuilder WithCredentials(string username, string password)
    {
        _request.Username = username;
        _request.Password = password;

        return this;
    }

    public AuthRequestBuilder WithInvalidCredentials()
    {
        _request.Username = "invalidUser";
        _request.Password = "invalidPassword";

        return this;
    }

    public AuthRequest Build()
    {
        return _request;
    }
}