namespace ClientsManager.Tests;

public class FailAddClientRepositoryStub : IClientRepository
{
    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        return Task.FromResult(false);
    }
}