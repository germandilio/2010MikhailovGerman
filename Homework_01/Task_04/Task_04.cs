using System;

namespace Task_04
{
    class Program
    {
        static void Main(string[] args)
        {
            double U = Convert.ToDouble(Console.ReadLine());

            double R = Convert.ToDouble(Console.ReadLine());

            double I = U * R;
            double P = Math.Pow(U,2) / R;

            Console.WriteLine("Сила тока = ");
            Console.WriteLine(I);
            Console.WriteLine("Мощность = ");
            Console.WriteLine(P);
            




        }


    }
}
