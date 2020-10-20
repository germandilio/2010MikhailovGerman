using System;
using System.IO;
using System.Linq;


partial class Program
{
    /// <summary>
    /// Метод считывает весь текст с файла.
    /// </summary>
    /// <param name="inputPath"></param>
    /// <returns></returns>
    private static string GetTextFromFile(string inputPath)
    {
        // Считываем все в одну строку.
        string inputFile = File.ReadAllText(inputPath);
        return inputFile;

    }
    /// <summary>
    /// Метод вычленияет цифры из строки.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static int GetSumFromText(string text)
    { 
        // Массив разделителей.
        char[] splitters = { '\n', '.', '!', '?', ' ', ',' };

        //  Создаем массив строк, разделенных по разделителям.
        string[] strings = text.Split(splitters);

        int numbertoConvert = 0;
        int sum = 0;
        // Пробегаем по строкам.
        foreach (var substring in strings)
        {
            if (substring != "" && !Array.Exists(splitters, i => i == substring[0]))
            {
                if (int.TryParse(substring, out numbertoConvert))
                {
                    sum += numbertoConvert;
                }
            }
           
        }

        return sum;

    }
}

partial class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.txt";
        string text = GetTextFromFile(inputPath);

        Console.WriteLine(GetSumFromText(text));
    }


}