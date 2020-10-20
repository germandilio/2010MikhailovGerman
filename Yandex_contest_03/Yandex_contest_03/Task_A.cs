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
        string input = a.ToString();
        char[] numb1 = input.ToCharArray();
        int[] numbers1 = new int[numb1.Length];
        // Debug
        Array.ForEach(numbers1, x => Console.Write(x));
        Console.WriteLine();


        // Парсим каждое число из numb1 в массив numbers1.
        for (int i = 0; i < numbers1.Length; i++)
        {
            numbers1[i] = int.Parse(numb1[i].ToString());
        }
        // Находим максимум A.
        int maxA = numbers1.Max();
        // Debug
        Console.WriteLine(maxA);



        input = b.ToString();
        char[] numb2 = input.ToCharArray();
        int[] numbers2 = new int[numb2.Length];
        // Debug
        Array.ForEach(numbers2, x => Console.Write(x));
        Console.WriteLine();



        // Парсим каждое число из numb1 в массив numbers1.
        for (int i = 0; i < numbers2.Length; i++)
        {
            numbers2[i] = int.Parse(numb2[i].ToString());
        }
        // Находим максимум A.
        int maxB = numbers2.Max();
        // Debug
        Console.WriteLine(maxB);



        // Меняем максимальные элементы.
        for (int i = 0; i < numbers1.Length; i++)
        {
            if (numbers1[i] == maxA)
            {
                numbers1[i] = maxB;
            }
        }
        for (int i = 0; i < numbers2.Length; i++)
        {
            if (numbers1[i] == maxA)
            {
                numbers1[i] = maxB;
            }
        }

        int clone = a;
        int digit1[] = new int[1];
        int position = 0;
        while (clone > 0)
        {
            digit[position++] = clone % 10;
            Array.Resize(ref digit, position + 1);
            clone /= 10;
        }
        Array.Reverse(digit1);

        string a1 = numbers1.ToString();
        string b1 = numbers2.ToString();

        //a = int.Parse(a1);
        //b = int.Parse(b1);

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
