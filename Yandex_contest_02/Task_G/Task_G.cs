using System;

partial class Program
{
    static void Main(string[] args)
    {
        double a = double.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());
        double c = double.Parse(Console.ReadLine());

        Console.WriteLine(MaxOfThree(a, b, c));
    }
}
partial class Program
{

    private static double MaxOfThree(double a, double b, double c)
    {
        double max = 0;
        // нахождение максимума из трех переменных
        if (a > b)
        {
            if (a > c)
            {
                max = a;
            }
            else
            {
                max = c;
            }
        }
        else
        {
            if (b > c)
            {
                max = b;
            }
            else
            {
                max = c;
            }
        }
        // возвращаемое значение
        return max;
    }
}


