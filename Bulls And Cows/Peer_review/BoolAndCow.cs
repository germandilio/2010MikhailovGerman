using System;

namespace Peer_review
{
    class BoolAndCow
    {
        /// <summary>
        /// Метод Main вызывает печать правил, и служит переключателем между.
        /// двумя режимами игры (для четырехзначного числа и для N-значного).
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Clear();
            // Печать правил.
            RullesOfGame();
            int mode;
            string str = Console.ReadLine();
            while (!int.TryParse(str, out mode) || (mode != 0 & mode != 1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка выбора режима!");
                Console.ResetColor();
                Console.WriteLine("Нажмите \"0\" для игры с 4-значным числом");
                Console.WriteLine("Нажмите \"1\" для игры с N-значным числом");
                str = Console.ReadLine();
            }

            // Switcher of game mode.
            switch (mode)
            {
                case 0:
                    GameFor_4();
                    break;
                case 1:
                    int N;
                    Console.WriteLine("Введите N от 1 до 9");
                    string input_N = Console.ReadLine();
                    while (!int.TryParse(input_N, out N) || N > 9 || N < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка выбора режима! Введите число N от 1 до 9");
                        Console.ResetColor();
                        input_N = Console.ReadLine();
                    }
                    GameFor_N(ref N);
                    break;
            }
        }

        /// <summary>
        /// Метод запускает игру для N = 4.
        /// </summary>
        static void GameFor_4()
        {
            do
            {
                int[] number = new int[4];
                // Mode of game.
                int mode = 4;
                Console.WriteLine("Введите четырехзначное число: ");

                // Создание рандомного числа.
                RandomNumbers(ref number, mode);

                int Number;
                int Bools = 0;
                int Cows = 0;
                while (Bools != 4)
                {
                    Bools = 0;
                    Cows = 0;
                    string input = Console.ReadLine();
                    while (!int.TryParse(input, out Number) || Number < 1000 || Number > 9999)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка ввода, введите четырёхзначное число!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                    }
                    // Строка с рандомным число и массив цифр введённого числа(для дальнейшего анализа).
                    string stringnumber = number[0].ToString() + number[1] + number[2] + number[3];
                    char[] digits = input.ToCharArray();

                    // Проверка совпадений.
                    for (int i = 0; i < 4; i++)
                    {
                        // Проверка, что входная строка содержит цифру из массива number.
                        if (stringnumber.Contains(digits[i]))
                        {   // Если совпадают и номера.
                            if (stringnumber[i] == digits[i])
                                Bools++;
                            else
                                Cows++;
                        }
                    }
                    // Печать подсказок пользователю.
                    HelpBox(Bools, Cows);


                }
                // Печать результата завершения и дальнейшиех инструкций.
                EndOfGame();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }

        /// <summary>
        /// Метод запускает игру для N, введенного с клавиатуры.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="N"></param>
        static void GameFor_N(ref int N)
        {
            do
            {
                int[] number = new int[N];
                Console.WriteLine($"Введите {N}-значное число: ");

                // Создание рандомного числа.
                RandomNumbers(ref number, N);

                int Number;
                int Bools = 0;
                int Cows = 0;
                int minNumber = 0;
                int maxNumber = 0;
                GetMinAndMaxValue(N, ref minNumber, ref maxNumber);
                while (Bools < N)
                {
                    Bools = 0;
                    Cows = 0;
                    // Ввод числа.
                    Number = 0;
                    string input = Console.ReadLine();
                    while (!int.TryParse(input, out Number) || Number < minNumber || Number > maxNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ошибка ввода, введите {N}-значное число!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                    }

                    // Строка с рандомным число и массив цифр введённого числа(для дальнейшего анализа).
                    string stringnumber = number[0].ToString();
                    if (N != 1)
                    {
                        for (int i = 1; i < N; i++)
                        {
                            stringnumber += number[i].ToString();
                        }
                    }

                    char[] digits = input.ToCharArray();

                    // Проверка совпадений.
                    for (int i = 0; i < N; i++)
                    {
                        // Проверка, что входная строка содержит цифру из массива number.
                        if (stringnumber.Contains(digits[i]))
                        {   // Если совпадают и номера.
                            if (stringnumber[i] == digits[i])
                                Bools++;
                            else
                                Cows++;
                        }
                    }
                    // Печать подсказок пользователю.
                    HelpBox(Bools, Cows);

                }
                // Печать резуллтата завершения и дальнейшиех инструкций.
                EndOfGame();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }
        /// <summary>
        /// Метод находит минимальное и максимальное допустимое значение для N- значного числа.
        /// </summary>
        /// <param name="N">N знаков</param>
        /// <param name="minNumber"></param>
        /// <param name="maxNumber"></param>
        static void GetMinAndMaxValue(int N, ref int minNumber, ref int maxNumber)
        {
            switch (N)
            {
                case 1:
                    minNumber = 1;
                    maxNumber = 9;
                    break;
                case 2:
                    minNumber = 11;
                    maxNumber = 99;
                    break;
                case 3:
                    minNumber = 111;
                    maxNumber = 999;
                    break;
                case 4:
                    minNumber = 1111;
                    maxNumber = 9999;
                    break;
                case 5:
                    minNumber = 11111;
                    maxNumber = 99999;
                    break;
                case 6:
                    minNumber = 111111;
                    maxNumber = 999999;
                    break;
                case 7:
                    minNumber = 1111111;
                    maxNumber = 9999999;
                    break;
                case 8:
                    minNumber = 11111111;
                    maxNumber = 99999999;
                    break;
                case 9:
                    minNumber = 111111111;
                    maxNumber = 999999999;
                    break;

            }
        }

        /// <summary>
        /// Метод печатает на экран правила игры.
        /// </summary>
        static void RullesOfGame()
        {
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|   Легенда игры:                                                            |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|Компьютер загадывает случайное четырёхзначное число(в случае с режимом \"1\"  |");
            Console.WriteLine("|N-значное).Покажите, что люди умнее компьютеров.                            |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|Специальные обозначения:                                                    |");
            Console.WriteLine("|\"Бык\" - когда угадана и цифра, и её местоположение.                         |");
            Console.WriteLine("|\"Корова\" - когда угадана цифра, но не её местоположение.                    |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|   Правила игры:                                                            |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|Игра продолжается до тех пор, пока не будет 4 \"Быка\".                       |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");
            Console.WriteLine("|Я вижу вы готовы? Осталось всего лишь выбрать режим игры:                   |");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("|!!!Режим игры можно выбрать только один раз(в начале игры)!!!               |");
            Console.ResetColor();
            Console.WriteLine("|Нажмите \"0\" для игры с 4-значным числом.                                    |");
            Console.WriteLine("|Нажмите \"1\" для игры с N-значным числом.                                    |");
            Console.WriteLine(" --------------------------------------------------------------------------- ");

        }

        /// <summary>
        /// Метод печатает результат игры и дальнейшие возможные действия.
        /// </summary>
        /// <param name="flag">Показывает была ли ошибка ввода</param>
        static void EndOfGame()
        {
            Console.WriteLine("Великолепно!!!");
            Console.WriteLine("Вы победили компютер. Это вселяет веру в человечество)");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Для выхода из игры нажмите ESC ...               ");
            Console.WriteLine("Для повторения игры нажмите любую другую клавишу ");
        }

        /// <summary>
        /// Метод печатает подсказки игроку.
        /// </summary>
        /// <param name="flag">Показывает была ли ошибка ввода</param>
        /// <param name="Bools">Количество Bools</param>
        /// <param name="Cows">Количеств Cows</param>
        static void HelpBox(int Bools, int Cows)
        {
            Console.WriteLine($"({Bools}) Быков, ({Cows})  Коров");
            if (Bools == 0)
            {
                Console.WriteLine("Неудача( , попробуйте ещё раз!");
            }
            else if (Bools != 4)
            {
                Console.WriteLine("Хорошо, попробуйте ещё раз!");
            }

        }

        /// <summary>
        /// Метод генерирует массив случайных чисел(N чисел).
        /// </summary>
        /// <param name="number">Массив,куда складываются значения</param>
        /// <param name="mode"> генерация для N-значного числа</param>>
        /// <returns>Массив цифр случайного числа</returns>
        static void RandomNumbers(ref int[] number, int mode)
        {
            Random Random = new Random();
            // Алгоритм для генерации неповторяющихся чисел(их количество = mode).
            int digit = 0;
            int stopper = 0;
            while (stopper < mode)
            {
                digit = Random.Next(9);
                bool flag = true;
                for (int i = 0; i < stopper; i++)
                    if (digit == number[i])
                    {
                        flag = false;
                        break;
                    }
                if (flag)
                {
                    number[stopper] = digit;
                    stopper++;
                }
            }
        }

    }
}
