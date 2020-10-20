using System;

namespace Yandex_cotest_02
{
    class Program
    {
        static void Main(string[] args)
        {
            // флаг для проверки был ли случай неверного ввода.
            bool flag = false;
            long Value;
            long sum = 0;
            do
            {
                // проверка ввода
                if (!long.TryParse(Console.ReadLine(), out Value))
                {
                    Console.WriteLine("Incorrect input");
                    flag = true;
                }
                else
                {
                    if (Value % 2 != 0)
                    {
                        sum += Value;
                    }
                }
            } while (Value != 0);
            if (!flag)
                Console.WriteLine(sum);

        }
    }
}


