namespace ClientRiskEvaluator;

public interface IClientRiskEvaluationStore
{
    public Task AddAsync(RiskEvaluation riskEvaluation);
}