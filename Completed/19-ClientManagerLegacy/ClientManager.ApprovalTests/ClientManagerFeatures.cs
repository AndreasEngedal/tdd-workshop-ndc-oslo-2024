namespace ClientManager.ApprovalTests;

public class ClientManagerFeatures
{
    private readonly ClntMngr _manager;

    public ClientManagerFeatures()
    {
        _manager = new();
    }

    [Fact]
    public Task Given_a_client_and_file_Then_updates_file_as_expected()
    {
        const string filePath = "testing.txt";
        EnsureFileExists(filePath);

        _manager.AddClnt("Gui", "random@gui-random.com", filePath);

        return VerifyFile(filePath);
    }

    [Fact]
    public Task Given_a_duplicated_client_Then_throws_exception()
    {
        const string filePath = "testing-duplicated.txt";
        EnsureFileExists(filePath);
        _manager.AddClnt("Gui", "random@gui-random.com", filePath);

        return Verifier.Throws(
            () => _manager.AddClnt("Gui", "random@gui-random.com", filePath));
    }

    [Fact]
    public Task Given_an_invalid_email_then_throws_exception()
    {
        const string filePath = "testing-invalid-email.txt";
        EnsureFileExists(filePath);

        return Verifier.Throws(
            () => _manager.AddClnt("Gui", "gui", filePath));
    }

       
    [Fact]
    public Task Given_an_empty_email_then_throws_exception()
    {
        const string filePath = "testing-empty-email.txt";
        EnsureFileExists(filePath);

        return Verifier.Throws(
            () => _manager.AddClnt("Gui", "", filePath));
    }

       
    [Fact]
    public Task Given_an_invalid_name_then_throws_exception()
    {
        const string filePath = "testing-empty-name.txt";
        EnsureFileExists(filePath);

        return Verifier.Throws(
            () => _manager.AddClnt("", "random@gui-random.com", filePath));
    }

    private static void EnsureFileExists(string path)
    {
        using (File.Create(path))
        {
        }
    }
}