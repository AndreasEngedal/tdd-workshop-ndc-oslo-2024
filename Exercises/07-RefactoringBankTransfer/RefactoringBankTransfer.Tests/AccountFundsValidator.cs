namespace RefactoringBankTransfer.Tests;

internal static class AccountFundsValidator
{
    public static void GuaranteeAvailableFunds(Account a, decimal val)
    {
        if (a.Balance >= val) 
            return;
        else
            throw new Exception("Insufficient funds for transfer");
    }
}