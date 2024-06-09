namespace ClientsManager.Tests;

public class SuccessfulAddClientRepositoryStub : IClientRepository
{
    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        return Task.FromResult(true);
    }
}