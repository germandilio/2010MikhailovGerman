using System;
/* 
Выделение отдельных цифр натурального числа

Задача. 
Ввести трехзначное натуральное число и напечатать его цифры "столбиком".
*/


namespace Task_05
{
    class Program
    {
        static void Main(string[] args)
        {   // повторение решения, если нажата клавиша не ESC
            do
            {
                int N;
                do
                {
                    Console.WriteLine("Введите число ");
                } while (!int.TryParse(Console.ReadLine(), out N));
                string str = $"{N}";
                foreach (char lenght in str)
                {
                    Console.WriteLine(lenght);
                }


                Console.WriteLine("Нажмите ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }
    }
}
