using ClientRiskEvaluator.RiskCalculator;

namespace ClientRiskEvaluator;

public class ClientRiskEvaluator
{
    private readonly ClientValidator _clientValidator = new();
    private readonly List<IRiskCalculator> _riskCalculators =
    [
        new AgeRiskCalculator(),
        new EmploymentRiskCalculator(),
        new DebtToIncomeRiskCalculator()
    ];

    public int CalculateRiskScore(Client client)
    {
        _clientValidator.ValidateClient(client);

        return _riskCalculators.Sum(calculator => calculator.Calculate(client));
    }
}