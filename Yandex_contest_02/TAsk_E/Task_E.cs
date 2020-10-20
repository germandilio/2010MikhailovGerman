using System;

partial class Program
{
    static void Main(string[] args)
    {
        int input = int.Parse(Console.ReadLine());
        if (!IsInputNumberCorrect(input))
        {
            Console.WriteLine("Incorrect input");
            return;
        }
        Console.WriteLine(Factorial(input));
    }
}
partial class Program
{

    static int Factorial(int value)
    {
        // breakpoints of recursion
        if (value == 1)
        {
            return 1;
        }
        else if (value == 0)
        {
            return 1;
        }
        else
        {   // recursion
            return value * Factorial(value - 1);
        }
    }

    static bool IsInputNumberCorrect(int number)
    {
        // проверка положительности числа
        if (number >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
