namespace DrivingPatterns.Tests.Factorial;

public static class FactorialCalculator
{
    public static int Calculate(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Number must be non-negative.");
        }

        return number switch
        {
            0 => 1,
            1 => 1,
            _ => number * Calculate(number - 1)
        };
    }
}