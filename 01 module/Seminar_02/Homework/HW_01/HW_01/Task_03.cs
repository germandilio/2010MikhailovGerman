using System;

/* Задача на применение тернарной операции. 
(Не применять оператор if.)
Написать метод, так обменивающий значения трех переменных x, y, z, чтобы выполнилось требование: x <= y <= z. 

В основной программе, вводить значения трех переменных и упорядочивать их с помощью обращения к написанному методу. 

Для выхода из программы вводите ESC, для повторения решения – любую другую клавишу. 
*/




namespace HW_01
{
    class Program
    {
        // метод печатает числа в порядке возрастания 
        static  void Converter(double x, double y, double z)
        {
            double a1 = x, a2 = y, a3 = z;
            double middle, max, min;
            min = a1 > a2 ? (a2 > a3 ? a3 : a2) : (a1 > a3 ? a3 : a1);
            max = a1 > a2 ? (a1 > a3 ? a1 : a3) : (a2 > a3 ? a2 : a3);
            middle = a2 > a3 ? (a3 > a1 ? a3 : (a1 > a2 ? a2 : a1)) : (a2 > a1 ? a2 : (a3 > a1 ? a1 : a3));
            Console.WriteLine($"x,y,z = {min:F2},{middle:F2},{max:F2}");
        }

        // считывает входные x, y, z и выводит результат
        static void Main(string[] args)
        {   // повторение решения, если нажата клавиша не ESC
            do
            {   // проверка ввода
                double x, y, z;
                do
                {
                    Console.WriteLine("Введите x ");

                } while (!double.TryParse(Console.ReadLine(), out x));
                do
                {
                    Console.WriteLine("Введите y ");

                } while (!double.TryParse(Console.ReadLine(), out y));
                do
                {
                    Console.WriteLine("Введите z ");

                } while (!double.TryParse(Console.ReadLine(), out z));

                // вызываем метод
                Program.Converter(x, y, z);



                Console.WriteLine("Нажмите ESC для выхода");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

    }
}
