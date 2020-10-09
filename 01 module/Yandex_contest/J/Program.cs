/*using System;

// программа выводит 3 раза строку s
namespace Yandex_contest
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            // программа выводит 3 раза строку s
            Console.WriteLine(s);
            Console.WriteLine(s);
            Console.WriteLine(s);

        }
    }
}

using System;

// программа выводи разность a и b
namespace Yandex_contest
{
    class Program
    {
        static void Main(string[] args)
        {   // проверка ввода двух чисел
            int a, b;
            if (!int.TryParse(Console.ReadLine(), out a) |
            !int.TryParse(Console.ReadLine(), out b))
            {
                Console.WriteLine("Incorrect input");


            }
            else
            {
                // вывод
                Console.WriteLine(a - b);
            }


        }
    }
}

using System;

// программа выводи последнюю цифру числа
namespace Yandex_contest
{
    class Program
    {
        static void Main(string[] args)
        {   // проверка корректности ввода
            int N;
            if (!int.TryParse(Console.ReadLine(), out N) | N < 0)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                // вывод
                Console.WriteLine(N % 10);
            }


        }
    }
}

using System;

// программа выводи последнюю цифру числа
namespace Yandex_contest
{
    class Program
    {
        static void Main(string[] args)
        {   // проверка корректности ввода
            int N1, N2;
            if (!int.TryParse(Console.ReadLine(), out N1) |
                !int.TryParse(Console.ReadLine(), out N2))
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {   // вывод с помощью конструкции(if else)
                if (N1 > N2)
                {
                    Console.WriteLine("First");

                }
                else if (N2 > N1)
                {
                    Console.WriteLine("Second");

                }
                else
                {
                    Console.WriteLine("Equal");

                }


            }


        }
    }
}

using System;

// программа проверяет симметричность числа
namespace Yandex_contest
{
    class Program
    {   // метод возвращает строку, которую надо вывести в ответ
        public static string Method(int N)
        {
            int a1, a2, a3, a4;
            string str;
            a4 = N % 10;
            a3 = (N / 10) % 10;
            a2 = (N / 100) % 10;
            a1 = N / 1000;
            if (a1 == a4 & a2 == a3)
            {
                str = "True";
            }
            else
            {
                str = "False";
            }
            return str;

        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            int N;
            if (!int.TryParse(Console.ReadLine(), out N) | N < 1000 | N > 9999)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {   // вывод с помощью конструкции(if else)
                string str = Program.Method(N);
                Console.WriteLine(str);


            }


        }
    }
}

using System;

// программа считает гипотенузу
namespace Yandex_contest
{
    class Program
    {   // метод высчитывает гипотенузу
        public static void Hypotenuse(float a, float b, out double c)
        {
            c = Math.Sqrt(a * a + b * b);

        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            float a, b;
            if (!float.TryParse(Console.ReadLine(), out a) |
                !float.TryParse(Console.ReadLine(), out b) | a < 0 | b < 0)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                double c;
                Program.Hypotenuse(a, b, out c);
                Console.WriteLine($"{c:F5}");

            }


        }
    }
}

using System;

// программа выводит первую цифру числа после запятой
namespace Yandex_contest
{
    class Program
    {   // метод высчитывает первую цифру после запятой
        public static void First_Number(double N1)
        {
            int Number;
            Number = (int)(N1 * 10) % 10;
            Console.WriteLine(Number);
        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            double N1;
            if (!double.TryParse(Console.ReadLine(), out N1) || N1 < 0)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                Program.First_Number(N1);
            }


        }
    }
}

using System;

// программа вычисляет номер буквы в алфавите
namespace Yandex_contest
{
    class Program
    {   // метод вычисляет номер буквы в алфавите и выводит это на экран
        public static int Number_of_Symbol(char A)
        {
            int Number;
            Number = (int)A - (int)'a' + 1;
            return Number;
        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            char A;
            if (!char.TryParse(Console.ReadLine(), out A) || A < (int)'a' || A > (int)'z')
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                int Number = Program.Number_of_Symbol(A);
                Console.WriteLine(Number);

            }


        }
    }
}

using System;

namespace Yandex_contest
{
    class Program
    {   
         // метод округляет число до ближайшего нечётного в случае равноудаленности от двух соседних чисел,
         // иначе по правилам математики
         //
        public static int Round(double N)
        {
            int Number;
            // если число равноудалено от двух целых
            if (N * 10 % 10 == 5 | N * 10 % 10 == -5)
            {
                if ((int)N % 2 == 0 & N >= 0) //целая часть чётная и N положительное
                {
                    Number = (int)N + 1;
                }
                else if ((int)N % 2 == 0 & N < 0)
                {
                    Number = (int)N - 1;
                }
                else
                {
                    Number = (int)N;
                }
            }
            else
            {
                if (N >= 0)
                {
                    Number = (int)(N + 0.5);

                }
                else
                {
                    Number = (int)(N - 0.5);
                }
            }
            return Number;
        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            double N;
            if (!double.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                int Number = Program.Round(N);
                Console.WriteLine(Number);

            }


        }
    }
}

using System;

namespace Yandex_contest
{
    class Program
    {   //метод вычисляет принадлежность заданной области
        public static bool Method(double X, double Y)
        {
            bool flag;
            // проверка принадлежности системе уравнений
            if ((X * X + Y * Y <= 2) & (X * X + Y * Y >= 1))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        static void Main(string[] args)
        {   // проверка корректности ввода
            double X, Y;
            if (!double.TryParse(Console.ReadLine(), out X) |
                !double.TryParse(Console.ReadLine(), out Y))
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                bool flag = Program.Method(X, Y);
                Console.WriteLine(flag);

            }


        }
    }
}
*/