using FluentAssertions;

namespace StringCalculatorTests;

public class StringCalculatorTests
{
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("1,2", 3)]
    [InlineData("1,2,3", 6)]
    public void When_ValidInputIsGiven_Then_ReturnSumOfInput(string input, int expectedSum)
    {
        var actual = StringCalculator.Add(input);

        actual.Should().Be(expectedSum);
    }
}

public static class StringCalculator
{
    public static object Add(string input)
    {
        var numbers = input.Split(',');
        return numbers.Sum(x => int.Parse(x));
    }
}