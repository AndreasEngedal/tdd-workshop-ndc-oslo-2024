namespace ClientManager.Tests;

public class ClientRegistrySpy : IClientsRegistry
{
    public int NumberOfClientsAdded { get; private set; }

    public void ValidateClientDoesNotExist(string name, string email)
    {
    }

    public void AppendToFile(string name, string email)
    {
        NumberOfClientsAdded++;
    }
}