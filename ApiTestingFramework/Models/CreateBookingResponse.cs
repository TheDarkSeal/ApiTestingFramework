namespace ApiTestingFramework.Models;

using System.Text.Json.Serialization;

public class CreateBookingResponse
{
    [JsonPropertyName("bookingid")]
    public int Bookingid { get; set; }

    [JsonPropertyName("booking")]
    public Booking Booking { get; set; }
}