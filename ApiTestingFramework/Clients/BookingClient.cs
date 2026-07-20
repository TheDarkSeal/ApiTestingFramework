using ApiTestingFramework.Models;
using Microsoft.Extensions.Logging;

namespace ApiTestingFramework.Clients;

public class BookingClient : BaseApiClient
{
    private const string BookingEndpoint = "booking";
    private const string CookieHeaderName = "Cookie";
    private const string TokenPrefix = "token=";

    private static string BookingById(int id)
        => $"{BookingEndpoint}/{id}";

    private static string BuildToken(string token)
        => $"{TokenPrefix}{token}";

    public BookingClient(
        HttpClient client,
        ILogger<BaseApiClient> logger)
        : base(client, logger)
    {
    }

    public Task<List<BookingId>> GetBookingsAsync()
        => GetAsync<List<BookingId>>(BookingEndpoint);

    public Task<Booking> GetBookingAsync(int id)
        => GetAsync<Booking>(BookingById(id));

    public Task<CreateBookingResponse> CreateBookingAsync(Booking booking)
        => PostAsync<CreateBookingResponse>(BookingEndpoint, booking);

    public Task<Booking> UpdateBookingAsync(
        int id,
        Booking booking,
        string token)
        => PutAsync<Booking>(
            BookingById(id),
            booking,
            new Dictionary<string, string>
            {
                { CookieHeaderName, BuildToken(token) }
            });

    public Task DeleteBookingAsync(
        int id,
        string token)
        => DeleteAsync(
            BookingById(id),
            new Dictionary<string, string>
            {
                { CookieHeaderName, BuildToken(token) }
            });
}
