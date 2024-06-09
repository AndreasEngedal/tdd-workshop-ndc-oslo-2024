using FluentAssertions;
using MutationBankTransfer;

namespace MutationBankTransfer.Tests
{
    public class BankTransferTests
    {
        private readonly BankService _bankService = new();

        [Fact]
        public void Should_transfer_money_between_two_accounts_and_update_balances()
        {
            const decimal openingBalance = 10;
            var source = new Account("ACC_01", openingBalance);
            var destination = new Account("ACC_02", openingBalance);

            _bankService.Transfer(source, destination, 5);

            source.Balance.Should().Be(5);
            destination.Balance.Should().Be(15);
        }
    }
}