using ClientRiskEvaluator;
using ClientRiskEvaluator.Evaluators;
using FluentAssertions;

namespace ClientRiskEvaluatorTests;

public class ClientRiskEvaluatorTests
{
    [Theory]
    [InlineData(30, Occupation.Employed, 100, 1000, 0)]
    [InlineData(15, Occupation.Employed, 100, 1000, 20)]
    [InlineData(30, Occupation.Unemployed, 100, 1000, 10)]
    [InlineData(30, Occupation.Employed, 410, 1000, 25)]
    [InlineData(15, Occupation.Unemployed, 500, 1000, 55)]
    public void When_ValidClientIsGiven_Then_ReturnExpectedRisk(int age, Occupation occupation, decimal debt, decimal income, int expectedRisk)
    {
        var client = new Client
        {
            Age = age,
            Occupation = occupation,
            Debt = debt,
            Income = income
        };

        var riskEvaluator = new ClientRiskEvaluatorClass();

        var risk = riskEvaluator.Evaluate(client);

        risk.Should().Be(expectedRisk);
    }
}