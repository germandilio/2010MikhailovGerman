using System;

partial class Program
{
    private static int[] ParseInput(string input)
    {
        string[] array = input.Split();
        int[] numbers = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            numbers[i] = int.Parse(array[i]);
        }
        return numbers;
    }

    private static int GetMaxInArray(int[] numberArray)
    {
        int max = int.MinValue;
        for (int i = 0; i < numberArray.Length; i++)
        {
            if (numberArray[i] > max)
            {
                max = numberArray[i];
            }
        }
        return max;
    }
}


partial class Program
{
    public static void Main(string[] args)
    {
        int[] numberArray = ParseInput(Console.ReadLine());

        Console.WriteLine(GetMaxInArray(numberArray));
    }
}
