using FluentAssertions;

namespace MutationBankTransfer.Tests;

public class AccountTests
{
    [Fact]
    public void Should_fail_when_has_insufficient_funds()
    {
        const decimal openingBalanceSourceAccount = 49;
        const decimal amountToDebit = openingBalanceSourceAccount + 1;
        var account = new Account("ACC_01", openingBalanceSourceAccount);

        var debit = () => account.Debit(amountToDebit);

        debit
            .Should()
            .Throw<InsufficientFundsException>()
            .WithMessage("Insufficient funds for transfer");
    }

    [Fact]
    public void Should_accept_debit_the_total_balance()
    {
        const decimal openingBalanceSourceAccount = 5;
        var account = new Account("ACC_01", openingBalanceSourceAccount);

        account.Debit(openingBalanceSourceAccount);

        account.Balance.Should().Be(0);
    }

    [Fact]
    public void ToString_should_return_account_id_and_balance()
    {
        const decimal openingBalance = 10;
        const string accountNumber = "ACC_01";
        var account = new Account(accountNumber, openingBalance);

        account.ToString()
            .Should()
            .Be($"Account {accountNumber} with balance {openingBalance}");
    }
}