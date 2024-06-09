using FluentAssertions;

namespace ClientsManager.Tests;

public class MockClientRepository : IClientRepository
{
    private AddClientCommand _savedCommand;
    private AddClientCommand _expectedCommand;

    public void ExpectToSave(AddClientCommand command)
    {
        _expectedCommand = command;
    }

    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        _savedCommand = command;
        return Task.FromResult(true);
    }

    public void Verify()
    {
        _savedCommand.Should().BeEquivalentTo(_expectedCommand);
    }
}