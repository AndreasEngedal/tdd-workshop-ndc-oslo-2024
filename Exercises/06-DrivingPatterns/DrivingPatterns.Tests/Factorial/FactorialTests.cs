using FluentAssertions;

namespace DrivingPatterns.Tests.Factorial;

public class FactorialTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(4, 24)]
    [InlineData(5, 120)]
    public void When_InputIsGiven_Then_FactorialIsReturned(int input, int expectedFactorial)
    {
        // Act
        var actual = FactorialClass.Calculate(input);

        // Assert
        actual.Should().Be(expectedFactorial);
    }
}

public static class FactorialClass
{
    public static int Calculate(int input)
    {
        if (input is 0 or 1)
            return 1;

        return input * Calculate(input - 1);
    }
}