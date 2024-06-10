using FluentAssertions;

namespace DrivingPatterns.Tests.SquareArea;

public class SquareAreaTests
{
    [Theory]
    [InlineData(5, 25)]
    [InlineData(4, 16)]
    public void When_SideIsGiven_Expect_AreaIsReturned(int side, int expectedArea)
    {
        // Act
        var area = SquareAreaClass.Calculate(side);

        // Assert
        area.Should().Be(expectedArea);
    }
}

public class SquareAreaClass
{
    public static int Calculate(int side)
    {
        return side * side;
    }
}