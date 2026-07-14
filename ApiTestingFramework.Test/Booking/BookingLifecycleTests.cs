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
    [Test]
    public async Task BookingLifecycle_Should_Create_Update_And_Delete_Booking()
    {
        // Arrange
        var booking = BookingBuilder
            .Create()
            .Build();

        var authRequest = AuthRequestBuilder
            .Create()
            .Build();

        // Authenticate
        var authResponse = await AuthClient.AuthenticateAsync(authRequest);

        authResponse.ShouldContainToken();

        // Create
        var createResponse = await BookingClient.CreateBookingAsync(booking);

        createResponse.Bookingid.Should().Be(101);
        createResponse.Booking.ShouldBeEquivalentTo(booking);

        // Read
        var createdBooking = await BookingClient.GetBookingAsync(101);

        createdBooking.Should().NotBeNull();

        // Update
        var updatedBooking = BookingBuilder
            .Create()
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
            101,
            updatedBooking,
            authResponse.Token);

        updateResponse.ShouldBeEquivalentTo(updatedBooking);

        // Read again
        var bookingAfterUpdate = await BookingClient.GetBookingAsync(101);

        bookingAfterUpdate.ShouldBeEquivalentTo(updatedBooking);

        // Delete
        await BookingClient.DeleteBookingAsync(
            101,
            authResponse.Token);

        // Verify deleted
        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            BookingClient.GetBookingAsync(101));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}