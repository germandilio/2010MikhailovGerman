using System;

namespace Task_05
{
    class Program
    {
        static void Main(string[] args)
        {
            double leg1 = Convert.ToDouble(Console.ReadLine());

            double leg2;
            bool a = double.TryParse(Console.ReadLine(), out leg2);
            
            double hypotenuse = Math.Sqrt(Math.Pow(leg1, 2) + Math.Pow(leg2, 2));
            Console.WriteLine("Гипотенуза = " + hypotenuse);
        }
    }
}
