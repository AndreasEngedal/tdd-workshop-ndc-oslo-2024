# Let's practice: FizzBuzz

Implement FizzBuzz using TDD.

## How FizzBuzz work?

Write a program that for a given number prints the number, but for multiples of three print “Fizz" instead of the number and for the multiples of five print "Buzz". 
For numbers which are multiples of both three and five print “FizzBuzz".


### Example:
 - 15 should return FizzBuzz
 - 5 should return Buzz
 - 9 should return Fizz
 - 2 should return 2


## Step-by-Step example with C# and xUnit


<details>
  <summary><i>Possible Solution (Step-by-Step)</i></summary>
  

1. **Test for Non-Multiples of 3 or 5**:
   - **Test**: Check if the function returns the number itself for non-multiples.
     ```csharp
     public class FizzBuzzTests
     {
         [Fact]
         public void ReturnsNumberForNonMultiples()
         {
             Assert.Equal("1", FizzBuzz(1));
             Assert.Equal("2", FizzBuzz(2));
         }
     }
     ```
   - **Implementation**:
     ```csharp
     public class FizzBuzzClass
     {
         public static string FizzBuzz(int number)
         {
             return number.ToString();
         }
     }
     ```

2. **Test for Multiples of 3**:
   - **Test**: Check if the function returns "Fizz" for multiples of 3.
     ```csharp
     public class FizzBuzzTests
     {
         [Fact]
         public void ReturnsFizzForMultiplesOfThree()
         {
             Assert.Equal("Fizz", FizzBuzzClass.FizzBuzz(3));
             Assert.Equal("Fizz", FizzBuzzClass.FizzBuzz(6));
         }
     }
     ```
   - **Implementation**:
     ```csharp
     public class FizzBuzzClass
     {
         public static string FizzBuzz(int number)
         {
             if (number % 3 == 0)
                 return "Fizz";
             return number.ToString();
         }
     }
     ```

3. **Test for Multiples of 5**:
   - **Test**: Check if the function returns "Buzz" for multiples of 5.
     ```csharp
     public class FizzBuzzTests
     {
         [Fact]
         public void ReturnsBuzzForMultiplesOfFive()
         {
             Assert.Equal("Buzz", FizzBuzzClass.FizzBuzz(5));
             Assert.Equal("Buzz", FizzBuzzClass.FizzBuzz(10));
         }
     }
     ```
   - **Implementation**:
     ```csharp
     public class FizzBuzzClass
     {
         public static string FizzBuzz(int number)
         {
             if (number % 3 == 0)
                 return "Fizz";
             if (number % 5 == 0)
                 return "Buzz";
             return number.ToString();
         }
     }
     ```

4. **Test for Multiples of Both 3 and 5**:
   - **Test**: Check if the function returns "FizzBuzz" for multiples of both 3 and 5.
     ```csharp
     public class FizzBuzzTests
     {
         [Fact]
         public void ReturnsFizzBuzzForMultiplesOfThreeAndFive()
         {
             Assert.Equal("FizzBuzz", FizzBuzzClass.FizzBuzz(15));
             Assert.Equal("FizzBuzz", FizzBuzzClass.FizzBuzz(30));
         }
     }
     ```
   - **Implementation**:
     ```csharp
     public class FizzBuzzClass
     {
         public static string FizzBuzz(int number)
         {
             if (number % 3 == 0 && number % 5 == 0)
                 return "FizzBuzz";
             if (number % 3 == 0)
                 return "Fizz";
             if (number % 5 == 0)
                 return "Buzz";
             return number.ToString();
         }
     }
     ```

5. **Refactor if Necessary**:
   - Ensure the implementation is clean and efficient. For example, you might refactor the conditions to improve readability.
   - Tip: Adopt xUnit Theories.

</details>