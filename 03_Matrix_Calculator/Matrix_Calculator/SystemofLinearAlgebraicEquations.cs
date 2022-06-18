using System;
using System.Threading.Tasks;
using Matrix_Calculator;

class LinearAlg
{
    /// <summary>
    /// Метод решения СЛАУ.
    /// </summary>
    public static void SystemofLinearAlgebraicEquations()
    {
        int rows;
        int cols;
        // Узнаем размерность матрицы.
        Console.WriteLine(" Подсказка: Матрица должна быть квадратной. ");
        Program.Size(out rows, out cols);

        double[][] matrix = new double[rows][];
        for (int i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new double[cols];
        }
        // Заполняем матрицу.
    Repeat:
        Console.WriteLine(" Построчно заполняйте матрицу, разделяя значения пробелами. Для завершения ввода строки, нажмите Enter");
        for (int i = 0; i < rows; i++)
        {
            Console.WriteLine($" Строка: {i + 1}, введите {cols} элементов(-а)");
            string[] str = Console.ReadLine().TrimEnd().Split(' ');
            if (str.Length != cols)
            {
                goto Repeat;
            }
            else
            {
                for (int j = 0; j < str.Length; j++)
                {
                    if (!double.TryParse(str[j], out matrix[i][j]))
                    {
                        Console.WriteLine(" Введены некорректные данные");
                        goto Repeat;
                    }

                }
            }

        }
    A: // Заполнение вектор-столбца.
        double[] b = new double[rows];
        Console.WriteLine(" Введите значение вектор-столбца");
        for (int i = 0; i < b.Length; i++)
        {
            Console.Write($" b[{i + 1}] = ");
            if (!double.TryParse(Console.ReadLine(), out b[i]))
            {
                Console.WriteLine(" Неверное значение!");
                goto A;
            }
        }

        if (matrix.Length != matrix[0].Length)
            Console.WriteLine("Матрица должны быть квадратной!");
        else
        {
            // Решение СЛАУ.
            double[] x = Solve(matrix, b);
            if (x == null)
                Console.WriteLine(" СЛАУ не имеет решений ");
            else if (double.NaN == x[0])
            {
                Console.WriteLine(" Система имеет бесконечно много решений. ");
            }
            else
            {
                string result = string.Empty;
                Array.ForEach(x, i => result += i + "\n");
                Console.WriteLine("\n Solution is x = \n" + result);
            }

        }
    }

    /// <summary>
    /// Разложение матрицы LUP Дулитл(тоже самое что и Крамер, только лучше).
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="perm"></param>
    /// <param name="toggle"></param>
    /// <returns></returns>
    static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
    {
        // Решние СЛАУ методом LUP Дулитла(тоже самое что и Крамер, только лучше).
        int n = matrix.Length;
        double[][] result = (double[][])matrix.Clone();
        perm = new int[n];
        for (int i = 0; i < n; ++i)
            perm[i] = i;

        toggle = 1;
        for (int j = 0; j < n - 1; ++j)
        {
            // Наибольшее значение в столбце j.
            double colMax = Math.Abs(result[j][j]);
            int pRow = j;
            Parallel.For(j + 1, n, i =>
            {
                if (result[i][j] > colMax)
                {
                    colMax = result[i][j];
                    pRow = i;
                }
            });
            // Перестановляем строки.
            if (pRow != j)
            {
                double[] rowPtr = result[pRow];
                result[pRow] = result[j];
                result[j] = rowPtr;
                // Меняем перестановку.
                int tmp = perm[pRow];
                perm[pRow] = perm[j];
                perm[j] = tmp;
                toggle = -toggle;
            }
            if (Math.Abs(result[j][j]) < 1.0E-20)
                return null;
            Parallel.For(j + 1, n, i =>
            {
                result[i][j] /= result[j][j];
                for (int k = j + 1; k < n; ++k)
                    result[i][k] -= result[i][j] * result[j][k];
            });
        }
        return result;
    }

    /// <summary>
    /// Решение СЛАУ.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    static double[] Solve(double[][] A, double[] b)
    {
        // Решаем Ax = b
        int n = A.Length;
        int[] perm;
        int toggle;
        double[][] luMatrix = MatrixDecompose(
          A, out perm, out toggle);
        if (luMatrix == null)
            return null;
        double[] bp = new double[b.Length];
        for (int i = 0; i < n; ++i)
            bp[i] = b[perm[i]];
        double[] x = Helper(luMatrix, bp);
        return x;
    }

    /// <summary>
    /// Метод вычисляет x, исходя из LUP матрицы.
    /// </summary>
    /// <param name="luMatrix"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    static double[] Helper(double[][] luMatrix, double[] b)
    {
        // Решаем luMatrix * x = b.
        int n = luMatrix.Length;
        double[] x = new double[n];
        b.CopyTo(x, 0);
        for (int i = 1; i < n; ++i)
        {
            double sum = x[i];
            for (int j = 0; j < i; ++j)
                sum -= luMatrix[i][j] * x[j];
            x[i] = sum;
        }
        x[n - 1] /= luMatrix[n - 1][n - 1];
        for (int i = n - 2; i >= 0; --i)
        {
            double sum = x[i];
            for (int j = i + 1; j < n; ++j)
                sum -= luMatrix[i][j] * x[j];
            x[i] = sum / luMatrix[i][i];
        }
        return x;
    }

}