namespace ClientRiskEvaluator;

public class ClientRiskEvaluator
{
    private const int NoImpactScore = 0;

    public int CalculateRiskScore(Client client)
    {
        ValidateClient(client);

        var riskScore = 0;

        riskScore += CalculateAgeRiskScore(client);
        riskScore += CalculateEmploymentRiskScore(client.EmploymentStatus);
        riskScore += CalculateDebtToIncomeRiskScore(client.DebtToIncomeRatio());

        return riskScore;
    }

    private void ValidateClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if (client.Age < 0)
        {
            throw new ArgumentException("Age cannot be negative");
        }

        if (client.MonthlyIncome < 0)
        {
            throw new ArgumentException("Monthly income must be greater than zero");
        }

        if (client.TotalMonthlyDebtPayments < 0)
        {
            throw new ArgumentException("Total monthly debt payments must be greater than zero");
        }
    }

    private static int CalculateAgeRiskScore(Client client) 
        => client.IsMinor() ? RiskScores.Minor : NoImpactScore;

    private static int CalculateEmploymentRiskScore(EmploymentStatus employmentStatus) 
        => employmentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : NoImpactScore;

    private static int CalculateDebtToIncomeRiskScore(decimal debtToIncomeRatio)
    {
        const decimal debtToIncomeRationThreshold = 0.4M;
        return debtToIncomeRatio > debtToIncomeRationThreshold ? RiskScores.HighDebtLevel : NoImpactScore;
    }
}