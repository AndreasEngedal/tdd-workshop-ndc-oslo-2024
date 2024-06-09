using MutationBankTransfer;

namespace MutationBankTransfer.Tests
{
#if false

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

            Assert.Equal(5, source.Balance);
            Assert.Equal(15, destination.Balance);
        }

        [Fact]
        public void Should_fail_when_account_has_insufficient_funds()
        {
            const decimal openingBalanceSourceAccount = 49;
            const decimal amountToMove = openingBalanceSourceAccount + 1;
            var source = new Account("ACC_01", openingBalanceSourceAccount);
            var destination = new Account("ACC_02", 10);

            Assert.Throws<InsufficientFundsException>(() => _bankService.Transfer(source, destination, amountToMove));
        }
    }
#endif
}