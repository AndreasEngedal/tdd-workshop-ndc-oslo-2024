using System.Net.Http.Json;
using FluentAssertions;

namespace ClientRiskEvaluator.AcceptanceTests;

[Collection("API collection")]
public class GetEvaluationFeature
{
    private readonly HttpClient _client;

    public GetEvaluationFeature(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Given_an_existing_evaluation_When_get_using_id_Then_can_access_the_score()
    {
        const string email = "peter@testing-get-feature.pt";
        var createdEvaluation = await CreateRiskEvaluation(email);

        var response = await _client.GetAsync($"/risk-evaluation/{createdEvaluation.Id}");
        
        response.EnsureSuccessStatusCode();
        var retrievedEvaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        retrievedEvaluation.Should().BeEquivalentTo(createdEvaluation);
    }

    private async Task<RiskEvaluation> CreateRiskEvaluation(string email)
    {
        var response = await _client.PostAsJsonAsync("/risk-evaluation", new Client
        {
            Email = email,
            Age = 25,
            EmploymentStatus = EmploymentStatus.Employed,
            MonthlyIncome = 5000,
            TotalMonthlyDebtPayments = 1000
        });
        
        response.EnsureSuccessStatusCode();
        var evaluation = await response.Content.ReadFromJsonAsync<RiskEvaluation>();
        return evaluation!;
    }

}