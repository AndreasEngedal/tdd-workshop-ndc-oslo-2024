using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientRiskEvaluator.Api;

public static class RiskEvaluationEndpoint
{
    public static async Task<Ok<RiskEvaluation>> HandleAsync(IClientRiskEvaluator riskEvaluator, Client client)
    {
        var evaluation = await riskEvaluator.CalculateRiskScore(client);

        return TypedResults.Ok(evaluation);
    }

}