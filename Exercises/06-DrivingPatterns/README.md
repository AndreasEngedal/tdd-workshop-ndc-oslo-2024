# Let's practice: Driving Patterns

1. [Drive a Square Area calculator using the “Fake It” pattern](#square-area)
2. [Drive a Factorial calculator using the “Triangulate” pattern](#factorial)

## Square Area

Create a Method to calculate the Area of a Square.
The method takes the length of a side of a square as an input and returns the area of the square.

[Optional] Validate the input.

### Step-by-Step example:


<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>
  

1. **Set Up Your Project**:
   - Create a test class named `SquareAreaCalculatorTests`.

2. **Write the First Test**:
   - Start by writing a test that checks if `CalculateSquareArea` returns a hard-coded value for a specific input.
     ```csharp
     public class SquareAreaCalculatorTests
     {
         [Fact]
         public void ReturnsAreaForSideLength1()
         {
             Assert.Equal(4, SquareAreaCalculator.CalculateSquareArea(2));
         }
     }

3. **Run the Test and see it fail**.

4. **Implement the method**:
   - Implement the Method to return the hard-coded value.
     ```csharp
     public class SquareAreaCalculator
     {
         public static int CalculateSquareArea(int sideLength)
         {
             return 4; // Fake it
         }
     }
     ```

5. **Make the Test Pass**:
   - Run the test and ensure it passes.


6. **Generalize the Solution**:
   - Update the implementation to use the general formula for the area of a square, `sideLength * sideLength`.
     ```csharp
     public class SquareAreaCalculator
     {
         public static int CalculateSquareArea(int sideLength)
         {
             return 2 * 2;
         }
     }
    ```
   - Run the test and ensure it passes.
7. **Gradually replace with argument**: 
   - Update the implementation.
     ```csharp
     public class SquareAreaCalculator
     {
         public static int CalculateSquareArea(int sideLength)
         {
             return sideLength * sideLength;
         }
     }
     ```
   - Run the test and ensure it passes.
   - Replace the other argument.
     ```csharp
     public class SquareAreaCalculator
     {
         public static int CalculateSquareArea(int sideLength)
         {
             return sideLength * sideLength;
         }
     }
     ```
   - Run the test and ensure it passes.

8. **Refactor and Clean Up**:
   - Ensure all tests pass.
   - Refactor the code to improve readability and maintainability if needed.
   - Remove any unnecessary code or comments.

</details>


## Factorial

Implement a factorial calculator using Test-Driven Development (TDD) and the “Triangulate” pattern. 
The goal is to develop the functionality step by step, starting with specific cases and generalizing the implementation incrementally.

Create a function that takes an integer `number` and returns the factorial of that number. The factorial of a number \( n \) (denoted as \( n! \)) is defined as:
- \( 0! = 1 \)
- \( 1! = 1 \)
- \( n! = n \times (n-1)! \) for \( n \geq 2 \)

### Step-by-Step example:


<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>
  

1. **Set Up Your Tests**:
   - Create a test class named `FactorialCalculatorTests`.

2. **Write the First Test**:
   - Start by writing a test that checks if `Calculate` returns the correct value for the base case `number = 0`.
     ```csharp
     public class FactorialCalculatorTests
     {
         [Theory]
         [InlineData(0, 1)]
         public void Returns_factorial_for_a_given_number(int number, int expected)
         {
             var result = FactorialCalculator.Calculate(number);
             Assert.Equal(expected, result);
         }
     }
     ```
   - Implement the function to return the hard-coded value for `number = 0`.
     ```csharp
     public static class FactorialCalculator
     {
         public static int Calculate(int number)
         {
             return 1; // Fake it for number = 0
         }
     }
     ```

3. **Make the Test Pass**:
   - Run the test and ensure it passes.

4. **Add Another Test**:
   - Add a test for another base case, `number = 1`.
     ```csharp
     public class FactorialCalculatorTests
     {
         [Theory]
         [InlineData(0, 1)]
         [InlineData(1, 1)]
         public void Returns_factorial_for_a_given_number(int number, int expected)
         {
             var result = FactorialCalculator.Calculate(number);
             Assert.Equal(expected, result);
         }
     }
     ```
   - Adjust the implementation to handle this case.
     ```csharp
     public static class FactorialCalculator
     {
         public static int Calculate(int number)
         {
             if (number == 0) return 1;
             if (number == 1) return 1;
             return 1; // Default fake value for other cases
         }
     }
     ```

5. **Triangulate with Another Test**:
   - Add a test for `number = 2` to start generalizing the solution.
     ```csharp
     public class FactorialCalculatorTests
     {
         [Theory]
         [InlineData(0, 1)]
         [InlineData(1, 1)]
         [InlineData(2, 2)]
         public void Returns_factorial_for_a_given_number(int number, int expected)
         {
             var result = FactorialCalculator.Calculate(number);
             Assert.Equal(expected, result);
         }
     }
     ```
   - Update the implementation to handle this case by introducing the factorial formula.
     ```csharp
     public static class FactorialCalculator
     {
         public static int Calculate(int number)
         {
             if (number == 0 || number == 1) return 1;
             return number * Calculate(number - 1);
         }
     }
     ```

6. **Generalize Further with More Tests**:
   - Add tests for larger values of `number`, such as `number = 3`, `number = 4`, etc.
     ```csharp
     public class FactorialCalculatorTests
     {
         [Theory]
         [InlineData(0, 1)]
         [InlineData(1, 1)]
         [InlineData(2, 2)]
         [InlineData(3, 6)]
         [InlineData(4, 24)]
         [InlineData(5, 120)]
         public void Returns_factorial_for_a_given_number(int number, int expected)
         {
             var result = FactorialCalculator.Calculate(number);
             Assert.Equal(expected, result);
         }
     }
     ```
   - The implementation already supports these cases, but writing the tests ensures correctness.

7. **Refactor and Optimize**:
   - Ensure all tests pass.
   - Refactor the code to improve readability and performance if needed.


</details>