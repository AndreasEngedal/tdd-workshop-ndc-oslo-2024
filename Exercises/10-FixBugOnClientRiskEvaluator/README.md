# Let's practice: Fix a Bug

Fix the bug described in the GitHub issue: "We can’t get the risk evaluation for an unemployed client without income. According to the Product Team, the risk should be the same as the Debt Risk." 

The goal is to address this bug using Test-Driven Development (TDD).


## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>


#### Step 1: Write a Failing Test

First, add a test case that describes the issue. Specifically, if the client is unemployed and has no income, the risk score should be the same as the debt risk score.

```csharp
using FluentAssertions;

namespace ClientRiskEvaluator.Tests
{
    public class ClientRiskEvaluationScenarios
    {
        private readonly ClientRiskEvaluator _evaluator = new();

        // Existing tests...

        [Fact]
        public void When_client_is_unemployed_and_has_no_income_then_risk_score_is_the_high_debt_risk_score()
        {
            var client = new ClientBuilder()
                .AsUnemployed()
                .WithMonthlyIncome(0)
                .WithTotalMonthlyDebtPayments(500)
                .Build();

            var riskScore = _evaluator.CalculateRiskScore(client);

            riskScore.Should().Be(RiskScores.HighDebtLevel);
        }
    }
}
```

#### Step 2: Make the Test Pass

Modify the `ClientRiskEvaluator` and `DebtToIncomeRiskCalculator` classes to handle the case where the client is unemployed and has no income.

```csharp
namespace ClientRiskEvaluator
{
    public class DebtToIncomeRiskCalculator : IRiskCalculator
    {
        private const decimal DebtToIncomeRatioThreshold = 0.4M;

        public int Calculate(Client client)
        {
            if (client.EmploymentStatus == EmploymentStatus.Unemployed && client.MonthlyIncome == 0)
            {
                return RiskScores.HighDebtLevel;
            }

            return client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
        }
    }
}
```


</details>