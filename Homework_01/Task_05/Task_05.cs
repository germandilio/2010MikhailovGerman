using System;

namespace Task_05
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = Console.ReadLine();
            double leg1 = Convert.ToDouble(str1);

            string str2 = Console.ReadLine();
            double leg2 = Convert.ToDouble(str2);
            
            double hypotenuse = Math.Sqrt(Math.Pow(leg1, 2) + Math.Pow(leg2, 2));
            Console.WriteLine("Гипотенуза = ");
            Console.WriteLine(hypotenuse);
        }
    }
}
