namespace ClientRiskEvaluator.Tests;

public class ClientBuilder
{
    private int _age = 18;
    private EmploymentStatus _employmentStatus = EmploymentStatus.Employed;
    private decimal _totalMonthlyDebtPayments = 200;
    private decimal _monthlyIncome = 2000;
    private string _email = "random@mail";

    public ClientBuilder WithAge(int age)
    {
        _age = age;
        return this;
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

    public ClientBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public Client Build()
    {
        return new Client
        {
            Email = _email,
            Age = _age,
            EmploymentStatus = _employmentStatus,
            TotalMonthlyDebtPayments = _totalMonthlyDebtPayments,
            MonthlyIncome = _monthlyIncome
        };
    }
}