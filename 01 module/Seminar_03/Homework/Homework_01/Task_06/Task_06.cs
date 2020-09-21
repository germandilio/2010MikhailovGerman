using System;

namespace Task_06
{
    class Program
    {   //  метод находит минимальное номер аудитории
        public static int Method(int N1, int N2, int N3)
        {   // массив номеров аудиторий без этажа
            int[] rooms = {N1 % 100, N2 % 100, N3 % 100};
            // масив этажей
            int[] floors = {N1 / 100, N2 / 100, N3 / 100};
     
            int minValue = 1000;
            int pointer = 0; // для того чтобы знать, какой элемент по счёту стал минимумом
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i] < minValue)
                {
                    minValue = rooms[i];
                    // номер этажа
                    pointer = i ;
                }
            }
            // прирепление номера этажа
            minValue = floors[pointer] * 100 + minValue;
            return minValue;



        }
        static void Main(string[] args)
        {   // повторение решения, ели нажата клавиша отличная от ESC
            do
            {
                Console.WriteLine("Введите 3 номера аудитории ");
                int N1, N2, N3;
                // проверка ввода
                while (!int.TryParse(Console.ReadLine(), out N1) |
                       !int.TryParse(Console.ReadLine(), out N2) |
                       !int.TryParse(Console.ReadLine(), out N3))    
                {
                    Console.WriteLine("Ошибка ввода, введите X ");
                }
                // вызов метода
                int minValue = Program.Method(N1, N2, N3);
                Console.WriteLine(minValue);



                Console.WriteLine("Для выхода нажмите ESC ");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
