namespace ClientRiskEvaluator.Tests;

public class ClientBuilder
{
    private int _age = 18;
    private EmploymentStatus _employmentStatus = EmploymentStatus.Employed;
    private decimal _totalMonthlyDebtPayments = 200;
    private decimal _monthlyIncome = 2000;

    public ClientBuilder WithAge(int age)
    {
        _age = age;
        return this;
    }
    
    public ClientBuilder AsUnemployed()
    {
        return WithEmploymentStatus(EmploymentStatus.Unemployed);
    }
    
    public ClientBuilder WithEmploymentStatus(EmploymentStatus employmentStatus)
    {
        _employmentStatus = employmentStatus;
        return this;
    }

    public ClientBuilder WithTotalMonthlyDebtPayments(decimal totalMonthlyDebtPayments)
    {
        _totalMonthlyDebtPayments = totalMonthlyDebtPayments;
        return this;
    }

    public ClientBuilder WithMonthlyIncome(decimal monthlyIncome)
    {
        _monthlyIncome = monthlyIncome;
        return this;
    }

    public Client Build()
    {
        return new Client
        {
            Age = _age,
            EmploymentStatus = _employmentStatus,
            TotalMonthlyDebtPayments = _totalMonthlyDebtPayments,
            MonthlyIncome = _monthlyIncome
        };
    }
}