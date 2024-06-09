namespace RefactoringBankTransfer.Tests
{
    public class BankTransferTests
    {
        private readonly BankService _bankService = new();

        [Fact]
        public void Should_transfer_money_between_two_accounts_and_update_balances()
        {
            const decimal openingBalance = 10;
            var source = new Account(openingBalance);
            var destination = new Account(openingBalance);

            _bankService.Transfer(source, destination, 5);

            Assert.Equal(5, source.Balance);
            Assert.Equal(15, destination.Balance);
        }

        [Fact]
        public void Should_fail_when_account_has_insufficient_funds()
        {
            const decimal openingBalanceSourceAccount = 49;
            const decimal amountToMove = openingBalanceSourceAccount + 1;
            var source = new Account(openingBalanceSourceAccount);
            var destination = new Account(10);

            Assert.Throws<InsufficientFundsException>(() => _bankService.Transfer(source, destination, amountToMove));
        }
    }
}