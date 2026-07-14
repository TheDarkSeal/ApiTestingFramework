using System.Net;

namespace ApiTestingFramework.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public string ResponseBody { get; }

    public ApiException(
        HttpStatusCode statusCode,
        string responseBody)
        : base($"API request failed. Status code: {(int)statusCode} {statusCode}")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}