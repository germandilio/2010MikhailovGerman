using System;
/*
 Введя значения коэффициентов А, В, С, вычислить корни квадратного уравнения.
 Учесть (как хотите) возможность появления комплексных корней. Оператор if не применять.
*/

namespace MS_03
{
    class Program
    {
        public static string Result(int A, int B, int C)
        {   // метод вычисляет корни и записывает готовый ответ в result  
            double D;
            D = B * B - 4 * A * C; // дикскриминант
            string result;
            result = (A != 0 & D > 0) ? ("Корни уравнения " + (-B + Math.Sqrt(D) / 2 * A).ToString("F2") + ", " + (-B - Math.Sqrt(D) / 2 * A).ToString("F2")) : ((D == 0) ? ("Корень уравнения " + (-B / 2 * A).ToString("F2")) : ("Нет корней, или же это не квадратное уравнение"));
            return result;
        }
        static void Main(string[] args)
        {   // повторение решения 
            do
            {
                Console.WriteLine("Введите значения коэффициентов А, В, С ");
                int A, B, C;

                // проверка ввода 
                while (!int.TryParse(Console.ReadLine(), out A) |
                       !int.TryParse(Console.ReadLine(), out B) |
                       !int.TryParse(Console.ReadLine(), out C))
                {
                    Console.WriteLine("Ошибка ввода ");
                    Console.WriteLine("Введите значения коэффициентов А, В, С ");

                }
                // вызов метода   
                string result = Program.Result(A, B, C);
                Console.WriteLine(result);




                Console.WriteLine("Нажмите ESC для выхода ");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
