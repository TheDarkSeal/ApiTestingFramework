using ApiTestingFramework.Clients;
using ApiTestingFramework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ApiTestingFramework.Tests;

public abstract class TestBase
{
    protected BookingClient BookingClient = null!;
    protected AuthClient AuthClient = null!;


    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddApiFramework();

        var provider = services.BuildServiceProvider();

        BookingClient = provider.GetRequiredService<BookingClient>();
        AuthClient = provider.GetRequiredService<AuthClient>();
    }
}