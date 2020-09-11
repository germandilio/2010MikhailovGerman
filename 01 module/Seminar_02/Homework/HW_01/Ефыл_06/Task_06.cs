using System;

namespace Task_06
{
    class Program
    {
        static void Main(string[] args)
        {   // повтор решения, если клавиша отлична от ESC
            do
            {
                int R = 10;

                // проверка входных данных
                Console.WriteLine("Введите координату x ");
                int x;
                while (!int.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine("Ошибка ввода");
                }
                Console.WriteLine("Введите координату y ");

                int y;
                while (!int.TryParse(Console.ReadLine(), out y))
                {
                    Console.WriteLine("Ошибка ввода");
                }
                // вычисление результата
                string result;
                result = x * x + y * y > R * R ? "Точка вне круга!" : "Точка внутри круга!";
                Console.WriteLine(result);

                Console.WriteLine("Нажмите ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
