# Let's practice: London School

The goal of this exercise is to add a GET API endpoint to retrieve previously generated risk evaluations by their ID. You will follow the London School of TDD, which emphasizes interaction-based testing using mocks.

## Important

Uncomment the tests first.

Go to the following files and remove the `#if false` and `#endif`:
- `ClientRiskEvaluationScenarios.cs`
- `ClientBlockListScenarios.cs`
- `ClientRiskEvaluationStoreScenarios.cs`

## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>

### Step 1: Write an Acceptance Test

- Create an acceptance test to validate the GET endpoint from the outside.

```csharp
public class GetEvaluationFeature : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public GetEvaluationFeature(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Given_an_existing_evaluation_When_get_using_id_Then_can_access_the_score()
    {
        const string email = "peter@testing-get-feature.pt";
        var createdEvaluation = await CreateRiskEvaluation(email);

        var response = await _client.GetAsync($"/risk-evaluation/{createdEvaluation.Id}");
        
        response.EnsureSuccessStatusCode();
        var retrievedEvaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        retrievedEvaluation.Should().BeEquivalentTo(createdEvaluation);
    }

    private async Task<RiskEvaluation> CreateRiskEvaluation(string email)
    {
        var response = await _client.PostAsJsonAsync("/risk-evaluation", new Client
        {
            Email = email,
            Age = 25,
            EmploymentStatus = EmploymentStatus.Employed,
            MonthlyIncome = 5000,
            TotalMonthlyDebtPayments = 1000
        });
        
        response.EnsureSuccessStatusCode();
        var evaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        return evaluation!;
    }
}
```

### Step 2: Run the tests and see the Acceptance Test failing

### Step 3: Introduce a Collection

- Use an xUnit Collection to share resources across both Acceptance Test classes

```csharp
[CollectionDefinition("API collection")]
public class ApiTestCollection : ICollectionFixture<ApiFactory>
{
}
```

- Apply it to the `GetEvaluationFeature`

```csharp
[Collection("API collection")]
public class GetEvaluationFeature
{
    private readonly HttpClient _client;

    public GetEvaluationFeature(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }
    // ...
}
```

- Apply it to the `RiskEvaluationFeature`

```csharp
[Collection("API collection")]
public class RiskEvaluationFeature
{
    private readonly HttpClient _client;
    private readonly IDistributedCache _cache;

    public RiskEvaluationFeature(ApiFactory factory)
    {
        _client = factory.CreateClient();
        _cache = factory.Services.GetRequiredService<IDistributedCache>();
    }
    //...
}
```

### Step 4: Run the tests and see the Acceptance Test failing

### Step 5: Create a Test for the API Endpoint

- Use a mock to verify that the Endpoint grabs the Evaluation from the `IClientRiskEvaluationStore`.

```csharp
public class GetEvaluationEndpointTests
{
    [Fact]
    public async Task Should_get_evaluation_from_store_using_id()
    {
        var expectedEvaluation = new RiskEvaluation(Guid.NewGuid(), "peter@the-great-peter.pt", 10);
        var storeMock = Substitute.For<IClientRiskEvaluationStore>();

        await GetEvaluationEndpoint.HandleAsync(storeMock, expectedEvaluation.Id);

        await storeMock.Received().GetByIdAsync(expectedEvaluation.Id);
    }
}
```

### Step 6: Generate the Endpoint missing code

### Step 7: Run the tests and see them failing

### Step 8: Make the test pass

```csharp
public static async Task<Ok<RiskEvaluation>> HandleAsync(IClientRiskEvaluationStore store, Guid id)
{
    var evaluation = await store.GetByIdAsync(id);
    return TypedResults.Ok(evaluation);
}
```

### Step 9: Register the new endpoint

- Add to the `Program.cs`

```csharp
app.MapGet("/risk-evaluation/{id}", GetEvaluationEndpoint.HandleAsync);
```

### Step 10: Run the Acceptance Tests 

</details>