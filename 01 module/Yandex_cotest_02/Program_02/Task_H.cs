using System;

partial class Program
{
    static void Main(string[] args)
    {
        string dayOfWeek = Console.ReadLine();
        int firstNumberInput = int.Parse(Console.ReadLine());
        int secondNumberInput = int.Parse(Console.ReadLine());
        int result = MorningWorkout(dayOfWeek, firstNumberInput, secondNumberInput);
        if (result == int.MinValue)
            Console.WriteLine("Incorrect input");
        else
            Console.WriteLine(result);
    }
}

partial class Program
{
    // В случае, если введённый день недели не соответствует формату входных данных
    // метод должен вернуть int.MinValue.
    // Гарантируется, что int.MinValue не может быть получен как верный ответ.
    /// <summary>
    /// Метод переключает алгоритмы в зависимости от дня недели.
    /// </summary>
    /// <param name="dayOfWeek">День недели</param>
    /// <param name="firstNumber">Первое число</param>
    /// <param name="secondNumber">Второе число</param>
    /// <returns></returns>
    static int MorningWorkout(String dayOfWeek, int firstNumber, int secondNumber)
    {
        int result;
        int modificator;
        switch (dayOfWeek)
        {
            case "Monday":
            case "Wednesday":
            case "Friday":
                modificator = 1;
                // 1 - означает вычислить сумму нечетных цифр первого числа.
                result = GetSumOfOddOrEvenDigits(firstNumber, modificator);
                break;
            case "Tuesday":
            case "Thursday":
                modificator = 2;
                // 2 - означает вычислить сумму четных чисел второго числа.
                result = GetSumOfOddOrEvenDigits(secondNumber, modificator);
                break;
            case "Saturday":
                result = Maximum(firstNumber, secondNumber);
                break;
            case "Sunday":
                result = Multiply(firstNumber, secondNumber);
                break;
            default:
                result = int.MinValue;
                break;
        }
        return result;
    }
    /// <summary>
    /// Метод вычисляет сумму цифр числа в зачисимости от параметра reminder.
    /// </summary>
    /// <param name="value">Число, которое нужно обработать</param>
    /// <param name="remainder">Модификатор режима алгоритма</param>
    /// <returns></returns>
    static int GetSumOfOddOrEvenDigits(int value, int remainder)
    {
        value = Math.Abs(value);
        // возвращаемое значение.
        int result = 0;
        // 1 - означает вычислить сумму нечетных цифр первого числа.
        if (remainder == 1)
        {
            int digit = 0;
            int sum = 0;
            while (value > 0)
            {
                digit = value % 10;
                if (digit % 2 != 0)
                {
                    sum += digit;
                }
                // отделение последней цифры.
                value /= 10;
            }
            result = sum;
        }
        // 2 - означает вычислить сумму четных чисел второго числа.
        if (remainder == 2)
        {
            int digit = 0;
            int sum = 0;
            while (value > 0)
            {
                digit = value % 10;
                if (digit % 2 == 0)
                {
                    sum += digit;
                }
                // отделение последней цифры.
                value = value / 10;
            }
            result = sum;
        }
        return result;

    }
    /// <summary>
    /// Метод вычисляет произведение firstValue и secondValue
    /// </summary>
    /// <param name="firstValue"> Первое число</param>
    /// <param name="secondValue">Второе число</param>
    /// <returns>Произведение</returns>
    static int Multiply(int firstValue, int secondValue)
    {
        return firstValue * secondValue;
    }
    /// <summary>
    /// Метод нахожит максимум их двуз чисел
    /// </summary>
    /// <param name="firstValue">Первое число</param>
    /// <param name="secondValue">Второе число</param>
    /// <returns>Максимум</returns>
    static int Maximum(int firstValue, int secondValue)
    {
        return firstValue >= secondValue ? firstValue : secondValue;
    }
}
