namespace RefactoringBankTransfer.Tests;

internal class BankService
{
    public void Transfer(Account a, Account b, decimal val)
    {
        AccountFundsValidator.GuaranteeAvailableFunds(a, val);
        a.Balance -= val;
        b.Balance += val;
    }
}