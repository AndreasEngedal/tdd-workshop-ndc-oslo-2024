namespace ClientsManager;

public class AddClientHandler
{
    private readonly IClientRepository _repository;

    public AddClientHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddClientResponse> Handle(AddClientCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        var success = await _repository.AddClientAsync(command);

        return new AddClientResponse
        {
            Success = success,
            Message = success ? "Client added successfully." : "Failed to add client."
        };
    }
}