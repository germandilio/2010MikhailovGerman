using System;
using System.Threading.Tasks;
using Matrix_Determinant;
using System.IO;

namespace Matrix_Calculator
{
    class Program
    {
        // Шапка, которая будет подсказывать пользователю что сейчас надо сделать.
        private static string headToPrint;

        /// <summary>
        /// Метод выполянет транспонирование матрицы.
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>True, если матрица не квадратная, иначе False</returns>
        private static double[,] Transposition(double[,] matrix)
        {
            double tmp;
            double[,] result = (double[,])matrix.Clone();
            for (int i = 0; i < result.GetLength(1); i++)
                for (int k = 0; k < i; k++)
                {
                    tmp = result[i, k];
                    result[i, k] = result[k, i];
                    result[k, i] = tmp;
                }
            return result;
        }



        /// <summary>
        /// Метод вычисляет след матрицы.
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <returns>True, если матрица не квадратная, иначе False</returns>
        private static bool MatrixTrace(double[,] matrix, out double trace)
        {
            trace = 0;
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                    trace += matrix[i, i];

                return false;
            }
            else
                return true;

        }
        /// <summary>
        /// Метод вычисляет сумму матриц.
        /// </summary>
        /// <param name="matrix1">Первое слагаемое</param>
        /// <param name="matrix2">Второе слагаемое</param>
        /// <returns></returns>
        private static double[,] SumMatrix(double[,] matrix1, double[,] matrix2)
        {

            double[,] result = (double[,])matrix1.Clone();
            // Параллельное вычисление суммы строк матриц.
            Parallel.For(0, matrix1.GetLength(1), i =>
            {
                for (int k = 0; k < matrix1.GetLength(0); k++)
                {
                    result[k, i] = matrix1[k, i] + matrix2[k, i];
                }
            });
            return result;

        }

        /// <summary>
        /// Метод вычисляет разницу матриц.
        /// </summary>
        /// <param name="matrix1">Уменьшаемое</param>
        /// <param name="matrix2">Вычитаемое</param>
        /// <returns></returns>
        private static double[,] DifMatrix(double[,] matrix1, double[,] matrix2)
        {

            double[,] result = (double[,])matrix1.Clone();
            // Параллельное вычисление разности строк матриц.
            Parallel.For(0, matrix1.GetLength(1), i =>
            {
                for (int k = 0; k < matrix1.GetLength(0); k++)
                {
                    result[k, i] = matrix1[k, i] - matrix2[k, i];
                }
            });
            return result;

        }

        /// <summary>
        /// Метод умножает матрицу на константу.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="constant"></param>
        /// <returns></returns>
        private static double[,] MultiplyMatrixByConst(double[,] matrix, double constant)
        {

            double[,] result = (double[,])matrix.Clone();
            // Параллельное вычисление строки, умноженной на константу.
            Parallel.For(0, matrix.GetLength(1), i =>
            {
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    result[k, i] = matrix[k, i] * constant;
                }
            });
            return result;

        }

        /// <summary>
        /// Метод перемножает матрицы.
        /// </summary>
        /// <param name="matrix1">Первая матрица.</param>
        /// <param name="matrix2">Вторая матрица.</param>
        /// <returns></returns>
        private static double[,] MultiplyMatrix(double[,] matrix1, double[,] matrix2)
        {
            int matrix1Cols = matrix1.GetLength(0);
            int matrix1Rows = matrix1.GetLength(1);
            int matrix2Cols = matrix2.GetLength(0);
            double[,] result = new double[matrix2Cols, matrix1Rows];
            // Параллельный цикл по i.
            Parallel.For(0, matrix1Rows, i =>
            {
                for (int j = 0; j < matrix2Cols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matrix1Cols; k++)
                    {
                        temp = matrix1[k, i] * matrix2[j, k];
                    }
                    result[j, i] = temp;
                }
            });
            return result;
        }

        /// <summary>
        /// Метод печатает в консоль правила использования файлового менеджера.
        /// </summary>
        private static void Help()
        {
            // Вызов подсказок в самом начале, как пользоваться программой.
            Console.WriteLine();
            Console.WriteLine("     Добро пожаловать в Матричный калькулятор !!!                                                     ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ПОЖАЛУЙСТА!!! Для корректной работы программы, откройте терминал в отдельной вкладке на весь экран.  ");
            Console.WriteLine();
            Console.WriteLine(" Навигация устроена следующим образом:                                                                ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Выбор операции осуществляется с помощью стрелочек \"вверх\" и \"вниз\" на клавиатуре.                ");
            Console.WriteLine(" Для подтверждения выбора нажмите Enter.                                                              ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" Нажмите Enter, чтобы запустить программу ...                                                         ");
            Console.WriteLine(" Или ESC, чтобы выйти.                                                                                ");
        }

        /// <summary>
        /// Метод Main вызывает печать правил и запускает программу.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            do
            {
                // Печать подсказок пользователю.
                Help();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {   
                    try
                    {
                        // Запуск программы.
                        ChooseOperation();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла непредвиденная ошибка: " + ex.Message);
                    }
                }
                
               
                Console.WriteLine(" Для выхода нажмите Esc, для повторения Enter...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            
        }


        /// <summary>
        /// Метод подсвечивает выбор пользователеля возвращает номер выбранного пункта.
        /// </summary>
        /// <param name="Items">Пункты меню.</param>
        /// <returns>Выбор пользователя.</returns>
        private static int GetUserItem(string[] items)
        {

            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintFunctions(items, choice);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        return choice;
                }
                // Зацикливаем выбор.
                choice = (choice + items.Length) % items.Length;
            }
        }

        /// <summary>
        /// Метод печатает передоваемое меню, подсвечивая красным цветом текущий выбор пользователя.
        /// </summary>
        /// <param name="Directories">Массив отображаемых названий.</param>
        /// <param name="choice">Выобор пользователя.</param>
        private static void PrintFunctions(string[] items, int choice)
        {
            Console.Clear();
            // Печать шапки, которая подсказывает что делать.
            Console.WriteLine(headToPrint);
            Console.WriteLine();

            for (int i = 0; i < items.Length; i++)
            {
                if (i == choice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(items[i]);
                    Console.ResetColor();
                }
                else
                    Console.WriteLine(items[i]);

            }
        }

        private static int MethodOfReading()
        {

            headToPrint = " ВХОДНЫЕ ДАННЫЕ: (для подтверждения выбора нажмите Enter)";

            string[] items = { " Random значения ", " Ввод с клавиатуры ", " Считывание с файла" };
            return GetUserItem(items);
        }

        /// <summary>
        /// Метод является переключателем действий, в зависимости от выбора пользователя.
        /// </summary>
        /// <returns>Выбранную кодировку&</returns>
        private static void ChooseOperation()
        {
            headToPrint = " ВЫБЕРИТЕ ОПЕРАЦИЮ: (для подтверждения выбора нажмите Enter)";

            string[] functions = { " Нахождение следа матрицы ", " Транспонирование матрицы ", " Сумма двух матриц ", " Разность двух матриц ", " Произведение двух матриц ", " Умножение матрицы на константу ", " Нахождение определителя матрицы ", " Решить СЛАУ " };
            int item = GetUserItem(functions);
            Console.Clear();
            Console.WriteLine(" ВВЕДИТЕ ПАРАМЕТРЫ МАТРИЦЫ:");
            // Свитчер выбора операций.
            int cols = 0;
            int rows = 0;
            double[,] matrix;
            double[,] matrixA;
            double[,] matrixB;

            switch (item)
            {
                case 0:
                    //  Нахождение следа матрицы.
                    FillMatrix(cols, rows, out matrix, item);
                    double trace;

                    if (MatrixTrace(matrix, out trace))
                        Console.WriteLine(" Матрица не является квадратной!");
                    else
                        Console.WriteLine($" След матрицы = {trace}");
                    break;
                case 1:
                    // Транспонирование матрицы.
                    FillMatrix(cols, rows, out matrix, item);

                    if (matrix.GetLength(0) != matrix.GetLength(1))
                        Console.WriteLine(" Матрица не является квадратной!");
                    else
                    {
                        Console.WriteLine(" Транспонированная матрица: ");
                        PrintMatrix(Transposition(matrix));
                    }
                    break;
                case 2:
                    // Сумма двух матриц.
                    FillMatrix(cols, rows, out matrixA, item);
                    FillMatrix(cols, rows, out matrixB, item);

                    if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
                        Console.WriteLine(" Матрицы имеют разный размер!");
                    else
                    {
                        double[,] matrixToPrint = SumMatrix(matrixA, matrixB);
                        Console.WriteLine(" Сумма матриц: ");
                        PrintMatrix(matrixToPrint);
                    }
                    break;
                case 3:
                    // Разность двух матриц.
                    FillMatrix(cols, rows, out matrixA, item);
                    FillMatrix(cols, rows, out matrixB, item);

                    if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
                        Console.WriteLine(" Матрицы имеют разный размер!");
                    else
                    {
                        double[,] matrixToPrint = DifMatrix(matrixA, matrixB);
                        Console.WriteLine(" Сумма матриц: ");
                        PrintMatrix(matrixToPrint);
                    }
                    break;
                case 4:
                    // Произведение двух матриц.
                    FillMatrix(cols, rows, out matrixA, 41);
                    FillMatrix(cols, rows, out matrixB, 42);

                    if (matrixA.GetLength(0) != matrixB.GetLength(1))
                        Console.WriteLine(" Кол-во столбцов в первой матрице не равно кол-ву строк во второй!");
                    else
                    {
                        double[,] matrixToPrint = MultiplyMatrix(matrixA, matrixB);
                        Console.WriteLine(" Умножение матриц: ");
                        PrintMatrix(matrixToPrint);
                    }
                    break;
                case 5:
                    // Умножение матрицы на константу.
                    FillMatrix(cols, rows, out matrix, item);

                    double value;
                    do
                    {
                        Console.WriteLine($" Введите значение константы в диапазоне [{int.MinValue} , {int.MaxValue}] : ");
                    } while (!double.TryParse(Console.ReadLine(), out value));

                    Console.WriteLine(" Итоговая матрица: ");
                    MultiplyMatrixByConst(matrix, value);
                    break;
                case 6:
                    // Нахождение определителя матрицы.
                    int mode = MethodOfReading();
                    Size(out rows, out cols);
                    matrix = new double[rows, cols];
                    if (mode == 0)
                        matrix = MatrixRandom(rows, cols);

                    else if (mode == 1)
                        matrix = MatrixConsole(rows, cols);

                    else
                        matrix = MatrixFile(rows, cols);

                    if (cols != rows)
                        Console.WriteLine(" Матрица не является квадратной!");
                    else
                    {
                        // Создание объекта matrix, класс прописан в файле Matrix_Determinant.cs. 
                        Matrix matrixDet = new Matrix(rows, cols);
                        Console.WriteLine($" Детерминант матрицы: {matrixDet.Determinant()} ");
                    }
                    break;
                case 7:
                    LinearAlg.SystemofLinearAlgebraicEquations();
                    break;
            }
        }

        /// <summary>
        /// Заполнение матрицы рандомными значениями.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static double[,] MatrixRandom(int rows, int cols)
        {

            int minValue;
            int maxValue;
            Console.Clear();
            do
            {
                Console.WriteLine($" Введите максимальное значение случайного числа, не больше чем: ({int.MaxValue}): ");
            } while (!int.TryParse(Console.ReadLine(), out maxValue));

            do
            {
                Console.WriteLine($" Введите минимальное значение случайного числа, не меньше чем: ({int.MinValue}): ");
            } while (!int.TryParse(Console.ReadLine(), out minValue));

            // Генерируем матрицу со значениями в диапазоне от minVal до maxVal.
            Random random = new Random();
            double[,] result = new double[cols, rows];
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    result[i, j] = (maxValue - minValue) * random.NextDouble() + minValue;
            // Запрашиваем вывод рандомной матрицы.
            Console.WriteLine(" Хотите вывести в консоль random матрицу? (yes / no)");
            string user = Console.ReadLine();
            if ( user == "yes" || user == "Yes")
                PrintMatrix(result);
            return result;
        }

        /// <summary>
        /// Считываие введенных значений размерности матрицы.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public static void Size(out int rows, out int cols)
        {
            do
            {
                Console.WriteLine($" Введите количество строк в матрице: ");
            } while (!int.TryParse(Console.ReadLine(), out rows) || rows < 1);

            do
            {
                Console.WriteLine($" Введите количество столбцов в матрице: ");
            } while (!int.TryParse(Console.ReadLine(), out cols) || cols < 1);
        }

        /// <summary>
        /// Метод вывода матрицы на экран.
        /// </summary>
        /// <param name="matrix"></param>
        private static void PrintMatrix(double[,] matrix)
        {
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++, Console.WriteLine())
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    Console.Write($"{matrix[i, k]:f3} \t");
                }
            }
        }

        /// <summary>
        /// Метод выбора способа заполнения матрицы.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <param name="matrix"></param>
        private static void FillMatrix(int cols, int rows, out double[,] matrix, int switchcase)
        {
            int mode = MethodOfReading();

            Console.Clear();
            if (switchcase < 2 || switchcase == 6)
                Console.WriteLine(" Подсказка: Матрица должна быть квадратной. ");
            else if (switchcase >= 2 & switchcase <= 3)
                Console.WriteLine(" Подсказка: Матрицы должны иметь одинаковый размер. ");
            else if (switchcase == 41)
            {
                Console.WriteLine(" Подсказка: Количество столбцов первой матрицы должно быть равно кол-ву строк во второй. ");
                Console.WriteLine(" Введите данные первой матрицы:");
            }
            else
            {
                Console.WriteLine(" Подсказка: Количество столбцов первой матрицы должно быть равно кол-ву строк во второй. ");
                Console.WriteLine(" Введите данные второй матрицы:");
            }

            Size(out rows, out cols);
            matrix = new double[cols, rows];
            if (mode == 0)
                matrix = MatrixRandom(rows, cols);

            else if (mode == 1)
                matrix = MatrixConsole(rows, cols);

            else
                matrix = MatrixFile(rows, cols);
        }

        /// <summary>
        /// Метод считывания матрицы с файла.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static double[,] MatrixFile(int rows, int cols)
        {
        Repeat:
            double[,] numbers = new double[cols, rows];
            Console.WriteLine(" Введите абсолютный путь к считываемому файлу:");
            string path = Console.ReadLine();
            try
            {
                string[] matrix = File.ReadAllLines(path);
                if (matrix.Length != cols)
                {
                    Console.WriteLine(" Несоответствие указанного размера и размера матрицы в файле! ");
                    Size(out rows, out cols);
                    goto Repeat;
                }
                else
                {
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        string[] intput = matrix[i].Split(' ');
                        for (int j = 0; j < intput.Length; j++)
                        {
                            if (!double.TryParse(intput[j], out numbers[i, j]))
                            {
                                Console.WriteLine("\n Данные матрицы имеют недопустимый формат! ");
                                goto Repeat;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ошибка: " + ex.Message);
                goto Repeat;
            }
            return numbers;

        }

        /// <summary>
        /// Метод ввода матрицы с консоли.
        /// </summary>
        /// <param name="rows">Кол-во строк.</param>
        /// <param name="cols">Кол-во столбцов.</param>
        /// <returns></returns>
        private static double[,] MatrixConsole(int rows, int cols)
        {
        Repeat:
            Console.WriteLine(" Построчно заполняйте матрицу, разделяя значения пробелами. Для завершения ввода строки, нажмите Enter");
            double[,] numbers = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($" Строка: {i + 1}, введите {cols} элементов");
                string[] str = Console.ReadLine().TrimEnd().Split(' ');
                if (str.Length != cols)
                {
                    goto Repeat;
                }
                else
                {
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (!double.TryParse(str[j], out numbers[i, j]))
                        {
                            Console.WriteLine(" Введены некорректные данные");
                            goto Repeat;
                        }

                    }
                }

            }
            return numbers;
        }        
    }
}

