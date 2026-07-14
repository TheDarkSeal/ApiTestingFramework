
using Microsoft.Extensions.Logging;

namespace ApiTestingFramework.Clients;

public class BookingClient : BaseApiClient
{
    public BookingClient(
    HttpClient client,
    ILogger<BaseApiClient> logger)
    : base(client, logger)
    {
    }

    public Task<List<BookingId>> GetBookingsAsync()
        => GetAsync<List<BookingId>>("booking");

    public Task<Booking> GetBookingAsync(int id)
        => GetAsync<Booking>($"booking/{id}");

    public Task<CreateBookingResponse> CreateBookingAsync(Booking booking)
        => PostAsync<CreateBookingResponse>("booking", booking);

    public Task<Booking> UpdateBookingAsync(
        int id,
        Booking booking,
        string token)
        => PutAsync<Booking>(
            $"booking/{id}",
            booking,
            new Dictionary<string, string>
            {
                { "Cookie", $"token={token}" }
            });

    public Task DeleteBookingAsync(
        int id,
        string token)
        => DeleteAsync(
            $"booking/{id}",
            new Dictionary<string, string>
            {
                { "Cookie", $"token={token}" }
            });
}