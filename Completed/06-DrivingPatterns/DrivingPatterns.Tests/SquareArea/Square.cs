namespace DrivingPatterns.Tests.SquareArea;

public class Square
{
    private readonly double _sideLength;

    public Square(double sideLength)
    {
        if (sideLength < 0)
        {
            throw new ArgumentException("Side length must be non-negative.");
        }

        _sideLength = sideLength;
    }

    public double CalculateArea()
    {
        return _sideLength * _sideLength;
    }
}