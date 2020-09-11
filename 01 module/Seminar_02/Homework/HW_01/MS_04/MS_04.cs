using System;
/*
 Получить от пользователя четырехзначное натуральное число и напечатать его цифры в обратном порядке.
*/

namespace MS_04
{
    class Program
    {   // метод вычисляет результат и записывает его в result
        public static string Result(int N)
        {
            int a1, a2, a3, a4;
            a1 = N / 1000;
            a2 = (N - a1 * 1000) / 100;
            a3 = (N - a1 * 1000 - a2 * 100) / 10;
            a4 = N % 10;
            string result = $"{a4},{a3},{a2},{a1}";
            return result;
        }

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Введите четырехзначние число");
                int N;
                // проверка ввода 
                while (int.TryParse(Console.ReadLine(), out N) | N < 1000 | N > 9999)
                {
                    Console.WriteLine("Ошибка ввода ");
                    Console.WriteLine("Введите четырехзначние число");

                }
                // вызов метода
                string result = Program.Result(N);
                Console.WriteLine(result);



                Console.WriteLine("Нажмите ESC чтобы выйти");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
