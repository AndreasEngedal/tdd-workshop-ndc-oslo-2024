using FluentAssertions;
using NSubstitute;

namespace ClientRiskEvaluator.Api.Tests;

public class RiskEvaluationEndpointTests
{
    [Fact]
    public async Task Should_call_risk_evaluator_with_client()
    {
        var riskEvaluatorMock = Substitute.For<IClientRiskEvaluator>();
        var client = new Client();

        await RiskEvaluationEndpoint.HandleAsync(riskEvaluatorMock, client);
        
        await riskEvaluatorMock.Received().CalculateRiskScore(client);
    }
    
    [Fact]
    public async Task Should_return_evaluation_from_risk_evaluator()
    {
        var expectedEvaluationResult = new RiskEvaluation(Guid.NewGuid(), "john@random.pt", 10);
        var riskEvaluatorMock = Substitute.For<IClientRiskEvaluator>();
        var client = new Client();
        riskEvaluatorMock.CalculateRiskScore(client).Returns(expectedEvaluationResult);

        var evaluation = await RiskEvaluationEndpoint.HandleAsync(riskEvaluatorMock, client);
        
        evaluation.StatusCode.Should().Be(200);
        evaluation.Value.RiskScore.Should().Be(expectedEvaluationResult.RiskScore);
        evaluation.Value.Id.Should().Be(expectedEvaluationResult.Id);
    }
}