using System;

partial class Program
{
    private static int[] ParseInput(string input)
    {
        string[] numbers = input.Split();
        int[] numb = new int[numbers.Length];

        // Заполняем массив числами(парсим).
        for (int i = 0; i < numbers.Length; i++)
        {
            numb[i] = int.Parse(numbers[i]);
        }

        return numb;
    }

    private static int GetNumberOfEqualElements(int[] first, int[] second)
    {
        int counter = 0;
        for (int i = 0; i < first.Length; i++)
        {
            for (int k = 0; k < second.Length; k++)
            {
                // Сравниваем элемент из первого массива со всеми элементами второго.
                if (first[i] == second[k])
                {
                    counter++;
                    break;
                }
            }
        }
        return counter;
    }
}


partial class Program
{
    public static void Main(string[] args)
    {
        int[] first = ParseInput(Console.ReadLine());
        int[] second = ParseInput(Console.ReadLine());

        Console.WriteLine(GetNumberOfEqualElements(first, second));
    }
}


