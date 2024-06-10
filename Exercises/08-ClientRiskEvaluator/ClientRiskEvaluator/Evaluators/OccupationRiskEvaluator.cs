namespace ClientRiskEvaluator.Evaluators;

internal class OccupationRiskEvaluator
{
    public int Evaluate(Occupation occupation)
    {
        var risk = 0;
        if (occupation == Occupation.Unemployed)
            risk += 10;

        return risk;
    }
}