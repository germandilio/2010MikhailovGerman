using System;

namespace Task_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число от 32 до 127");
            string str = Console.ReadLine();
            int Code;
            bool a = int.TryParse(str, out Code);
            Console.WriteLine((char)Code);
         
        }
    }
}
