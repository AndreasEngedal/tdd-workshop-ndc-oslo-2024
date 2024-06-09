namespace RefactoringBankTransfer.Tests
{
#if false
    public class BankTransferTests
    {
      
        [Fact]
        public void Should_transfer_money_between_two_accounts_and_update_balances()
        {
            var a = new Account() { Balance = 10 };
            var b = new Account() { Balance = 10 };
            var bankService = new BankService();
            
            bankService.Transfer(a, b, 5);

            Assert.Equal(5, a.Balance);
            Assert.Equal(15, b.Balance);
        }
        
        [Fact]
        public void Should_fail_when_account_has_insufficient_funds()
        {
            var a = new Account() { Balance = 10 };
            var b = new Account() { Balance = 10 };
            var bankService = new BankService();
            var act = () => bankService.Transfer(a, b, 50);
            Assert.Throws<Exception>(act);
        }
    }
#endif
}