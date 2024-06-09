using FluentAssertions;

namespace DrivingPatterns.Tests.Factorial;

public class FactorialCalculatorTests
{
    [Fact]
    public void GivenNegativeNumber_ThenThrowsArgumentException()
    {
        Action act = () => FactorialCalculator.Calculate(-1);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Number must be non-negative.");
    }

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

        result.Should().Be(expected);
    }
}