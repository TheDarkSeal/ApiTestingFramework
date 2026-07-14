using Bogus;

namespace ApiTestingFramework.Builders;

public class BookingBuilder
{
    private static readonly Faker Faker = new();

    private readonly Booking _booking;

    private BookingBuilder()
    {
        _booking = new Booking
        {
            Firstname = "John",
            Lastname = "Smith",
            Totalprice = 150,
            Depositpaid = true,
            Bookingdates = new BookingDates
            {
                Checkin = "2026-08-01",
                Checkout = "2026-08-07"
            },
            Additionalneeds = "Breakfast"
        };
    }

    public static BookingBuilder Create()
    {
        return new BookingBuilder();
    }

    public BookingBuilder WithRandomData()
    {
        var checkin = Faker.Date.Future();
        var checkout = checkin.AddDays(Faker.Random.Int(1, 14));

        _booking.Firstname = Faker.Name.FirstName();
        _booking.Lastname = Faker.Name.LastName();
        _booking.Totalprice = Faker.Random.Int(50, 1000);
        _booking.Depositpaid = Faker.Random.Bool();
        _booking.Bookingdates = new BookingDates
        {
            Checkin = checkin.ToString("yyyy-MM-dd"),
            Checkout = checkout.ToString("yyyy-MM-dd")
        };
        _booking.Additionalneeds = Faker.PickRandom(
            "Breakfast",
            "Dinner",
            "Lunch",
            "Late Checkout",
            "Baby Crib",
            "Airport Transfer",
            "None");

        return this;
    }

    public BookingBuilder WithFirstname(string firstname)
    {
        _booking.Firstname = firstname;
        return this;
    }

    public BookingBuilder WithLastname(string lastname)
    {
        _booking.Lastname = lastname;
        return this;
    }

    public BookingBuilder WithTotalPrice(int totalPrice)
    {
        _booking.Totalprice = totalPrice;
        return this;
    }

    public BookingBuilder WithDepositPaid(bool depositPaid)
    {
        _booking.Depositpaid = depositPaid;
        return this;
    }

    public BookingBuilder WithCheckin(DateTime checkin)
    {
        _booking.Bookingdates.Checkin = checkin.ToString("yyyy-MM-dd");
        return this;
    }

    public BookingBuilder WithCheckout(DateTime checkout)
    {
        _booking.Bookingdates.Checkout = checkout.ToString("yyyy-MM-dd");
        return this;
    }

    public BookingBuilder WithBookingDates(DateTime checkin, DateTime checkout)
    {
        _booking.Bookingdates = new BookingDates
        {
            Checkin = checkin.ToString("yyyy-MM-dd"),
            Checkout = checkout.ToString("yyyy-MM-dd")
        };

        return this;
    }

    public BookingBuilder WithAdditionalNeeds(string additionalNeeds)
    {
        _booking.Additionalneeds = additionalNeeds;
        return this;
    }

    public BookingBuilder WithoutAdditionalNeeds()
    {
        _booking.Additionalneeds = string.Empty;
        return this;
    }

    public Booking Build()
    {
        return _booking;
    }
}