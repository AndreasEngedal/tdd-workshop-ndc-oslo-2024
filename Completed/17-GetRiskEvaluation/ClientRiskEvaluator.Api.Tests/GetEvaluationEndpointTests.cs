using NSubstitute;

namespace ClientRiskEvaluator.Api.Tests;

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