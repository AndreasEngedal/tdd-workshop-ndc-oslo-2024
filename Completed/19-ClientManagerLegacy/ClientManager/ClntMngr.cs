using System.IO.Pipes;

namespace ClientManager;

using System;

public class ClntMngr
{
    private readonly TimeProvider _timeProvider;

    public ClntMngr()
    {
        _timeProvider = TimeProvider.System;
    }

    public ClntMngr(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public void AddClnt(string name, string email, string filePath)
    {
        AddClient(name, email, new ClientsFile(filePath));
    }

    public void AddClient(string name, string email, IClientsRegistry clientsRegistry)
    {
        ValidateClient(name, email);

        if (!IsBusinessDay())
            throw new Exception("Cannot add clients on Sundays.");

        clientsRegistry.ValidateClientDoesNotExist(name, email);

        clientsRegistry.AppendToFile(name, email);
    }

    private bool IsBusinessDay()
    {
        return _timeProvider.GetUtcNow().DayOfWeek != DayOfWeek.Sunday;
    }

    private static void ValidateClient(string name, string email)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        {
            throw new Exception("Name and email are required.");
        }

        if (!email.Contains("@"))
        {
            throw new Exception("Invalid email.");
        }
    }
}