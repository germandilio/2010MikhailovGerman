using System;
using System.IO;
using System.Collections.Generic;

partial class Program
{
    private static string[] ReadCodeLines(string codePath)
    {
        string[] input = File.ReadAllLines(codePath);
        return input;
    }

    private static string[] CleanCode(string[] codeWithComments)
    {
        string[] CodewithoutComments = new string[1];
        // index показывает номер объекта в массиве, куда надо записать строку.
        int index = 0;
        // Флаг для проверки строки.
        bool commentisclose = true;
        bool flag = true;
        int position;

        foreach (var str in codeWithComments)
        {
            if (str.Contains("/*") || str.Contains("*/") || str.Contains("//"))
            {
                position = str.Length;
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (commentisclose & (str.Contains("Write(") || str.Contains("WriteLine(")))
                    {
                        if (str[i] == '"')
                        {
                            position = i;
                        }
                    }


                    if (i < position & str[i] == '/' & str[i + 1] == '*')
                    {
                        commentisclose = false;
                        flag = false;
                    }
                    if (i < position & str[i] == '/' & str[i + 1] == '/')
                    {
                        flag = false;
                    }
                    if (!commentisclose && i < position & str[i] == '*' & str[i + 1] == '/')
                    {
                        commentisclose = true;
                        flag = false;
                    }
                }

            }

            if (flag && commentisclose)
            {
                CodewithoutComments[index++] = str;
                // Увеличиваем длинну массива, для последующего добавления строки.
                Array.Resize(ref CodewithoutComments, index + 1);
            }
            // Сброс флага.
            if (commentisclose)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }


        }
        return CodewithoutComments;
    }

    private static void WriteCode(string codeFilePath, string[] codeLines)
    {
        File.AppendAllLines(codeFilePath, codeLines);
    }
}



partial class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = "code.cs";
        string outputFilePath = "cleanCode.cs";
        string[] codeWithComments = ReadCodeLines(inputFilePath);
        string[] codeWithoutComments = CleanCode(codeWithComments);

        WriteCode(outputFilePath, codeWithoutComments);
    }
}




