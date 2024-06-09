namespace RefactoringBankTransfer.Tests;

internal class BankService
{
    public void Transfer(Account from, Account to, decimal amount)
    {
        from.Debit(amount);
        to.Credit(amount);
    }
}