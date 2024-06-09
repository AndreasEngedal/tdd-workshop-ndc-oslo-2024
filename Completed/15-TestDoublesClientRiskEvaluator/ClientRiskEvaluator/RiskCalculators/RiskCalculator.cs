namespace ClientRiskEvaluator.RiskCalculators;

internal class RiskCalculator
{
    private RiskCalculator? _nextRiskCalculator;

    public RiskCalculator SetNext(RiskCalculator handler)
    {
        _nextRiskCalculator = handler;
        return handler;
    }

    public virtual async ValueTask<int> CalculateAsync(Client client)
    {
        if (_nextRiskCalculator is null)
            return 0;

        return await _nextRiskCalculator.CalculateAsync(client);
    }
}