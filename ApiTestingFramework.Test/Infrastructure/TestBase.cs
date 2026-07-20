using ApiTestingFramework.Clients;
using ApiTestingFramework.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ApiTestingFramework.Tests;

public abstract class TestBase : IDisposable
{
    private ServiceProvider _provider = null!;

    protected BookingClient BookingClient = null!;
    protected AuthClient AuthClient = null!;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddApiFramework();

        _provider = services.BuildServiceProvider();

        BookingClient = _provider.GetRequiredService<BookingClient>();
        AuthClient = _provider.GetRequiredService<AuthClient>();
    }

    public void Dispose()
    {
        _provider?.Dispose();
    }
}
