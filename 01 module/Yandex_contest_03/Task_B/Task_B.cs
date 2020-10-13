using System;

partial class Program
{
    public static void Main(string[] args)
    {
        GetLetterDigitCount(Console.ReadLine(), out int digitCount, out int letterCount);

        Console.WriteLine(digitCount);
        Console.WriteLine(letterCount);
    }
}

partial class Program
{
    private static void GetLetterDigitCount(string line, out int digitCount, out int letterCount)
    {
        digitCount = 0;
        letterCount = 0;

        foreach (var symbol in line)
        {
            if (symbol >= '0' && symbol <= '9')
            {
                digitCount++;
            }
            else if ((symbol >= 'a' & symbol <= 'z') || (symbol >= 'A' & symbol <= 'Z'))
            {
                letterCount++;
            }
        }
    }
}