using System;
/*
 Получить от пользователя вещественное значение – бюджет пользователя и
целое число – процент бюджета, выделенный на компьютерные игры.
Вычислить и вывести на экран сумму в рублях\евро или долларах, выделенную на компьютерные игры.
Использовать спецификаторы формата для валют.
*/
// курсы валют и сделать вывод в консоль можно в Main

namespace MS_06
{
    class Program
    {
        // метод выводит на экран сконвертированную в EUR и USD сумму
        public static void Converter(int N, int P)
        {
            double value = N * P / 100;
            // статичные курсы валют
            double USD = 74.92;
            double EUR = 88.77;
            double valueUSD = value / USD;
            double valueEUR = value / EUR;
            Console.WriteLine($"Сумма в рублях {value:C}, в долларах {valueUSD:C}, и в евро {valueEUR:C}");


        }
        static void Main(string[] args)
        {
            do
            {
                int N, P;

                Console.WriteLine("Ведите свой бюджет и % от него, котрый вы готовы потратить на игры");
                do
                {
                    Console.WriteLine("Ошибка ввода ");
                    Console.WriteLine("Ведите свой бюджет и % от него, котрый вы готовы потратить на игры");

                } while (!int.TryParse(Console.ReadLine(), out N) |
                         !int.TryParse(Console.ReadLine(), out P));
                // вызов метода 
                Program.Converter(N, P);

                Console.WriteLine("Нажмите ESC чтобы выйти");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
