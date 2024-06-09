namespace ClientRiskEvaluator.RiskCalculator;

internal class DebtToIncomeRiskCalculator : IRiskCalculator
{
    private const decimal DebtToIncomeRatioThreshold = 0.4M;

    public int Calculate(Client client)
    {
        return client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
    }
}