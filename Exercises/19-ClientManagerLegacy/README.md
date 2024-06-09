# Let's practice: Legacy Code

The goal of this exercise is to refactor the `ClntMngr` class to make the Sunday validation testable and to optionally allow using new methods to save clients besides a text file. You will use Approval Tests (using Verify, a C# library) and TDD to ensure that the current functionality is preserved during the refactoring.

## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>


### Step 1: Install Verify Library

1. Add the Verify library to your project by running the following commands:

   ```bash
   dotnet add package Verify.Xunit
   ```

### Step 2: Create Approval Tests

1. **Write tests to approve the current functionality:**

   - Create a test project if you don't have one already.
   - Add a new test class `ClientManagerApprovalTests`.

   ```csharp
   using ClientManager;
   using System;
   using System.IO;
   using System.Threading.Tasks;
   using VerifyXunit;
   using Xunit;

   [UsesVerify]
   public class ClientManagerApprovalTests
   {
       private readonly ClntMngr _manager = new();

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
   ```

2. **Run the tests and approve the results:**

   - Run the tests to generate the approval files.
   - Review and approve the generated `.approved.txt` files to establish the baseline.

### Step 3: Refactor for Testability

1. **Extract date-related logic to make it testable:**

   ```csharp
   using Microsoft.Extensions.Time.Testing;

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
               throw new Exception("Name and email are required.");

           if (!email.Contains("@"))
               throw new Exception("Invalid email.");
       }
   }
   ```

2. **Create tests to verify the date-related logic:**

   ```csharp
   public class ClientManagerScenarios
   {
       [Fact]
       public void When_today_is_sunday_Then_throws_exception()
       {
           var sundayDateTime = new DateTimeOffset(2024, 1, 7, 0, 0, 0, TimeSpan.Zero);
           var timeProvider = new FakeTimeProvider(sundayDateTime);
           var manager = new ClntMngr(timeProvider);

           Assert.Throws<Exception>(() => manager.AddClnt("Gui", "gui@gui", "testing-sunday.txt"));
       }
   }
   ```

### Step 4: Refactor for Extensibility

1. **Abstract the storage mechanism to make it extensible:**

   ```csharp
   public interface IClientsRegistry
   {
       void ValidateClientDoesNotExist(string name, string email);
       void AppendToFile(string name, string email);
   }

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
                   continue;

               var existingName = parts[0];
               var existingEmail = parts[1];

               if (existingName == name && existingEmail == email)
                   throw new Exception("Client already exists.");
           }
       }

       public void AppendToFile(string name, string email)
       {
           using var writer = new StreamWriter(_filePath, true);
           writer.WriteLine($"{name},{email}");
       }
   }
   ```

2. **Create tests to verify the new storage mechanism:**

   ```csharp
   public class ClientManagerScenarios
   {
       [Fact]
       public void When_clients_registry_is_provided_then_adds_client()
       {
           var clientsRegistry = new ClientRegistrySpy();
           var manager = new ClntMngr();

           manager.AddClient("Gui", "gui@gui", clientsRegistry);

           clientsRegistry.NumberOfClientsAdded.Should().Be(1);
       }
   }

   public class ClientRegistrySpy : IClientsRegistry
   {
       public int NumberOfClientsAdded { get; private set; }

       public void ValidateClientDoesNotExist(string name, string email) { }

       public void AppendToFile(string name, string email)
       {
           NumberOfClientsAdded++;
       }
   }
   ```

3. **Run the tests and ensure they pass:**

   - Run all the tests to verify that the functionality remains consistent and matches the approved results.

</details>

