using ClientRiskEvaluator.RiskCalculators;

namespace ClientRiskEvaluator;

public class ClientRiskEvaluator : IClientRiskEvaluator
{
    private readonly ClientValidator _clientValidator = new();
    private readonly RiskCalculator _riskCalculator;
    private readonly IClientRiskEvaluationStore _evaluationStore;

    public ClientRiskEvaluator(IBlockList blockList, IClientRiskEvaluationStore evaluationStore)
    {
        _evaluationStore = evaluationStore;
        _riskCalculator = new RiskCalculator();
        _riskCalculator
            .SetNext(new BlockListRiskCalculator(blockList))
            .SetNext(new AgeRiskCalculator())
            .SetNext(new EmploymentRiskCalculator())
            .SetNext(new DebtToIncomeRiskCalculator());
    }

    public async Task<RiskEvaluation> CalculateRiskScore(Client client)
    {
        _clientValidator.ValidateClient(client);

        var score = await _riskCalculator.CalculateAsync(client);

        var evaluation = new RiskEvaluation(Guid.NewGuid(), client.Email, score);
        await _evaluationStore.AddAsync(evaluation);

        return evaluation;
    }
}