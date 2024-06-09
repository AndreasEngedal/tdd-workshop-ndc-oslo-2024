namespace ClientRiskEvaluator.Tests.TestDoubles;

internal class SuccessfulClientRiskEvaluationStoreStub : IClientRiskEvaluationStore
{
    public Task AddAsync(RiskEvaluation riskEvaluation)
    {
        return Task.CompletedTask;
    }

    public Task<RiskEvaluation?> GetByIdAsync(Guid id)
    {
        return null!;
    }
}