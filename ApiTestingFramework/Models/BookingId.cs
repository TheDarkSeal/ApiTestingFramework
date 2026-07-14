using System.Text.Json.Serialization;

public class BookingId
{
    [JsonPropertyName("bookingid")]
    public int Bookingid { get; set; }
}