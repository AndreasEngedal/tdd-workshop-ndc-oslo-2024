namespace RefactoringBankTransfer.Tests;

internal class Account
{
    private decimal _balance;

    public Account(decimal openingBalance)
    {
        _balance = openingBalance;
    }

    public decimal Balance => _balance;

    public void Debit(decimal amount)
    {
        if (Balance < amount) {
            throw new InsufficientFundsException("Insufficient funds for transfer");
        }
        _balance -= amount;
    }
    
    public void Credit(decimal amount)
    {
        _balance += amount;
    }
}