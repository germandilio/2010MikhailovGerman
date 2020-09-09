using System;

namespace Task_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите x, y, z");
            double x;
            double y;
            double z;
            // проверка корректности ввода данных 
            if (!double.TryParse(Console.ReadLine(), out x) ||
                !double.TryParse(Console.ReadLine(), out y) ||
                !double.TryParse(Console.ReadLine(), out z)     )
            {
                Console.WriteLine("Ошибка ввода ");
                return;
            }
            
            double tmp = 0;
            if (y > z)
            {   tmp = z;
                z = y;
                y = tmp;
            }
            if (x > y)
            {
                tmp = x;
                x = y;
                y = tmp;
            }
            Console.WriteLine(x.ToString("F2"));
            Console.WriteLine(y.ToString("F2"));
            Console.WriteLine(z.ToString("F2"));



        }
    }
}
