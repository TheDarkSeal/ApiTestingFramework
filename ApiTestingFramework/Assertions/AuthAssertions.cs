using ApiTestingFramework.Models;
using FluentAssertions;

namespace ApiTestingFramework.Tests.Assertions;

public static class AuthAssertions
{
    public static void ShouldBeEquivalentTo(
        this AuthRequest actual,
        AuthRequest expected)
    {
        actual.Should().BeEquivalentTo(expected);
    }
    public static void ShouldContainToken(
        this AuthResponse actual)
    {
        actual.Should().NotBeNull();
        actual.Token.Should().NotBeNullOrWhiteSpace();
    }

    public static void ShouldHaveToken(
        this AuthResponse actual,
        string expectedToken)
    {
        actual.Should().NotBeNull();
        actual.Token.Should().Be(expectedToken);
    }
}