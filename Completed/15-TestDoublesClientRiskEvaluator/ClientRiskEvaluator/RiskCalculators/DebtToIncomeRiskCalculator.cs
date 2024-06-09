namespace ClientRiskEvaluator.RiskCalculators;

internal class DebtToIncomeRiskCalculator : RiskCalculator
{
    private const decimal DebtToIncomeRatioThreshold = 0.4M;

    public override async ValueTask<int> CalculateAsync(Client client)
    {
        if (client.MonthlyIncome is 0) return RiskScores.NoIncome + await base.CalculateAsync(client);

        var score = client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
        return score + await base.CalculateAsync(client);
    }
}