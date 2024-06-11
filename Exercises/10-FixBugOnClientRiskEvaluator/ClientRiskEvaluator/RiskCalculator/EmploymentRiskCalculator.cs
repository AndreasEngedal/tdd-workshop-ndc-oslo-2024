namespace ClientRiskEvaluator.RiskCalculator;

internal class EmploymentRiskCalculator : IRiskCalculator
{
    public int Calculate(Client client)
    {
        if (client.EmploymentStatus == EmploymentStatus.Unemployed
            && client.MonthlyIncome == 0
            && client.DebtToIncomeRatio() > 0)
            return 0;

        return client.EmploymentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : 0;
    }
}