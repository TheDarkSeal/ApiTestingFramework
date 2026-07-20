using ApiTestingFramework.Builders;
using ApiTestingFramework.Clients;
using ApiTestingFramework.Exceptions;
using ApiTestingFramework.Tests.Assertions;
using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace ApiTestingFramework.Tests.Bookings;

public class BookingLifecycleTests : TestBase
{
    private async Task<string> AuthenticateAsync()
    {
        var authRequest = AuthRequestBuilder.Create().Build();
        var authResponse = await AuthClient.AuthenticateAsync(authRequest);

        authResponse.ShouldContainToken();
        return authResponse.Token;
    }

    [Test]
    public async Task Should_Create_Booking()
    {
        var booking = BookingBuilder.Create().Build();

        var response = await BookingClient.CreateBookingAsync(booking);

        response.Bookingid.Should().BeGreaterThan(0);
        response.Booking.Should().BeEquivalentTo(booking);
    }

    [Test]
    public async Task Should_Read_Booking()
    {
        var booking = BookingBuilder.Create().Build();
        var createResponse = await BookingClient.CreateBookingAsync(booking);

        var created = await BookingClient.GetBookingAsync(createResponse.Bookingid);

        created.Should().NotBeNull();
        created.Should().BeEquivalentTo(booking);
    }

    [Test]
    public async Task Should_Update_Booking()
    {
        var token = await AuthenticateAsync();

        var original = BookingBuilder.Create().Build();
        var createResponse = await BookingClient.CreateBookingAsync(original);

        var updated = BookingBuilder.Create()
            .WithFirstname("Updated")
            .WithLastname("User")
            .WithTotalPrice(500)
            .WithDepositPaid(false)
            .WithAdditionalNeeds("Dinner")
            .WithBookingDates(
                new DateTime(2026, 9, 1),
                new DateTime(2026, 9, 10))
            .Build();

        var updateResponse = await BookingClient.UpdateBookingAsync(
            createResponse.Bookingid,
            updated,
            token);

        updateResponse.Should().BeEquivalentTo(updated);

        var readBack = await BookingClient.GetBookingAsync(createResponse.Bookingid);
        readBack.Should().BeEquivalentTo(updated);
    }

    [Test]
    public async Task Should_Delete_Booking()
    {
        var token = await AuthenticateAsync();

        var booking = BookingBuilder.Create().Build();
        var createResponse = await BookingClient.CreateBookingAsync(booking);

        await BookingClient.DeleteBookingAsync(createResponse.Bookingid, token);

        var ex = await Assert.ThrowsAsync<ApiException>(() =>
            BookingClient.GetBookingAsync(createResponse.Bookingid));

        ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}