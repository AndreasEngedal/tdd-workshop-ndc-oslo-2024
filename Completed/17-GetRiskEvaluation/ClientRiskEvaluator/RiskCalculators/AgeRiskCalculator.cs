namespace ClientRiskEvaluator.RiskCalculators;

internal class AgeRiskCalculator : RiskCalculator
{
    public override async ValueTask<int> CalculateAsync(Client client)
    {
        var score = client.IsMinor() ? RiskScores.Minor : 0;
        return score + await base.CalculateAsync(client);
    }
}