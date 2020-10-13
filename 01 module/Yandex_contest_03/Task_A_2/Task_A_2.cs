using System;
using System.Linq;
partial class Program
{
    private static bool TryParseInput(string inputA, string inputB, out int a, out int b)
    {
        a = 0;
        b = 0;
        if (!int.TryParse(inputA, out a) || !int.TryParse(inputB, out b) || a < 0 || b < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static void SwapMaxDigit(ref int a, ref int b)
    {
        int clone = a;
        int[] digit1 = new int[1];
        int position = 0;
        while (clone > 0)
        {
            digit1[position++] = clone % 10;
            Array.Resize(ref digit1, position + 1);
            clone /= 10;
        }
        Array.Reverse(digit1);

        // Находим максимум A.
        int maxA = digit1.Max();

        clone = b;
        int[] digit2 = new int[1];
        position = 0;
        while (clone > 0)
        {
            digit2[position++] = clone % 10;
            Array.Resize(ref digit2, position + 1);
            clone /= 10;
        }
        Array.Reverse(digit2);

        // Находим максимум B.
        int maxB = digit2.Max();

        a = 0;
        // Меняем максимальные элементы.
        for (int i = 0; i < digit1.Length; i++)
        {
            if (digit1[i] == maxA)
            {
                digit1[i] = maxB;
            }
            a = a * 10 + digit1[i];
        }

        b = 0;
        for (int i = 0; i < digit2.Length; i++)
        {
            if (digit2[i] == maxB)
            {
                digit2[i] = maxA;
            }
            b = b * 10 + digit2[i];
        }

    }

}

partial class Program
{
    public static void Main(string[] args)
    {
        if (!TryParseInput(Console.ReadLine(), Console.ReadLine(), out int a, out int b))
        {
            Console.WriteLine("Incorrect input");
        }
        else
        {
            SwapMaxDigit(ref a, ref b);

            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}

