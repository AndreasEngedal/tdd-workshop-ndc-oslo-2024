namespace ClientsManager.Tests;

public class SpyClientRepository : IClientRepository
{
    public int NumberOfSaves { get; private set; } = 0;

    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        NumberOfSaves++;
        return Task.FromResult(true);
    }
}