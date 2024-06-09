using System.Diagnostics.Contracts;
using FluentAssertions;

namespace ClientRiskEvaluator.Tests;

public class ClientRiskEvaluationScenarios
{
    private readonly ClientRiskEvaluator _evaluator = new();

    [Fact]
    public void When_client_has_no_risk_factor_then_risk_is_zero()
    {
        var client = new ClientBuilder().Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(17)]
    public void When_client_is_under_18_years_old_and_not_employed_then_risk_score_is_the_minor_risk(int age)
    {
        var client = new ClientBuilder()
            .WithAge(age)
            .Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(RiskScores.Minor);
    }

    [Fact]
    public void When_client_is_under_18_years_old_and_not_employed_then_risk_score_is_the_sum_of_both_risks()
    {
        const int expectedScore = RiskScores.Minor + RiskScores.Unemployed;
        var client = new ClientBuilder()
            .WithAge(16)
            .WithEmploymentStatus(EmploymentStatus.Unemployed)
            .Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(expectedScore);
    }

    [Theory]
    [InlineData(1100, 2000)]
    [InlineData(401, 1000)]
    public void When_client_debt_to_income_ration_is_high_then_risk_score_is_the_high_debt_risk_score(
        decimal debtPayments, decimal income)
    {
        var client = new ClientBuilder()
            .WithMonthlyIncome(income)
            .WithTotalMonthlyDebtPayments(debtPayments)
            .Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(RiskScores.HighDebtLevel);
    }

    [Theory]
    [InlineData(0, 2000)]
    [InlineData(400, 1000)]
    public void When_client_debt_to_income_ration_is_in_the_threshold_limit_does_not_impact_the_risk_score(decimal debtPayments, decimal income)
    {
        var client = new ClientBuilder()
            .WithMonthlyIncome(income)
            .WithTotalMonthlyDebtPayments(debtPayments)
            .Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(0);
    }

    [Fact]
    public void When_client_is_null_throws_argument_null_exception()
    {
        Action act = () => _evaluator.CalculateRiskScore(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void When_age_is_negative_throws_argument_exception()
    {
        Action act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithAge(-1).Build());

        act.Should().Throw<ArgumentException>().WithMessage("Age cannot be negative");
    }

    [Fact]
    public void When_monthly_income_is_negative_throws_argument_exception()
    {
        Action act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithMonthlyIncome(-1).Build());

        act.Should().Throw<ArgumentException>().WithMessage("Monthly income must be greater than zero");
    }

    [Fact]
    public void When_total_monthly_debt_payments_is_negative_throws_argument_exception()
    {
        Action act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithTotalMonthlyDebtPayments(-1).Build());

        act.Should().Throw<ArgumentException>().WithMessage("Total monthly debt payments must be greater than zero");
    }

    [Fact]
    public void When_client_has_no_income_then_risk_score_is_the_same_as_debt_risk()
    {
        const int expectedScore = RiskScores.HighDebtLevel;
        var client = new ClientBuilder()
            .WithMonthlyIncome(0)
            .Build();

        var riskScore = _evaluator.CalculateRiskScore(client);

        riskScore.Should().Be(expectedScore);
    }

    [Fact]
    public void No_income_risk_is_the_same_as_high_debt_risk()
    {
        RiskScores.NoIncome.Should().Be(RiskScores.HighDebtLevel);
    }
}