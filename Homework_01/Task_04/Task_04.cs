using System;

namespace Task_04
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = Console.ReadLine();
            double U = Convert.ToDouble(str1);

            string str2 = Console.ReadLine();
            double R = Convert.ToDouble(str2);

            double I = U * R;
            double P = Math.Pow(U,2) / R;

            Console.WriteLine("Сила тока = ");
            Console.WriteLine(I);
            Console.WriteLine("Мощность = ");
            Console.WriteLine(P);
            




        }


    }
}
