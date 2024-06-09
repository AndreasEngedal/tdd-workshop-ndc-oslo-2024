namespace BankTransfer.Tests;

internal class BankService
{
    public void Transfer(Account from, Account to, int amount)
    {
        from.Balance -= amount;
        to.Balance += amount;
    }
}