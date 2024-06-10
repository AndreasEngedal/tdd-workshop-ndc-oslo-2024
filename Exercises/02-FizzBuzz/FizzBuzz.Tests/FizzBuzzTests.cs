using FluentAssertions;

namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    [Fact]
    public void When_InputIsGiven_Then_InputIsReturned()
    {
        const int expected = 1;

        var actual = FizzBuzzClass.FizzBuzz(expected);

        actual.Should().Be(expected.ToString());
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(9)]
    [InlineData(12)]
    public void When_InputIsMultipleOfThree_Then_FizzIsReturned(int input)
    {
        var actual = FizzBuzzClass.FizzBuzz(input);

        actual.Should().Be("Fizz");
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(25)]
    public void When_InputIsMultipleOfFive_Then_BuzzIsReturned(int input)
    {
        var actual = FizzBuzzClass.FizzBuzz(input);

        actual.Should().Be("Buzz");
    }

    [Theory]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(45)]
    [InlineData(60)]
    public void When_InputIsMultipleOfThreeAndFive_Then_FizzBuzzIsReturned(int input)
    {
        var actual = FizzBuzzClass.FizzBuzz(input);

        actual.Should().Be("FizzBuzz");
    }
}

public static class FizzBuzzClass
{
    public static string FizzBuzz(int input)
    {
        if (input % 15 == 0)
        {
            return "FizzBuzz";
        }

        if (input % 3 == 0)
        {
            return "Fizz";
        }

        if (input % 5 == 0)
        {
            return "Buzz";
        }

        return input.ToString();
    }
}