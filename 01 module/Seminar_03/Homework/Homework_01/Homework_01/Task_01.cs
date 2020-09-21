using System;

namespace Homework_01
{
    class Program
    {

        static void Main(string[] args)
        {


            int number, result;
            // вызов метода
            number = Program.Inkubator(out result);
            Console.WriteLine($"Ваше число: {result}, количество членов ряда {number}");
            Console.WriteLine($"1+2+3+...+{number - 2}+{number - 1}+{number}");
            Console.WriteLine("Нажмите ESC для выхода");

        }
        // метод возвращает последнее число ряда и сумму(sum % 111 == 0)
        public static int Inkubator(out int result)
        {
            int sum = 0;
            result = 0;
            int number = 0;
            for (int i = 1; i < 45; i++) // i < 45 так как сумма ряда от 1 до 45 = 1035 - число четырехзначное
            {
                sum += i;
                if (sum % 111 == 0 & sum >= 100 & sum <= 999)
                {
                    result = sum;
                    number = i;
                    break;
                }
            }

            return number;

            /* Альтернативное решение:
             * надо доработать метод(проверка N < 999 чтобы выдавало правильное значение)
             * public static int Inkubator(out int sum, out int counter)
             * {
             *     sum = 0;
             *     int N = 100;
             *     int number = 0;
             *     // counter - количество членов ряда
             *     counter = 0;
             *     do
             *     {
             *         for (int i = 1; sum <= N; i++)
             *         {
             *             sum += i;
             *             counter += 1;
             *             number = i;
             *         }
             *
             *         N += 1;
             *     } while (sum % 111 != 0 & sum != N);
             *     
             *     return number;
             *
             *
             * }
             */



        }
    }
}
