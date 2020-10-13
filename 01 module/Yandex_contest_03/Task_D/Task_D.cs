using System;

partial class Program
{
    public static void Main(string[] args)
    {
        if (!Validate(Console.ReadLine(), out int num))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        Console.WriteLine(RecurrentFunction(num));
    }
}

partial class Program
{
    private static bool Validate(string input, out int num)
    {
        if (!int.TryParse(input, out num) || num < 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private static int RecurrentFunction(int n)
    {
        int[] result = new int[n + 1];
        if (n == 0)
        {
            result[0] = 3;
        }
        else
        {
            // Прошлое значение + прирост.
            result[n] = result[n - 1] + 2 * (RecurrentFunction(n - 1) + 1);
        }
        return result[n];

    }
}
