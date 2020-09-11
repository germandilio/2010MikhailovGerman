using System;
/*
 Ввести значение x и вывести значение полинома: F(x) = 12x4 + 9x3 - 3x2 + 2x – 4.
Не применять возведение в степень. Использовать минимальное количество операций умножения.
*/

namespace Myselfwork_01
{
    class Program
    {   // вычисление значение полинома
        static double Polinom(int x)
        {
            double polinom = x * (3 * x * (x + 1) * (4 * x - 1) + 2) - 4;
            return polinom;
        }

        static void Main(string[] args)
        {   // повтор решения  
            do
            {
                Console.WriteLine("Введите x ");
                int x;
                while (!int.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine("Ошибка ввода");
                }
                double polinom;
                polinom = Program.Polinom(x);
                Console.WriteLine($"Значение полинома {polinom:F2}");

                Console.WriteLine("Для выхода нажмите ESC ");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
