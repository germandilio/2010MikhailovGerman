using System;

namespace Yandex_contest
{
    class Program
    {   /*
         * метод округляет число до ближайшего нечётного в случае равноудаленности от двух соседних чисел,
         * иначе по правилам математики
         */
        public static int Round(double N)
        {
            int Number;
            // если число равноудалено от двух целых
            if (N * 10 % 10 == 5 | N * 10 % 10 == -5)
            {
                if ((int)N % 2 == 0 & N >= 0) //целая часть чётная и N положительное
                {
                    Number = (int)N + 1;
                }
                else if ((int)N % 2 == 0 & N < 0)
                {
                    Number = (int)N - 1;
                }
                else
                { 
                    Number = (int)N;
                }
            }
            else
            {
                if (N >= 0)
                {
                    Number = (int)(N + 0.5);

                }
                else
                {
                    Number = (int)(N - 0.5);
                }
            }
            return Number;
        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            double N;
            if (!double.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                int Number = Program.Round(N);
                Console.WriteLine(Number);

            }


        }
    }
}
