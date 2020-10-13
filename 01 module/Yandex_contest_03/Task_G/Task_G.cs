using System.Globalization;
using System;
using System.Linq;
partial class Program
{
    public static void Main(string[] args)
    {
        double[] array = ReadNumbers(Console.ReadLine());

        Console.WriteLine($"{GetMin(array):F2}{Environment.NewLine}" +
                          $"{GetAverage(array):F2}{Environment.NewLine}" +
                          $"{GetMedian(array):F2}");
    }
}


partial class Program
{
    private static double GetMin(double[] array)
    {
        return array.Min();
    }

    private static double GetAverage(double[] array)
    {
        return array.Average();
    }

    private static double GetMedian(double[] array)
    {
        Array.Sort(array);
        double result = 0;
        if (array.Length % 2 == 0)
        {
            result = (array[(array.Length / 2) - 1] + array[array.Length / 2]) / 2;
        }
        else
        {
            result = array[array.Length / 2];
        }
        return result;
    }

    private static double[] ReadNumbers(string line)
    {
        string[] numb = line.Split(' ');
        double[] numbers = new double[numb.Length];
        // Парсим строки в числа в массив numbers.
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = double.Parse(numb[i]);
        }
        return numbers;
    }

}
