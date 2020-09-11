using System;
/*
 Получить от пользователя три вещественных числа и проверить для них неравенство треугольника.
Оператор if не применять. Точность вывода три знака после запятой.
*/
// можно сделать, чтобы метод возвращал данные, а не готовую строку

namespace MS_05
{
    class Program
    {   // метод записыващий в result ответ: выпоняется ли неравенство треугольника
        public static string Result(int N1, int N2, int N3)
        {
            string result;
            result = (Math.Abs(N1) <= Math.Abs(N2) + Math.Abs(N3)) ? ("Неравенство треугольника выполняется ") : ("Неравенство треугольника не выполняется");
            return result;

        }
        static void Main(string[] args)
        {
            do
            {
                int N1, N2, N3;
                Console.WriteLine("Введите три числа ");
                do
                {
                    Console.WriteLine("Ошибка ввода");
                    Console.WriteLine("Введите три числа ");

                } while (!int.TryParse(Console.ReadLine(), out N1) |
                         !int.TryParse(Console.ReadLine(), out N2) |
                         !int.TryParse(Console.ReadLine(), out N3));
                // вызываем метод
                string result = Program.Result(N1, N2, N3);
                Console.WriteLine(result);


                Console.WriteLine("Нажмите ESC чтобы выйти");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);



        }
    }
}
