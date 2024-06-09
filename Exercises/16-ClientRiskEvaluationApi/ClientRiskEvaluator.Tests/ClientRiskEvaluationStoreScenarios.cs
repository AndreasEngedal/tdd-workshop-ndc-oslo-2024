using ClientRiskEvaluator.Tests.TestDoubles;
using FluentAssertions;

namespace ClientRiskEvaluator.Tests;
#if false
public class ClientRiskEvaluationStoreScenarios
{
    [Fact]
    public async Task Should_store_client_risk_evaluation()
    {
        var store = new InMemoryClientRiskEvaluationStore();
        var evaluator = new ClientRiskEvaluator(new NotInBlockListStub(), store);
        var client = new ClientBuilder().Build();

        var evaluation = await evaluator.CalculateRiskScore(client);

        store[client.Email].Id.Should().Be(evaluation.Id);
        store[client.Email].RiskScore.Should().Be(evaluation.RiskScore);
    }
}
#endif