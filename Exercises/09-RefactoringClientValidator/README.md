# Let's practice: Refactoring Client Risk Evaluator

Refactor the `ClientRiskEvaluator` class to adhere to the Single Responsibility Principle (SRP) without adding new tests. 
The goal is to ensure that each class has a single responsibility and the code remains maintainable and scalable.



## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>

### Steps to Refactor

#### Step 1: Extract Validation Logic to a Separate Class

Move the client validation logic to a new `ClientValidator` class to adhere to SRP.

```csharp
namespace ClientRiskEvaluator
{
    public class ClientValidator
    {
        public void Validate(Client client)
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
    }
}
```

Update `ClientRiskEvaluator` to use `ClientValidator`.

```csharp
namespace ClientRiskEvaluator
{
    public class ClientRiskEvaluator
    {
        private const int NoImpactScore = 0;
        private readonly ClientValidator _validator = new();

        public int CalculateRiskScore(Client client)
        {
            _validator.Validate(client);

            var riskScore = 0;
            riskScore += CalculateAgeRiskScore(client);
            riskScore += CalculateEmploymentRiskScore(client.EmploymentStatus);
            riskScore += CalculateDebtToIncomeRiskScore(client.DebtToIncomeRatio());

            return riskScore;
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
}
```

#### Step 2: Extract Risk Calculation Logic to Separate Classes

Create individual classes for each risk factor calculation.

```csharp
namespace ClientRiskEvaluator
{
    public interface IRiskCalculator
    {
        int Calculate(Client client);
    }

    public class AgeRiskCalculator : IRiskCalculator
    {
        public int Calculate(Client client)
        {
            return client.IsMinor() ? RiskScores.Minor : 0;
        }
    }

    public class EmploymentRiskCalculator : IRiskCalculator
    {
        public int Calculate(Client client)
        {
            return client.EmploymentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : 0;
        }
    }

    public class DebtToIncomeRiskCalculator : IRiskCalculator
    {
        private const decimal DebtToIncomeRatioThreshold = 0.4M;

        public int Calculate(Client client)
        {
            return client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
        }
    }
}
```

#### Step 3: Update `ClientRiskEvaluator` to Use the New Risk Calculators

Modify the `ClientRiskEvaluator` to utilize the new risk calculator classes.

```csharp
namespace ClientRiskEvaluator
{
    public class ClientRiskEvaluator
    {
        private readonly ClientValidator _validator = new();
        private readonly List<IRiskCalculator> _riskCalculators = new()
        {
            new AgeRiskCalculator(),
            new EmploymentRiskCalculator(),
            new DebtToIncomeRiskCalculator()
        };

        public int CalculateRiskScore(Client client)
        {
            _validator.Validate(client);

            return _riskCalculators.Sum(calculator => calculator.Calculate(client));
        }
    }
}
```

### Final Code Structure

After refactoring, the classes are organized as follows:

#### Client Class

```csharp
namespace ClientRiskEvaluator
{
    public class Client
    {
        public int Age { get; init; }
        public EmploymentStatus EmploymentStatus { get; init; }
        public decimal TotalMonthlyDebtPayments { get; init; }
        public decimal MonthlyIncome { get; init; }

        public decimal DebtToIncomeRatio() => TotalMonthlyDebtPayments / MonthlyIncome;
        public bool IsMinor() => Age < 18;
    }
}
```

#### ClientValidator Class

```csharp
namespace ClientRiskEvaluator
{
    public class ClientValidator
    {
        public void Validate(Client client)
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
    }
}
```

#### Risk Calculators

```csharp
namespace ClientRiskEvaluator
{
    public interface IRiskCalculator
    {
        int Calculate(Client client);
    }

    public class AgeRiskCalculator : IRiskCalculator
    {
        public int Calculate(Client client)
        {
            return client.IsMinor() ? RiskScores.Minor : 0;
        }
    }

    public class EmploymentRiskCalculator : IRiskCalculator
    {
        public int Calculate(Client client)
        {
            return client.EmploymentStatus == EmploymentStatus.Unemployed ? RiskScores.Unemployed : 0;
        }
    }

    public class DebtToIncomeRiskCalculator : IRiskCalculator
    {
        private const decimal DebtToIncomeRatioThreshold = 0.4M;

        public int Calculate(Client client)
        {
            return client.DebtToIncomeRatio() > DebtToIncomeRatioThreshold ? RiskScores.HighDebtLevel : 0;
        }
    }
}
```

#### ClientRiskEvaluator Class

```csharp
namespace ClientRiskEvaluator
{
    public class ClientRiskEvaluator
    {
        private readonly ClientValidator _validator = new();
        private readonly List<IRiskCalculator> _riskCalculators = new()
        {
            new AgeRiskCalculator(),
            new EmploymentRiskCalculator(),
            new DebtToIncomeRiskCalculator()
        };

        public int CalculateRiskScore(Client client)
        {
            _validator.Validate(client);

            return _riskCalculators.Sum(calculator => calculator.Calculate(client));
        }
    }
}
```

By refactoring the code, we ensure each class has a single responsibility, making the code more maintainable and easier to extend in the future. The tests remain unchanged and continue to pass, ensuring the functionality is intact.

</details>