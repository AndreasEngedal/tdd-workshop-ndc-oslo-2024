namespace ClientRiskEvaluator.RiskCalculator;

internal class DebtToIncomeRiskCalculator : IRiskCalculator
{
    private const decimal DebtToIncomeRatioThreshold = 0.4M;

    public int Calculate(Client client)
    {
        if (client.MonthlyIncome is 0) return RiskScores.NoIncome;

        return client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
    }
}