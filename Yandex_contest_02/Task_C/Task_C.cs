using System;

namespace Program_02
{
    class Program
    {
        static void Main(string[] args)
        {
            // проверка ввода.
            int a, b;
            if (!int.TryParse(Console.ReadLine(), out a) ||
                !int.TryParse(Console.ReadLine(), out b) || a >= b)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                // создаем массив длины промежутка "b - a".
                int[] array = new int[b - a];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i + a;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] % 2 == 0)
                    {
                        Console.WriteLine(array[i]);
                    }
                }
            }

        }
    }
}

