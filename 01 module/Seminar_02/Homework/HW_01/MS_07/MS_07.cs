using System;
/*
 Написать программу с использованием двух методов. Первый метод возвращает
дробную и целую часть числа. Второй метод возвращает квадрат и корень из числа.
В основной программе пользователь вводит дробное число. Программа должна вычислить,
если это возможно, значение корня, квадрата числа, выделить целую и дробную часть из числа.
*/

namespace MS_07
{
    class Program
    {   // метод возвращает дробную и целую часть числа
        public static string Converter_int_and_float(float N)
        {
            float res1 = N % 1;
            float res2 = (int)(N + 0.5);
            string result = $"Целая часть числа {N:F2} = {res2:F2}, дробная = {res1:F2}";
            return result;
        }
        // метод вычисляет квадрат и корень из числа, если такое возможно
        public static string Converter_SQRT_and_Pow(float N)
        {
            double result1 = Math.Pow(N, 2);
            double result2 = -1;
            if (N > 0)
            {
                result2 = Math.Sqrt(N);

            }

            string result = (result2 == -1) ? ($"Невозможно извлечь корень, квадрат числа {N:F2} = {result1:F2}") : ($"Корень числа {N:F2} = {result2:F2}, квадрат числа {N:F2} = {result1:F2} ");
            return result;
        }

        static void Main(string[] args)
        {   // повторение решения  
            do
            {
                float N;
                Console.WriteLine("Введите число ");
                do
                {
                    Console.WriteLine("Ошибка ввода");
                    Console.WriteLine("Введите число ");
                } while (!float.TryParse(Console.ReadLine(), out N));

                string str1, str2;
                // вызов методов 
                str1 = Program.Converter_int_and_float(N);
                str2 = Program.Converter_SQRT_and_Pow(N);
                Console.WriteLine(str1);
                Console.WriteLine(str2);




                Console.WriteLine("Нажмите ESC чтобы выйти");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
