# Let's practice: Test Doubles

The goal of this exercise is to evolve the `ClientRiskEvaluator` to support a block list, which will be used to check if a client's email is part of a block list storage. 
You will also implement and test the use of the `IBlockList` and `IClientRiskEvaluationStore` interfaces using Test Doubles.

## Context from previous exercise

**Design the architecture for the Client Risk API. Using sticky notes, enumerate the Adapters and Ports needed.**
The API should provide an endpoint to calculate the risk score for a client.
The API uses storage (Redis) to check if the client is part of a Block List using the client's email. If so, it’s added 2000 points to the risk. 
The API adds the database (PgSQL) to the risk evaluation. An ID is assigned to the Risk Evaluation.
The API Response contains the Risk Evaluation ID.

## Requirements

1. **Check Block List**:
    - Implement the `IBlockList` interface to check if a client's email is in the block list.
    - If a client's email is found in the block list, the risk evaluation should immediately return a predefined high risk score.

2. **Store Risk Evaluation Results**:
    - Implement the `IClientRiskEvaluationStore` interface to add the results of the risk evaluation to a storage system.

3. **Test Doubles**:
    - Use Test Doubles (e.g., Mocks, Stubs) to simulate the behavior of the `IBlockList` and `IClientRiskEvaluationStore` interfaces during testing.


## Important

Uncomment the tests first.

Go to the `ClientRiskEvaluationScenarios.cs` and remove the `#if false` and `#endif`.


## Solution Tips:

<details>
  <summary><i>Solution Tips</i></summary>


##### 1: Implement a Stub 

Use a Stub to quickly keep existing Tests working.

```csharp
public class ClientRiskEvaluationScenarios
{
    private readonly ClientRiskEvaluator _evaluator = new(new NotInBlockListStub(),
        new SuccessfulClientRiskEvaluationStoreStub());
    //...
}

internal class SuccessfulClientRiskEvaluationStoreStub : IClientRiskEvaluationStore
{
    public Task AddAsync(RiskEvaluation riskEvaluation)
    {
        return Task.CompletedTask;
    }
}

internal class NotInBlockListStub : IBlockList
{
    public Task<bool> IsBlocked(string email)
    {
        return Task.FromResult(false);
    }
}
```

##### 2: Use In Memory Fakes to simulate behaviour

Implement the functionality to ensure the tests pass:

```csharp
    public class InMemoryBlockList : HashSet<string>, IBlockList
    {
        public Task<bool> IsBlocked(string email)
        {
            return Task.FromResult(Contains(email));
        }
    }

    public class InMemoryClientRiskEvaluationStore : Dictionary<string, RiskEvaluation>, IClientRiskEvaluationStore
    {
        public Task AddAsync(RiskEvaluation riskEvaluation)
        {
            this[riskEvaluation.Email] = riskEvaluation;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task When_client_email_is_on_the_block_list_then_risk_score_is_high()
    {
        var client = new ClientBuilder()
            .WithEmail("john@mailinator.com")
            .Build();
        var blockList = new InMemoryBlockList { client.Email };
        var evaluator = new ClientRiskEvaluator(blockList, new SuccessfulClientRiskEvaluationStoreStub());

        var evaluation = await evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(RiskScores.BlockListed);
    }
```
</details>



