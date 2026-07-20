namespace ApiTestingFramework.Models;

using System.Text.Json.Serialization;

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}