namespace BankTransfer.Tests
{
#if false
    public class BankTransferTests
    {
        private readonly BankService _bankService;
        private readonly Account _account1;
        private readonly Account _account2;

        public BankTransferTests()
        {
            _bankService = new BankService();
            _account1 = new Account { Balance = 1000 };
            _account2 = new Account { Balance = 500 };
        }

        [Fact]
        public void Should_transfer_money_between_two_accounts_and_update_balances()
        {
            const int amountToTransfer = 200;
            _bankService.Transfer(_account1, _account2, amountToTransfer);

            Assert.Equal(800, _account1.Balance);
            Assert.Equal(700, _account2.Balance);
        }
    }
#endif
}