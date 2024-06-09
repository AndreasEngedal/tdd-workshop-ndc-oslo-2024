using FluentAssertions;
using Microsoft.Extensions.Time.Testing;

namespace ClientManager.Tests;

public class ClientManagerScenarios
{
    [Fact]
    public void When_today_is_sunday_Then_throws_exception()
    {
        var sundayDateTime = new DateTimeOffset(2024, 1, 7, 0, 0, 0, TimeSpan.Zero);
        var timeProvider = new FakeTimeProvider(sundayDateTime);
        var manager = new ClntMngr(timeProvider);
        
        Assert.Throws<Exception>(() => manager.AddClnt("Gui", "gui@gui", "testing-sunday.txt"));
    }
    
    [Fact]
    public void When_clients_registry_is_provided_then_adds_client()
    {
        var clientsRegistry = new ClientRegistrySpy();
        var manager = new ClntMngr();
        
        manager.AddClient("Gui", "gui@gui", clientsRegistry);

        clientsRegistry.NumberOfClientsAdded.Should().Be(1);
    }
}