namespace ClientRiskEvaluator.Evaluators;

internal class DebtIncomeRatioRiskEvaluator
{
    public int Evaluate(decimal debt, decimal income)
    {
        var risk = 0;
        var debtToIncomeRatio = debt / income * 100;
        if (debtToIncomeRatio > 40)
            risk += 25;

        return risk;
    }
}