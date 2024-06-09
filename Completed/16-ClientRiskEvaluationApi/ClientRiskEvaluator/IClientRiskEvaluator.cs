namespace ClientRiskEvaluator;

public interface IClientRiskEvaluator
{
    public Task<RiskEvaluation> CalculateRiskScore(Client client);
}