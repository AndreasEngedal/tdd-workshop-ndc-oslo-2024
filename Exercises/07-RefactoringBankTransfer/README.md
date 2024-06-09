# Let's practice: Refactoring

Refactor Bank Transfers by applying the 4 rules of simple design to achieve a clean, maintainable, and well-tested solution.


**4 Rules of Simple Design:**

1. Passes the tests: Ensure all tests are passing.
2. Reveals intention: Code should clearly express its purpose.
3. No duplication: Remove any code duplication.
4. Minimize the number of classes and methods: Keep the design as simple as possible.

## Important

Uncomment the tests first.

Go to the `BankTransferTests.cs` and remove the `#if false` and `#endif`.


## Step-by-Step example:

<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>

1. **Encapsulate Balance**:
   - **Rule 2: Reveals intention** - Encapsulate the balance field to make the `Account` class responsible for its own state.
   - Refactor `Account` class to make `_balance` private and provide a `Balance` property.

   ```csharp
   internal class Account
   {
       private decimal _balance;

       public decimal Balance => _balance;
   }
   ```

   **Run the tests to ensure they stay green**.

2. **Introduce Constructor for Initial Balance**:
   - **Rule 2: Reveals intention** - Add a constructor to initialize the balance, making the class's intention clearer.
   - Modify `Account` to include a constructor.

   ```csharp
   internal class Account
   {
       private decimal _balance;

       public Account(decimal openingBalance)
       {
           _balance = openingBalance;
       }

       public decimal Balance => _balance;
   }
   ```

   Update tests to use the constructor:

   ```csharp
   public class BankTransferTests
   {
       [Fact]
       public void Should_transfer_money_between_two_accounts_and_update_balances()
       {
           var a = new Account(10);
           var b = new Account(10);
           var bankService = new BankService();
           
           bankService.Transfer(a, b, 5);

           Assert.Equal(5, a.Balance);
           Assert.Equal(15, b.Balance);
       }
       
       [Fact]
       public void Should_fail_when_account_has_insufficient_funds()
       {
           var a = new Account(10);
           var b = new Account(10);
           var bankService = new BankService();
           var act = () => bankService.Transfer(a, b, 50);
           Assert.Throws<Exception>(act);
       }
   }
   ```

   **Run the tests to ensure they stay green**.

3. **Move Validation Logic into Account Class**:
   - **Rule 4: Minimize the number of classes and methods** - Simplify the design by reducing the number of classes.
   - Move the balance validation logic into the `Account` class.

   ```csharp
   internal class Account
   {
       private decimal _balance;

       public Account(decimal openingBalance)
       {
           _balance = openingBalance;
       }

       public decimal Balance => _balance;

       public void Debit(decimal amount)
       {
           if (_balance < amount)
           {
               throw new Exception("Insufficient funds for transfer");
           }
           _balance -= amount;
       }

       public void Credit(decimal amount)
       {
           _balance += amount;
       }
   }
   ```

   Update `BankService` to use `Debit` and `Credit` methods:

   ```csharp
   internal class BankService
   {
       public void Transfer(Account from, Account to, decimal amount)
       {
           from.Debit(amount);
           to.Credit(amount);
       }
   }
   ```

   Remove `AccountFundsValidator` class since it's no longer needed.

   **Run the tests to ensure they stay green**.

4. **Introduce Custom Exception**:
   - **Rule 2: Reveals intention** - Use a custom exception to make error handling clearer and more specific.
   - Create a custom exception `InsufficientFundsException`.

   ```csharp
   internal class InsufficientFundsException : Exception
   {
       public InsufficientFundsException(string message) : base(message) { }
   }
   ```

   Update `Account` class to throw `InsufficientFundsException`:

   ```csharp
   internal class Account
   {
       private decimal _balance;

       public Account(decimal openingBalance)
       {
           _balance = openingBalance;
       }

       public decimal Balance => _balance;

       public void Debit(decimal amount)
       {
           if (_balance < amount)
           {
               throw new InsufficientFundsException("Insufficient funds for transfer");
           }
           _balance -= amount;
       }

       public void Credit(decimal amount)
       {
           _balance += amount;
       }
   }
   ```

   Update the test to expect `InsufficientFundsException`:

   ```csharp
   public class BankTransferTests
   {
       [Fact]
       public void Should_transfer_money_between_two_accounts_and_update_balances()
       {
           var a = new Account(10);
           var b = new Account(10);
           var bankService = new BankService();
           
           bankService.Transfer(a, b, 5);

           Assert.Equal(5, a.Balance);
           Assert.Equal(15, b.Balance);
       }
       
       [Fact]
       public void Should_fail_when_account_has_insufficient_funds()
       {
           var a = new Account(10);
           var b = new Account(10);
           var bankService = new BankService();
           var act = () => bankService.Transfer(a, b, 50);
           Assert.Throws<InsufficientFundsException>(act);
       }
   }
   ```

   **Run the tests to ensure they stay green**.


</details>


