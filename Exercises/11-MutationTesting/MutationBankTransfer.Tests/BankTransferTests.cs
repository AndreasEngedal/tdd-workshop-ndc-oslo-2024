using FluentAssertions;

namespace MutationBankTransfer.Tests
{

    public class BankTransferTests
    {
        private readonly BankService _bankService = new();

        [Theory]
        [InlineData(10, 5, 5, 15)]
        [InlineData(10, 10, 0, 20)]
        public void Should_transfer_money_between_two_accounts_and_update_balances(
            decimal openingBalance,
            decimal amount,
            decimal expectedSourceBalance,
            decimal expectedDestinationBalance)
        {
            var source = new Account("ACC_01", openingBalance);
            var destination = new Account("ACC_02", openingBalance);

            _bankService.Transfer(source, destination, amount);

            Assert.Equal(expectedSourceBalance, source.Balance);
            Assert.Equal(expectedDestinationBalance, destination.Balance);
        }

        [Fact]
        public void Should_fail_when_account_has_insufficient_funds()
        {
            const decimal openingBalanceSourceAccount = 49;
            const decimal amountToMove = openingBalanceSourceAccount + 1;
            var source = new Account("ACC_01", openingBalanceSourceAccount);
            var destination = new Account("ACC_02", 10);

            var action = () => _bankService.Transfer(source, destination, amountToMove);

            action.Should().Throw<InsufficientFundsException>().WithMessage("Insufficient funds for transfer");
        }
    }
}