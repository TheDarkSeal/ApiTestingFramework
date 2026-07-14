using FluentAssertions;

namespace ApiTestingFramework.Tests.Assertions;

public static class BookingAssertions
{
    public static void ShouldBeEquivalentTo(
        this Booking actual,
        Booking expected)
    {
        actual.Should().BeEquivalentTo(expected);
    }
}