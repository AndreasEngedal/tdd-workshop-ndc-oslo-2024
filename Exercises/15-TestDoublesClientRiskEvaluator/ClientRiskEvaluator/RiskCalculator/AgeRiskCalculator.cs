namespace ClientRiskEvaluator.RiskCalculator;

internal class AgeRiskCalculator : IRiskCalculator
{
    public int Calculate(Client client)
    {
        return client.IsMinor() ? RiskScores.Minor : 0;
    }
}