namespace ClientRiskEvaluator;

public interface IClientRiskEvaluationStore
{
    public Task AddAsync(RiskEvaluation riskEvaluation);
    Task<RiskEvaluation?> GetByIdAsync(Guid id);
}