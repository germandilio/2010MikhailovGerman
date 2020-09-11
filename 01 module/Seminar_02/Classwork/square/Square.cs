using System;

namespace square
{
    class Program
    {   // метод вычисляет площадь круга с радиусом r
        public static double S(double r)
        {
            return Math.PI * r * r;

        }

        // метод вычисляет длину окружности 
        public static double L(double r)
        {
            return Math.PI * 2 * r;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите значение радиуса");
            double radius;

            //проверка корректности ввода данных
            if (!double.TryParse(Console.ReadLine(), out radius) || radius <= 0)
            {
                Console.WriteLine("Введите корректные данные");
                return;
            }
            double S = Program.S(radius);
            double L = Program.L(radius);
            Console.WriteLine($"Площадь круга = {S:F2}");
            Console.WriteLine($"Длина окружности= {L:F2}");


        }
    }
}
