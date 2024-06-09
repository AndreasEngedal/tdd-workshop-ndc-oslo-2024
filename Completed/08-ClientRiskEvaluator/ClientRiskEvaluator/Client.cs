namespace ClientRiskEvaluator;

public class Client
{
    public int Age { get; init; }
    public EmploymentStatus EmploymentStatus { get; init; }
    public decimal TotalMonthlyDebtPayments { get; init; }
    public decimal MonthlyIncome { get; init; }

    public decimal DebtToIncomeRatio()
        => TotalMonthlyDebtPayments / MonthlyIncome;
    public bool IsMinor()
        => Age < 18;
}