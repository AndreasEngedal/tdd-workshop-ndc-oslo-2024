using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace ClientRiskEvaluator.AcceptanceTests;

#if false
public class RiskEvaluationFeature : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;
    private readonly IDistributedCache _cache;

    public RiskEvaluationFeature(ApiFactory factory)
    {
        _client = factory.CreateClient();
        _cache = factory.Services.GetRequiredService<IDistributedCache>();
    }

    [Fact]
    public async Task Given_client_has_no_risk_factor_then_risk_is_zero()
    {
        var response = await _client.PostAsJsonAsync("/risk-evaluation", new Client
        {
            Email = "john@randomcorp.pt",
            Age = 25,
            EmploymentStatus = EmploymentStatus.Employed,
            MonthlyIncome = 5000,
            TotalMonthlyDebtPayments = 1000
        });
        
        response.EnsureSuccessStatusCode();
        var evaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        evaluation.RiskScore.Should().Be(0);
        evaluation.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Given_minor_client_and_unemployed_then_risk_score_is_the_sum_of_both_risks()
    {
        var response = await _client.PostAsJsonAsync("/risk-evaluation", new Client
        {
            Email = "john@randomcorp.pt",
            Age = 17,
            EmploymentStatus = EmploymentStatus.Unemployed,
            MonthlyIncome = 200,
            TotalMonthlyDebtPayments = 0,
        });


        response.EnsureSuccessStatusCode();
        var evaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        evaluation.RiskScore.Should().Be(30);
        evaluation.Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Given_blocked_client_then_risk_score_is_high()
    {
        const string email = "blocked_jonny@blockedteam.pt";
        
        await _cache.SetStringAsync(email, "blocked");
            
        var response = await _client.PostAsJsonAsync("/risk-evaluation", new Client
        {
            Email = email,
            Age = 17,
            EmploymentStatus = EmploymentStatus.Unemployed,
            MonthlyIncome = 200,
            TotalMonthlyDebtPayments = 0,
        });


        response.EnsureSuccessStatusCode();
        var evaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        evaluation.RiskScore.Should().Be(2000);
        evaluation.Id.Should().NotBeEmpty();
    }

}
#endif