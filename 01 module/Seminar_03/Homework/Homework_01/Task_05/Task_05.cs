using System;

namespace Task_05
{
    class Program
    {   // метод G является математической функцией(из условия), возвращает result 
        public static double G(double X)
        {
            double result;
            if (X <= 0.5)
            {
                result = Math.Sin(Math.PI / 2);
            }
            else
            {
                result = Math.Sin(Math.PI * (X - 1) / 2);
            }
            return result;
        }
        static void Main(string[] args)
        {   // повторение программы при нажатии клавиши отличной от ESC
            do
            {
                Console.WriteLine("Введите X ");
                double X;
                // проверка ввода
                while (!double.TryParse(Console.ReadLine(), out X))
                {
                    Console.WriteLine("Ошибка ввода, введите X ");
                }
                double result = Program.G(X);
                Console.WriteLine($"{result:F3}");


                Console.WriteLine("Нажмте ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
