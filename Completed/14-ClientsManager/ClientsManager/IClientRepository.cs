namespace ClientsManager;

public interface IClientRepository
{
    Task<bool> AddClientAsync(AddClientCommand command);
}