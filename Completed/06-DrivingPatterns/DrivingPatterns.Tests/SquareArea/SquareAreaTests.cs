using FluentAssertions;

namespace DrivingPatterns.Tests.SquareArea;

public class SquareAreaTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 4)]
    [InlineData(3, 9)]
    [InlineData(4, 16)]
    public void Calculate_area_based_on_side_length(double sideLength, double expected)
    {
        var square = new Square(sideLength);

        var result = square.CalculateArea();

        result.Should().Be(expected);
    }

    [Fact]
    public void Throws_argument_exception_when_side_length_is_negative()
    {
        Action act = () => new Square(-1);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Side length must be non-negative.");
    }
}