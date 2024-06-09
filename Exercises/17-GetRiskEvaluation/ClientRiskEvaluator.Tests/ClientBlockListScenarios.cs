using ClientRiskEvaluator.Tests.TestDoubles;
using FluentAssertions;

namespace ClientRiskEvaluator.Tests;

#if false

public class ClientBlockListScenarios
{
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
}
#endif