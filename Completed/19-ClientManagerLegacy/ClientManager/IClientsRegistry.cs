namespace ClientManager;

public interface IClientsRegistry
{
    void ValidateClientDoesNotExist(string name, string email);
    void AppendToFile(string name, string email);
}