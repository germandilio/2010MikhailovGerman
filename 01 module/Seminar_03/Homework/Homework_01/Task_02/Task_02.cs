using System;

namespace Task_02
{
    class Program
    {   // метод создает массив на основе стоки, разворачивает ее и создает строку,
        // в которую помещает массив
        public static string Reverse(string s)
        {
            char[] s_to_array = s.ToCharArray();
            Array.Reverse(s_to_array);
            s = new string(s_to_array);
            return s;
            
        }
        
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Введите число ");
                int N;
                string s = Console.ReadLine();
                // проверка ввода, но использовать будем сроку
                while (!int.TryParse(s, out N))
                {
                    Console.WriteLine("Ошибка ввода, введите число ");
                }
                Console.WriteLine(Program.Reverse(s));


                Console.WriteLine("Нажмите ESC для выхода");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
