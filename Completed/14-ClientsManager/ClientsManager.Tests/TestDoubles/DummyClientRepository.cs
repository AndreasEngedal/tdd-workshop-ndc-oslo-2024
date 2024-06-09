namespace ClientsManager.Tests;

public class DummyClientRepository : IClientRepository
{
    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        return Task.FromResult(true);
    }
}