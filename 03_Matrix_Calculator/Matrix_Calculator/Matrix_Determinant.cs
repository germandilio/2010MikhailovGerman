using System;
using System.Threading.Tasks;


namespace Matrix_Determinant
{
    public class Matrix
    {
        private double[,] data;

        public int M { get; }
        public int N { get; }

        public Matrix(int m, int n)
        {
            this.M = m;
            this.N = n;
            this.data = new double[m, n];
            this.ReplaceData((i, j) => this.data[i, j] = 0);
        }

        private double precalculatedDeterminant = double.NaN;

        /// <summary>
        /// Подсчет детерминанта.
        /// </summary>
        /// <returns>Детерминант.</returns>
        public double Determinant()
        {
            if (!double.IsNaN(this.precalculatedDeterminant))
            {
                return this.precalculatedDeterminant;
            }
            
            if (this.N == 2)
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }
            double result = 0;
            Parallel.For(0, this.N, i =>
            {
                result += (i % 2 == 1 ? 1 : -1) * this[1, i] *
                    this.CalculateMinor(1, i);
            });
            this.precalculatedDeterminant = result;
            return result;
        }

        /// <summary>
        /// Вычисление минора.
        /// </summary>
        /// <param name="i">Строку,которую надо удалить.</param>
        /// <param name="j">Столбец, который нужно удалить.</param>
        /// <returns>Минор.</returns>
        private double CalculateMinor(int i, int j)
        {
            return CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(i).Determinant();
        }

        /// <summary>
        /// Создание матрицы без строки.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <returns>Матрицу, без строки.</returns>
        private Matrix CreateMatrixWithoutRow(int row)
        {
            // Создаем новый объект без 1 строки.
            var result = new Matrix(this.M - 1, this.N);
            // Пропускаем заполнение строки, которую удаляем.
            // Каждый i-ый элемент после row(удаляемая строка), будет в исходной матрице i + 1, а в конечной i.
            result.ReplaceData((i, j) => result[i, j] = i < row ? this[i, j] : this[i + 1, j]);
            return result;
        }

        /// <summary>
        /// Создание матрицы без столбца, номер столбца.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private Matrix CreateMatrixWithoutColumn(int column)
        {
            // Создаем новый объект без 1 столбца.
            var result = new Matrix(this.M, this.N - 1);
            // Пропускаем заполнение столбца, который удаляем.
            // Каждый j-ый элемент после column(удаляемого столбца), будет в исходной матрице j + 1, а в конечной j.
            result.ReplaceData((i, j) => result[i, j] = j < column ? this[i, j] : this[i, j + 1]);
            return result;
        }
        /// <summary>
        /// Метод копирует данные из одной матрицы в другую.
        /// </summary>
        /// <param name="func"></param>
        public void ReplaceData(Action<int, int> func)
        {
            for (int i = 0; i < this.M; i++)
            {
                for (int j = 0; j < this.N; j++)
                {
                    func(i, j);
                }
            }
        }

        public double this[int x, int y]
        {
            get
            {
                return this.data[x, y];
            }
            set
            {
                this.data[x, y] = value;
                this.precalculatedDeterminant = double.NaN;
            }
        }
    }
}
