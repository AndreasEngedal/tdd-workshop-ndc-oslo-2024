namespace FizzBuzz;

public static class FizzBuzzGame
{
    public static string Play(int number) =>
        number switch
        {
            _ when number.IsDivisibleBy(15)
                => "FizzBuzz",
            _ when number.IsDivisibleBy(3)
                => "Fizz",
            _ when number.IsDivisibleBy(5)
                => "Buzz",
            _ => number.ToString()
        };

    private static bool IsDivisibleBy(
        this int i, int divisibleBy)
        => i % divisibleBy == 0;
}