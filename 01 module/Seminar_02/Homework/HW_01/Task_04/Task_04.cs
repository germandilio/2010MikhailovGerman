using System;
/* Задача 4. 
Написать метод с целочисленным параметром, определяющий является ли значение аргумента
кодом цифры, кодом буквы русского алфавита (прописной либо строчной), или ни тем и ни другим.
В основной программе, вводя целые числа, выводить сообщения о них: «Это цифра!», «Это буква!»,
«Это ни буква, ни цифра!». Для выхода из программы – ESC, для повторения решения - любой символ.
При анализе цифрового кода использовать тернарную операцию. Строку сообщения может возвращать метод,
либо метод возвращает признак, а строку формирует основная программа 
*/



namespace Task_04
{
    class Program
    {   // метод возвращает строку, которую надо вывести в ответ
        static string Converter(int code)
        {
            string Report;
            Report = code <= (int)'9' && code >= (int)'0' ? "Это цифра: " + (char)code
                : code <= (int)'Я' && code >= (int)'А' ? "Это прописная буква: " + (char)code
                : code <= (int)'я' && code >= (int)'а' ? "Это строчная буква: " + (char)code
                : "Неизвестный символ!";
            return Report;

        }
        static void Main(string[] args)
        {   // повторение решения, если нажата клавиша не ESC
            do
            {
                Console.WriteLine("Введите число ");
                int code;
                while (!int.TryParse(Console.ReadLine(), out code))
                {
                    Console.WriteLine("Ошибка ввода");

                }

                string report;
                report = Program.Converter(code);
                Console.WriteLine(report);

                Console.WriteLine("Нажмите ESC для выхода");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
