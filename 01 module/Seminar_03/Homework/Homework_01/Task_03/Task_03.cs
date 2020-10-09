using System;

namespace Task_03
{
    class Program
    {
        public static bool System(double X, double Y)
        {
            // по условию радиус круга.
            int R = 2; 
            bool flag;
            double alpha;
            alpha = Math.Asin(Y/Math.Sqrt(X*X + Y*Y));
            // max angle.
            double max = Math.PI / 4;
            // min angle.
            double min = Math.PI / -2; 
            if (Math.Sqrt(X * X + Y * Y) <= R && (alpha <= max & alpha >= min))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Введите X, а затем Y ");
                double X, Y;
                while (!double.TryParse(Console.ReadLine(), out X) |
                       !double.TryParse(Console.ReadLine(), out Y))
                {
                    Console.WriteLine("Ошибка ввода, введите x, а затем y");
                }
                bool flag = Program.System(X, Y);
                Console.WriteLine(flag);


                Console.WriteLine("Нажмте ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
