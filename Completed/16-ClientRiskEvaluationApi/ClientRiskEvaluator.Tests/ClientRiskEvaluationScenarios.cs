using ClientRiskEvaluator.Tests.TestDoubles;
using FluentAssertions;

namespace ClientRiskEvaluator.Tests;

public class ClientRiskEvaluationScenarios
{
    private readonly ClientRiskEvaluator _evaluator = new(new NotInBlockListStub(),
        new SuccessfulClientRiskEvaluationStoreStub());

    [Fact]
    public async Task When_client_has_no_risk_factor_then_risk_is_zero()
    {
        var client = new ClientBuilder().Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(17)]
    public async Task When_client_is_under_18_years_old_and_not_employed_then_risk_score_is_the_minor_risk(int age)
    {
        var client = new ClientBuilder()
            .WithAge(age)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(RiskScores.Minor);
    }

    [Fact]
    public async Task When_client_is_under_18_years_old_and_not_employed_then_risk_score_is_the_sum_of_both_risks()
    {
        const int expectedScore = RiskScores.Minor + RiskScores.Unemployed;
        var client = new ClientBuilder()
            .WithAge(16)
            .WithEmploymentStatus(EmploymentStatus.Unemployed)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(expectedScore);
    }

    [Theory]
    [InlineData(1100, 2000)]
    [InlineData(401, 1000)]
    public async Task When_client_debt_to_income_ration_is_high_then_risk_score_is_the_high_debt_risk_score(
        decimal debtPayments, decimal income)
    {
        var client = new ClientBuilder()
            .WithMonthlyIncome(income)
            .WithTotalMonthlyDebtPayments(debtPayments)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(RiskScores.HighDebtLevel);
    }

    [Theory]
    [InlineData(0, 2000)]
    [InlineData(400, 1000)]
    public async Task When_client_debt_to_income_ration_is_in_the_threshold_limit_does_not_impact_the_risk_score(
        decimal debtPayments, decimal income)
    {
        var client = new ClientBuilder()
            .WithMonthlyIncome(income)
            .WithTotalMonthlyDebtPayments(debtPayments)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(0);
    }

    [Fact]
    public async Task When_client_is_null_throws_argument_null_exception()
    {
        var act = () => _evaluator.CalculateRiskScore(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task When_age_is_negative_throws_argument_exception()
    {
        var act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithAge(-1).Build());

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Age cannot be negative");
    }

    [Fact]
    public async Task When_monthly_income_is_negative_throws_argument_exception()
    {
        var act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithMonthlyIncome(-1).Build());

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Monthly income must be greater than zero");
    }

    [Fact]
    public async Task When_total_monthly_debt_payments_is_negative_throws_argument_exception()
    {
        var act = () => _evaluator.CalculateRiskScore(new ClientBuilder().WithTotalMonthlyDebtPayments(-1).Build());

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Total monthly debt payments must be greater than zero");
    }

    [Fact]
    public async Task When_client_has_no_income_then_risk_score_is_the_same_as_debt_risk()
    {
        const int expectedScore = RiskScores.HighDebtLevel;
        var client = new ClientBuilder()
            .WithMonthlyIncome(0)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(expectedScore);
    }

    [Fact]
    public void No_income_risk_is_the_same_as_high_debt_risk()
    {
        RiskScores.NoIncome.Should().Be(RiskScores.HighDebtLevel);
    }

    [Fact]
    public async Task When_risk_evaluation_is_made_its_added_to_the_storage_and_id_is_returned()
    {
        var client = new ClientBuilder().Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task When_has_all_the_risks_then_risk_score_is_the_sum_of_all_risks()
    {
        const int expectedScore = RiskScores.Minor + RiskScores.Unemployed + RiskScores.HighDebtLevel;
        var client = new ClientBuilder()
            .WithAge(17)
            .WithEmploymentStatus(EmploymentStatus.Unemployed)
            .WithMonthlyIncome(1500)
            .WithTotalMonthlyDebtPayments(1000)
            .Build();

        var evaluation = await _evaluator.CalculateRiskScore(client);

        evaluation.RiskScore.Should().Be(expectedScore);
    }
}
