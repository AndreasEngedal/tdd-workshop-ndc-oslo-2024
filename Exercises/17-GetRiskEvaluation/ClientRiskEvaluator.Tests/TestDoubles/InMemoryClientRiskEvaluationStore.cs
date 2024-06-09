namespace ClientRiskEvaluator.Tests.TestDoubles;

public class InMemoryClientRiskEvaluationStore : Dictionary<string, RiskEvaluation>, IClientRiskEvaluationStore
{
    public Task AddAsync(RiskEvaluation riskEvaluation)
    {
        this[riskEvaluation.Email] = riskEvaluation;
        return Task.CompletedTask;
    }
}