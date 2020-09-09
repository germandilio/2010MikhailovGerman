using System;

namespace Seminar_02
{
    class Program
    {
        public static int Fibonachi(double N)
        {
            // метод возвращает N-ое число Фибоначи и округляет его
            double b = (1 + Math.Sqrt(5)) / 2;
            double result = (Math.Pow(b, N) - Math.Pow(-b, -N)) / (2 * b - 1);
            int Result = (int)(result + 0.5);
            return Result;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите n ");
            uint N;
            //проверка корректности ввода данных 
            if (!uint.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Ошибка ввода данных ");
            }
            else
            {
                int Result = Program.Fibonachi(N);
                Console.WriteLine(Result);
            }


        }
    }
}

