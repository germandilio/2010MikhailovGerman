using System;

namespace Task_04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите U: ");
            double U = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите R: ");
            double R = Convert.ToDouble(Console.ReadLine());
            // вычисление I и R
            double I = U * R;
            double P = Math.Pow(U,2) / R;

            Console.Write("Сила тока = ");
            Console.WriteLine(I);
            Console.Write("Мощность = ");
            Console.WriteLine(P);
            




        }


    }
}
