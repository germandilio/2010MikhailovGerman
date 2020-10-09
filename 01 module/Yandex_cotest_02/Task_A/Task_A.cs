using System;

namespace Yandex_cotest_02
{
    class Program
    {
        static void Main(string[] args)
        {
            uint N;
            // check input.
            if (!uint.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                uint digit = 0;
                uint sum = 0;
                // отделение последней цифры.00
                while (N > 0)
                {
                    digit = N % 10;
                    // суммирование.
                    sum += digit;
                    N = N / 10;
                }
                Console.WriteLine(sum);
            }
        }
    }
}
