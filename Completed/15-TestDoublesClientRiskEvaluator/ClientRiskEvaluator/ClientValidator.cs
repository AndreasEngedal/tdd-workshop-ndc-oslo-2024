namespace ClientRiskEvaluator;

internal class ClientValidator
{
    public void ValidateClient(Client client)
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