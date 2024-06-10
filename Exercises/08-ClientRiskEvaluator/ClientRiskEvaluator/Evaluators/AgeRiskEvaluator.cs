namespace ClientRiskEvaluator.Evaluators;

internal class AgeRiskEvaluator
{
    public int Evaluate(int age)
    {
        var risk = 0;
        if (age < 18)
            risk += 20;

        return risk;
    }
}