using System;

partial class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        if (Validate(n))
            Console.WriteLine(DivisorsSum(n));
        else
            Console.WriteLine("Incorrect input");
    }
}
partial class Program
{
    static bool Validate(int n)
    {   // проверка отрицательности числа
        if (n < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    static int DivisorsSum(int n)
    {
        int sum = 0;
        // пройдемся по всем числам от 1 до n
        for (int i = 1; i < n; i++)
        {   // если делится нацело
            if (n % i == 0)
            {
                sum += i;
            }
        }
        return sum;
    }
}
