namespace ClientRiskEvaluator.Evaluators;

public class ClientRiskEvaluatorClass
{
    private readonly AgeRiskEvaluator _ageRiskEvaluator = new();
    private readonly OccupationRiskEvaluator _occupationRiskEvaluator = new();
    private readonly DebtIncomeRatioRiskEvaluator _debtIncomeRatioRiskEvaluator = new();

    public int Evaluate(Client client)
    {
        var risk = 0;

        risk += _ageRiskEvaluator.Evaluate(client.Age);
        risk += _occupationRiskEvaluator.Evaluate(client.Occupation);
        risk += _debtIncomeRatioRiskEvaluator.Evaluate(client.Debt, client.Income);

        return risk;
    }
}