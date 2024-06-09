using System.Runtime.InteropServices;
using FluentAssertions;

namespace ClientsManager.Tests;

public class AddClientHandlerTests
{
    [Fact]
    public async Task Should_throw_argument_null_exception_when_command_is_null()
    {
        var repositoryDummy = new DummyClientRepository();
        var handler = new AddClientHandler(repositoryDummy);

        Func<Task> act = async () => await handler.Handle(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Should_return_success_response_when_client_is_added_successfully()
    {
        var repositoryStub = new SuccessfulAddClientRepositoryStub();
        var handler = new AddClientHandler(repositoryStub);

        var response = await handler.Handle(CreateCommand());

        response.Should()
            .BeEquivalentTo(new
            {
                Success = true,
                Message = "Client added successfully."
            });
    }


    [Fact]
    public async Task Should_return_fail_response_when_client_is_not_added_successfully()
    {
        var repositoryStub = new FailAddClientRepositoryStub();
        var handler = new AddClientHandler(repositoryStub);

        var response = await handler.Handle(CreateCommand());

        response.Should()
            .BeEquivalentTo(new
            {
                Success = false,
                Message = "Failed to add client."
            });
    }

    [Fact]
    public async Task Should_add_client_only_once()
    {
        var repositorySpy = new SpyClientRepository();
        var handler = new AddClientHandler(repositorySpy);

        await handler.Handle(CreateCommand());

        repositorySpy.NumberOfSaves.Should().Be(1);
    }

    [Fact]
    public async Task Should_save_client_with_correct_data()
    {
        var repositoryMock = new MockClientRepository();
        repositoryMock.ExpectToSave(CreateCommand());
        var handler = new AddClientHandler(repositoryMock);

        await handler.Handle(CreateCommand());

        repositoryMock.Verify();
    }

    [Fact]
    public async Task Should_fail_if_client_already_exists()
    {
        var repository = new InMemoryClientRepository();
        var handler = new AddClientHandler(repository);
        var command = CreateCommand();
        repository.Add(command.Email, command);

        var response = await handler.Handle(command);

        response.Should()
            .BeEquivalentTo(new
            {
                Success = false,
                Message = "Failed to add client."
            });
    }

    private static AddClientCommand CreateCommand()
    {
        return new AddClientCommand
        {
            Name = "John Doe",
            Email = "john@mailinator.com"
        };
    }
}

public class InMemoryClientRepository : Dictionary<string, AddClientCommand>, IClientRepository
{
    public Task<bool> AddClientAsync(AddClientCommand command)
    {
        if (ContainsKey(command.Email))
        {
            return Task.FromResult(false);
        }

        Add(command.Email, command);

        return Task.FromResult(true);
    }
}