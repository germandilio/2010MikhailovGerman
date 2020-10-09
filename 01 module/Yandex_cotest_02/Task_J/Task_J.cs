using System;

partial class Program
{
    /// <summary>
    /// Проверка на валидные входные данные.
    /// </summary>
    /// <param name="day">День недели</param>
    /// <param name="month">Месяц</param>
    /// <param name="year">Год</param>
    /// <returns>True if correct, false in other cases </returns>
    private static bool ValidateData(int day, int month, int year)
    {
        int[] DaysInMonthNormal = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] DaysInMonthLeap = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        // Проверяем високосный ли год.
        bool IsLeapYear = IsThisYearIsLeap(year);
        // Проверяем существует ли такой день в месяце.
        bool Notcorrectday = (IsLeapYear) ? (day > DaysInMonthLeap?[month - 1]) : (day > DaysInMonthNormal?[month - 1]);
        if (year < MinYear || year > MaxYear || day < 1 || day > 31 || month < 1 || month > 12 || Notcorrectday)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    /// <summary>
    /// Получение дня недели.
    /// </summary>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns>Возвращает число от 1 до 7, где 1 - понедельник, и т.д. </returns>
    private static int GetDayOfWeek(int day, int month, int year)
    {
        /* Метод с Хабра работает с ошибками.
                int[] Monthes = { 6, 2, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
                // (50 – Yв/2 + dY) % 7 , а затем y + m + d
                // алгоритм поиска дня недели
                int MinLeapYear = GetNearestLeftLeapYear(year);
                *тут должен быть метод,находящий ближайший високосный год*
                // смещение года.
                int BiasOfYear = Math.Abs(50 - (MinLeapYear / 2) + (year % 100) - MinLeapYear) % 7;
                bool IsLeapYear = IsThisYearIsLeap(year);
                int DayOfWeek;
                if (IsLeapYear & (month == 2 || month == 1))
                {
                    DayOfWeek = (BiasOfYear + Monthes[month - 1] + day - 1) % 7;
                }
                else
                {
                    DayOfWeek = (BiasOfYear + Monthes[month - 1] + day) % 7;

                }
        */

        DateTime dateTime = new DateTime(year, month, day);
        int date = (int)dateTime.DayOfWeek;
        if (date == 0)
        {
            date = 7;
        }
        return date;


    }
    /// <summary>
    /// Метод находит ближайшую пятницу.
    /// </summary>
    /// <param name="dateOfWeek"></param>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns>Answer</returns>
    private static string GetDateOfFriday(int dateOfWeek, int day, int month, int year)
    {
        // смещение
        int bias;
        // вычисление смещения по дням.
        if (dateOfWeek <= 5 & dateOfWeek >= 1)
        {
            bias = 5 - dateOfWeek;
        }
        else if (dateOfWeek == 6)
        {
            bias = 6;
        }
        else
        {
            bias = 5;
        }
        UpdatedDay(ref day, ref month, ref year, bias);
        return GetFormatMessage(day, month, year);

    }
    /// <summary>
    /// Метод возвращает обновленные значения дня, месяца и года.
    /// </summary>
    /// <param name="day">День,введенный пользователем</param>
    /// <param name="month">Месяц,,введенный пользователем</param>
    /// <param name="year">Год,введенный пользователем</param>
    /// <param name="bias">Смещение до пятницы</param>
    private static void UpdatedDay(ref int day, ref int month, ref int year, int bias)
    {
        bool IsLeapYear = IsThisYearIsLeap(year);
        // массивы дней в месяцах (високосный и нет).
        int[] DaysInMonthNormal = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] DaysInMonthLeap = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        if (IsLeapYear)
        {   // високосный.
            // проверка переполнения года. 
            if ((month < 12) || (month == 12 & day + bias <= DaysInMonthLeap?[month - 1]))
            {
                // проверка переполнения месяца.
                if (day + bias > DaysInMonthLeap?[month - 1])
                {
                    day = DaysInMonthLeap[month - 1] - (day + bias);
                    day = Math.Abs(day);
                    ++month;
                }
                else
                {
                    day += bias;
                }
            }
            else
            {
                day = DaysInMonthLeap[month - 1] - (day + bias);
                day = Math.Abs(day);
                month++;
                year++;
            }

        }
        else
        {
            if ((month < 12) || (month == 12 & day + bias < DaysInMonthNormal?[month - 1]))
            {
                // проверка переполнения месяца.
                if (day + bias > DaysInMonthNormal?[month - 1])
                {
                    day = DaysInMonthNormal[month - 1] - (day + bias);
                    day = Math.Abs(day);
                    month++;
                }
                else
                {
                    day = day + bias;
                }
            }
            else
            {
                day = DaysInMonthNormal[month - 1] - (day + bias);
                day = Math.Abs(day);
                month++;
                year++;
            }
        }
    }
    /// <summary>
    /// Метод узнает был ли год високосным.
    /// </summary>
    /// <param name="year">Год, который необходимо проверить</param>
    /// <returns>True if is leap year, false if not</returns>
    private static bool IsThisYearIsLeap(int year)
    {   // массив високосных годов.
        int[] LeapYears = {1704,1708,1712,1716,1720,1724,1728,1732,1736,1740,1744,1748,
                          1752,1756,1760,1764,1768,1772,1776,1780,1784,1788,1792,1796 };

        // узнаем был ли год високосным.
        bool IsLeapYear = false;
        for (int i = 0; i < LeapYears.Length; i++)
        {
            if (year == LeapYears[i])
            {
                IsLeapYear = true;
            }
        }

        return IsLeapYear;
    }
}

partial class Program
{
    private const int MinYear = 1701;
    private const int MaxYear = 1800;

    private static string GetFormatMessage(int day, int month, int year)
    {
        return String.Format("{0:D2}.{1:D2}.{2:D4}", day, month, year);
    }

    private static void Main(string[] args)
    {
        int day = int.Parse(Console.ReadLine());
        int month = int.Parse(Console.ReadLine());
        int year = int.Parse(Console.ReadLine());

        if (!ValidateData(day, month, year))
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        int dateOfWeek = GetDayOfWeek(day, month, year);

        string outputMessage = GetDateOfFriday(dateOfWeek, day, month, year);

        Console.WriteLine(outputMessage);
    }
}
