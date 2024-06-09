namespace ClientManager;

public class ClientsFile : IClientsRegistry
{
    private readonly string _filePath;

    public ClientsFile(string filePath)
    {
        _filePath = filePath;
    }

    public void ValidateClientDoesNotExist(string name, string email)
    {
        var lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
            {
                continue;
            }

            var existingName = parts[0];
            var existingEmail = parts[1];

            if (existingName == name && existingEmail == email)
            {
                throw new Exception("Client already exists.");
            }
        }
    }

    public void AppendToFile(string name, string email)
    {
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine($"{name},{email}");
    }
}