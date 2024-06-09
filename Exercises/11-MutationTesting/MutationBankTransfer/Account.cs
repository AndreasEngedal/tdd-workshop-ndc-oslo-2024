namespace MutationBankTransfer;

public class Account
{
    private decimal _balance;

    public Account(string number, decimal openingBalance)
    {
        _balance = openingBalance;
        Number = number;
    }

    public decimal Balance => _balance;
    public string Number { get; }

    public void Debit(decimal amount)
    {
        if (Balance < amount)
        {
            throw new InsufficientFundsException("Insufficient funds for transfer");
        }

        _balance -= amount;
    }

    public void Credit(decimal amount)
    {
        _balance += amount;
    }
    
    public override string ToString()
    {
        return $"Account {Number} with balance {Balance}";
    }
}