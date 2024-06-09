namespace ClientRiskEvaluator.RiskCalculator;

internal class EmploymentRiskCalculator : IRiskCalculator
{
    public int Calculate(Client client)
    {
        return client.EmploymentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : 0;
    }
}