using System;

partial class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        if (Validate(n))
        {
            Console.WriteLine(GetPerfectNumber(n));
        }
        else
            Console.WriteLine("Incorrect input");
    }
}
partial class Program
{

    // Проверка входных чисел на корректность.
    static bool Validate(int a)
    {
        return a > 0;
    }

    static int GetPerfectNumber(int a)
    {
        int result = a;
        // массив совершенных чисел
        int[] PerfectNumbers = new int[6];
        PerfectNumbers[0] = 6;
        PerfectNumbers[1] = 28;
        PerfectNumbers[2] = 496;
        PerfectNumbers[3] = 8128;
        PerfectNumbers[4] = 33550336;
        for (int i = 0; i < PerfectNumbers.Length; i++)
        {
            if (a <= PerfectNumbers[i])
            {
                result = PerfectNumbers[i];
                break;
            }
        }     
        return result;
    }

}
