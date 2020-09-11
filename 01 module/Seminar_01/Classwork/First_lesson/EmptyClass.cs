using System;
// выводим различные форматы данных
namespace First_lesson
{
    class Task_04
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("5.0/3.0" + " Формат F" + " =" + (5.0 / 3.0).ToString("F"));
            Console.WriteLine("5.0/3.0" + " Формат F4" + " =" + (5.0 / 3.0).ToString("F4"));
            Console.WriteLine("5.0/3.0" + " Формат E" + " =" + (5.0 / 3.0).ToString("E"));
            Console.WriteLine("5.0/3.0" + " Формат E5" + " =" + (5.0 / 3.0).ToString("E5"));
            Console.WriteLine("5.0/3.0" + " Формат G" + " =" + (5.0 / 3.0).ToString("G"));
            Console.WriteLine("5.0/3.0" + " Формат G3" + " =" + (5.0 / 3.0).ToString("G3"));
            Console.WriteLine("5.0/3e10" + " Формат G3" + " =" + (5.0 / 3.0).ToString("G3"));
            Console.WriteLine("5.0/3e10" + " Формат G" + " =" + (5.0 / 3.0).ToString("G"));
            Console.WriteLine("5.0/3e10" + " Формат G" + " =" + (5.0 / 3.0).ToString("G"));



        }
    }
}