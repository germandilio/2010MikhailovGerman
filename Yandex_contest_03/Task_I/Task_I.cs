using System;
using System.IO;
using System.Linq;


partial class Program
{
    private static string GetTextFromFile(string inputPath)
    {
        string inputFile = File.ReadAllText(inputPath);
        return inputFile;

    }

    private static int GetSumFromText(string text)
    {

        // Массив разделителей.
        string[] splitters = { "\n", ".", "!", "?", " ", "," };
        //Строка для сбора числа.
        string numberToConvert = string.Empty;

        int gtr = 0;
        int number = 0;
        int sum = 0;

        for (int k = 0; k < text.Length; k++)
        {

            // Если левый символ сплиттер или это первый элемент.
            if (k == 0 || Array.Exists(splitters, i => i == text[k - 1].ToString()))
            {
                // Это число.
                if (text[k] >= '0' && text[k] <= '9')
                {
                    // Справа сплиттер и это число или это последний элемент и число.
                    if (k == text.Length - 1 || Array.Exists(splitters, i => i == text[k + 1].ToString()))
                    {
                        bool tryparse = int.TryParse(text[k].ToString(), out number);
                        if (tryparse)
                        {
                            // Прибавляем
                            sum += number;
                            // Debug
                            Console.WriteLine(sum);
                        }
                    }
                    // Справа тоже число
                    else
                    {

                        //  собираем число из цифр.
                        while (!Array.Exists(splitters, i => i == text[k].ToString()))
                        {
                            numberToConvert += text[k];
                            k++;
                        }
                        bool tryparse = int.TryParse(numberToConvert, out number);
                        if (tryparse)
                        {
                            // Прибавляем
                            sum += number;
                            // Debug
                            Console.WriteLine(sum);
                        }
                        numberToConvert = string.Empty;
                        k--;

                    }


                }//это число


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


// !int.TryParse(text[k + 1], out gtr)