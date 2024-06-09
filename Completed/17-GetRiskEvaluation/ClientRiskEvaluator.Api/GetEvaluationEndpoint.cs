using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientRiskEvaluator.Api;

public static class GetEvaluationEndpoint
{
    public static async Task<Ok<RiskEvaluation>> HandleAsync(IClientRiskEvaluationStore store, Guid id)
    {
        var evaluation = await store.GetByIdAsync(id);

        return TypedResults.Ok(evaluation);
    }
}