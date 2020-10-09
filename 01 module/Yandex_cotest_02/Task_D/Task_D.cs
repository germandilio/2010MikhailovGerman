using System;

partial class Program
{
    static void Main(string[] args)
    {
        double a = double.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());

        Console.WriteLine(Max(a, b));
    }
}
partial class Program
{
    private static double Max(double a, double b)
    {
        // нахождение макисмума
        if (a >= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }
}



