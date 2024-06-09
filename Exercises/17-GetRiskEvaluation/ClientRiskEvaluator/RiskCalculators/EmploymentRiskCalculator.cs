namespace ClientRiskEvaluator.RiskCalculators;

internal class EmploymentRiskCalculator : RiskCalculator
{
    public override async ValueTask<int> CalculateAsync(Client client)
    {
        var score = client.EmploymentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : 0;
        return score + await base.CalculateAsync(client);
    }
}