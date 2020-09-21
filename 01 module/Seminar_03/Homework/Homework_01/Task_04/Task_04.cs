using System;

namespace Task_04
{
    class Program
    {   // метод G является математической функцией(из условия), возвращает result 
        public static double G(double X, double Y)
        {
            double result;
            if (X < Y & X > 0)
            {
                result = X + Math.Sin(Y);
            }
            else if (X > Y & X < 0)
            {
                result = Y - Math.Cos(X);
            }
            else
            {
                result = 0.5 * X * Y;
            }
            return result;
        }
        static void Main(string[] args)
        {   // повторение программы при нажатии клавиши отличной от ESC
            do
            {
                Console.WriteLine("Введите X, а затем Y ");
                double X, Y;
                // проверка ввода
                while (!double.TryParse(Console.ReadLine(), out X) |
                       !double.TryParse(Console.ReadLine(), out Y))
                {
                    Console.WriteLine("Ошибка ввода, введите x, а затем y");
                }
                double result = Program.G(X, Y);
                Console.WriteLine($"{result:F3}");


                Console.WriteLine("Нажмте ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
