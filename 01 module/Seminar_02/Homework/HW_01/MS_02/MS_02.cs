using System;
/*
 Ввести натуральное трехзначное число Р. Найти наибольшее целое число, которое можно получить,
переставляя цифры числа Р.
*/

namespace MS_02
{
    class Program
    {   // метод возвращает макисмальное число, которое можно получить предсталяя цифры числа P
        static int MaxValue(int P)
        {
            int max, middle, min;
            int a1 = P / 100;
            int a2 = (P - a1 * 100) / 10;
            int a3 = P - (a1 * 100) - (a2 * 10);
            min = a1 > a2 ? (a2 > a3 ? a3 : a2) : (a1 > a3 ? a3 : a1);
            max = a1 > a2 ? (a1 > a3 ? a1 : a3) : (a2 > a3 ? a2 : a3);
            middle = a2 > a3 ? (a3 > a1 ? a3 : (a1 > a2 ? a2 : a1)) : (a2 > a1 ? a2 : (a3 > a1 ? a1 : a3));
            P = (max * 100) + (middle * 10) + min;
            return P;
        }
        static void Main(string[] args)
        {   // повторение решения при нажатии клавиши отличной от ESC
            do
            {
                int P;
                Console.WriteLine("Введите число от 100 до 999 ");
                // проверка ввода
                while (!int.TryParse(Console.ReadLine(), out P) | P < 100 | P > 999)
                {
                    Console.WriteLine("Ошибка ввода");
                    Console.WriteLine("Введите число от 100 до 999 ");

                }
                P = Program.MaxValue(P);
                Console.WriteLine($"Макисмальное число {P}");



                Console.WriteLine("Для выхода нажмите ESC ");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
