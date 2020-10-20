using System;

partial class Program
{
    public static void Main(string[] args)
    {
        if (!ValidateNumber(out int n) ||
            !ReadNumbers(n, out int[] array))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        double averageNumber = GetAverage(array);
        int countAboveAverage = GetCountGreaterThanValue(array, averageNumber);

        Console.WriteLine(countAboveAverage);
    }

}

partial class Program
{
    /// <summary>
    /// Метод рассчитывает коилчество чисел, превосзодящих среднее.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="average"></param>
    /// <returns></returns>
    private static int GetCountGreaterThanValue(int[] array, double average)
    {
        int counter = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > average)
            {
                counter++;
            }
        }
        return counter;
    }
    /// <summary>
    /// Метод рассчитывает корректность числа.
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private static double GetAverage(int[] array)
    {
        double sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum / array.Length;
    }
    /// <summary>
    /// Проверка корректности числа.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private static bool ValidateNumber(out int n)
    {
        n = 0;
        string input = Console.ReadLine();
        if (!int.TryParse(input, out n) || n < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Метод считывает числа и записывает в массив. В случае неверного ввода возвращает false.
    /// </summary>
    /// <param name="n">Количество элементов</param>
    /// <param name="array">Массив</param>
    /// <returns></returns>
    private static bool ReadNumbers(int n, out int[] array)
    {
        array = new int[n];
        int inputnumber;

        bool flag = true;
        for (int i = 0; i < n; i++)
        {
            if (!ValidateNumber(out inputnumber))
            {
                flag  = false;
            }
            else
            {
                array[i] = inputnumber;
            }
        }
        return flag;

    }
}
