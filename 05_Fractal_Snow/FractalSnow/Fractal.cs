using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FractalSnow
{
    /// <summary>
    /// Абстрактный класс фракталов.
    /// </summary>
    abstract class Fractal
    {
        static protected PictureBox picture;
        static protected TextBox text;
        static protected TextBox text1;
        static protected TextBox text2;
        static internal Graphics graphics;

        static internal Pen pen1;
        static internal Pen pen2;
        static internal Color startColor;
        static internal Color endColor;
        static protected SolidBrush brush;

        /// <summary>
        /// Количество итераций, введенных пользователем.
        /// </summary>
        static internal int iter;
        /// <summary>
        /// Макисмальное количество итераций.
        /// </summary>
        static internal int max;

        // Дополнительные параметры.
        static protected double coefficient;
        static protected int angleFirst;
        static protected int angleSecond;
        static protected int DistanceInKantor;
        static internal int iteration = 0;


        virtual protected bool ValidateData(string str) { return false; }
        virtual protected bool ValidateData(string str, string str1, string str2) { return false; }
        virtual protected bool ValidateData(string str, string str1) { return false; }
        abstract public void PrintFractal();
        virtual protected int AlgorithmOfPrint(PointF point1, PointF point2, PointF point3, PointF point4,
            int n) { return 0; }
        virtual protected int AlgorithmOfPrint(PointF point1, PointF point2, PointF point3, int n) { return 0; }
        virtual protected int AlgorithmOfPrint(PointF point1, PointF point2, int step, int n) { return 0; }
        virtual protected int AlgorithmOfPrint(PointF point1, PointF point2, int n) { return 0; }
        virtual protected void AlgorithmOfPrint(int x, int y, int length, int angle, int angleLeft, int angleRight,
            double coefficient) { }

        /// <summary>
        /// Метод проверяющий заданныйе цвета.
        /// </summary>
        /// <returns></returns>
        protected static bool ValidateColor()
        {
            if (startColor == Color.Empty || endColor == Color.Empty)
            {
                MessageBox.Show("Не выбран начальный и/или конечный цвет!", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            if (startColor == Color.White && endColor == Color.White)
            {
                MessageBox.Show("Начальный и конечный цвет - белый! На белом фоне не будет видно фрактала.",
                    "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            ColorGradient.GenarateColors(max);
            return true;
        }

    }

    /// <summary>
    /// Класс отрисовки "Дерева Паскаля".
    /// </summary>
    class Pifagor : Fractal
    {
        public Pifagor(PictureBox box, TextBox text, TextBox text1, TextBox text2)
        {
            picture = box;
            // Угол наклона первого отрезка.
            Fractal.text = text;
            // угол наклона второго отрезка.
            Fractal.text1 = text1;
            // Коэффициент.
            Fractal.text2 = text2;
        }
        /// <summary>
        /// Метод запускает отрисовку Дерева Паскаля.
        /// </summary>
        public override void PrintFractal()
        {
            graphics.Clear(Color.White);
            if (ValidateData(text.Text, text1.Text, text2.Text) && ValidateColor())
                AlgorithmOfPrint(picture.Width / 2, (int)(0.05 * picture.Height), (int)(picture.Height / 3.45),
                    0, angleFirst, angleSecond, coefficient);
        }

        /// <summary>
        /// Алгоритм отрисовки "Дерева Паскаля".
        /// </summary>
        /// <param name="length">Длина линии.</param>
        /// <param name="angle">Угол.</param>
        /// <param name="angleLeft">Угол наклона влево.</param>
        /// <param name="angleRight">Угол наклона впарво.</param>
        /// <param name="coefficient">Коэфициент соотношения длины отрезка на разных итерациях.</param>
        protected override void AlgorithmOfPrint(int x, int y, int length, int angle, int angleLeft, int angleRight,
            double coefficient)
        {
            double x1, y1;
            x1 = x + length * Math.Sin(angle * 2 * Math.PI / 360.0);
            y1 = y + length * Math.Cos(angle * 2 * Math.PI / 360.0);
            pen1 = new Pen(ColorGradient.GetColor(iter++));
            graphics.DrawLine(pen1, x, picture.Height - y, (int)x1, picture.Height - (int)y1);
            if (length > 2)
            {
                AlgorithmOfPrint((int)x1, (int)y1, (int)(length / coefficient), angle + angleLeft, angleLeft,
                    angleRight, coefficient);
                AlgorithmOfPrint((int)x1, (int)y1, (int)(length / coefficient), angle - angleRight, angleLeft,
                    angleRight, coefficient);
            }
        }

        /// <summary>
        /// Метод проверяет корректность заданных данных.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        protected override bool ValidateData(string str, string str1, string str2)
        {
            int angle;
            int angle1;
            double coeff;
            if (!int.TryParse(str, out angle) || angle > 45 || angle < 10)
            {
                MessageBox.Show("Введено некорректное значение угла наклона первого отрезка! (Допустимый угол: от" +
                    " 10 до 45)", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            if (!int.TryParse(str1, out angle1) || angle1 > 45 || angle1 < 10)
            {
                MessageBox.Show("Введено некорректное значение угла наклона второго отрезка! (Допустимый угол: от" +
                    " 10 до 45)", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            if (!double.TryParse(str2, out coeff) || coeff > 5 || coeff < 1.4)
            {
                MessageBox.Show("Введено некорректное значение коэффициента соотношения! (Допустимые значения: от" +
                    " 1,4 до 5)", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            angleFirst = angle;
            angleSecond = angle1;
            coefficient = coeff;
            if (coefficient > 3)
                max = 20;
            else if (coefficient > 2)
                max = 50;
            else if (coefficient > 1.7)
                max = 100;
            else if (coefficient > 1.5)
                max = 200;
            else
                max = 6000;
            iter = 0;
            return true;
        }
    }

    /// <summary>
    /// Класс отрисовки "Кривой Коха".
    /// </summary>
    class Koch : Fractal
    {   
        static private int counter;
        public Koch(PictureBox box, TextBox text)
        {
            picture = box;
            Fractal.text = text;
        }
        /// <summary>
        /// Метод рсует фрактал "Кривая Коха".
        /// </summary>
        public override void PrintFractal()
        {
            graphics.Clear(Color.White);
            float w = picture.Width;
            float h = picture.Height;
            if (ValidateData(text.Text) && ValidateColor())
                AlgorithmOfPrint(new PointF(w / 8, h / 2 - h / 12), new PointF(7 * w / 8, h / 2 - h / 12), iter);
        }

        /// <summary>
        /// Метод проверяет корректность заданных данных.
        /// </summary>
        /// <param name="str">Входная строка из TextBox</param>
        /// <returns>Глубину рекурсии?</returns>
        protected override bool ValidateData(string str)
        {
            int iterNumber;
            if (!int.TryParse(str, out iterNumber) || iterNumber < 1 || iterNumber > 10)
            {
                MessageBox.Show("Введено некорректное значение глубины рекурсии! (Допустимая глубина: от 1 до 10)",
                    "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            max = (int)Math.Pow(4, iterNumber - 1);
            iter = iterNumber;
            iteration = iterNumber;
            return true;
        }

        /// <summary>
        /// Алгоритм отрисовки фрактала.
        /// </summary>
        /// <param name="n">Номер итерации.</param>
        /// <returns></returns>
        protected override int AlgorithmOfPrint(PointF point1, PointF point2, int n)
        {   
            if (n == 1)
            {
                var pen = new Pen(ColorGradient.GetColor(counter));
                counter++;
                graphics.DrawLine(pen, point1.X, picture.Height - point1.Y, point2.X, picture.Height - point2.Y);
                
            }
            else if (n > 1)
            {
                var length1 = point2.X - point1.X;
                var length2 = point2.Y - point1.Y;
                var x1 = point1.X + length1 / 3;
                var y1 = point1.Y + length2 / 3;
                var x2 = (float)(0.5 * (point1.X + point2.X) + Math.Sqrt(3) * (point1.Y - point2.Y) / 6);
                var y2 = (float)(0.5 * (point1.Y + point2.Y) + Math.Sqrt(3) * (point2.X - point1.X) / 6);
                var x3 = point1.X + 2 * length1 / 3;
                var y3 = point1.Y + 2 * length2 / 3;

                AlgorithmOfPrint(point1, new PointF(x1, y1), n - 1);
                AlgorithmOfPrint(new PointF(x1, y1), new PointF(x2, y2), n - 1);
                AlgorithmOfPrint(new PointF(x2, y2), new PointF(x3, y3), n - 1);
                AlgorithmOfPrint(new PointF(x3, y3), point2, n - 1);


            }
            return n;
        }
    }

    /// <summary>
    /// Класс построения ковра Серпинского.
    /// </summary>
    class SerpinskyCarpet : Fractal
    {
        static private int minPictureLength;

        public SerpinskyCarpet(PictureBox box, TextBox text)
        {
            picture = box;
            minPictureLength = Math.Min(picture.Height, picture.Width) - 5;
            Fractal.text = text;
        }
        /// <summary>
        /// Метод запускает алгоритм построения фрактала.
        /// </summary>
        public override void PrintFractal()
        {
            graphics.Clear(Color.White);
            if (ValidateData(text.Text) && ValidateColor())
            {
                var point1 = new PointF(0, minPictureLength);
                var point2 = new PointF(0, 0);
                var point3 = new PointF(minPictureLength, 0);
                var point4 = new PointF(minPictureLength, minPictureLength);

                AlgorithmOfPrint(point1, point2, point3, point4, iter);
            }
            
        }

        /// <summary>
        /// Рекурсивный алгоритм построения фрактала.
        /// </summary>
        /// <param name="n">Номер итерации.</param>
        /// <returns></returns>
        protected override int AlgorithmOfPrint(PointF point1, PointF point2, PointF point3, PointF point4, int n)
        {
            if (n == max)
            {
                brush = new SolidBrush(ColorGradient.GetColor(0));
                graphics.FillPolygon(brush, new PointF[] { point1, point2, point3, point4 });

                AlgorithmOfPrint(point1, point2, point3, point4, n - 1);
            }
            else if (n > 0)
            {
                PrintCarpet(point1, point2, point3, n);
            }
            return n;
        }

        /// <summary>
        /// Метод вычисляет точки и строит по ним квадраты.
        /// </summary>
        /// <param name="n">Номер итерации.</param>
        private void PrintCarpet(PointF p1, PointF p2, PointF p3, int n)
        {
            brush = new SolidBrush(ColorGradient.GetColor(max - n));
            // Вторая итерация.
            var point2 = new PointF((p3.X - p2.X) / 3 + p2.X, (p1.Y - p2.Y) / 3 + p2.Y);
            var point4 = new PointF(point2.X + (p3.X - p2.X) / 3, point2.Y + (p1.Y - p2.Y) / 3);
            var point1 = new PointF(point2.X, point2.Y + (p1.Y - p2.Y) / 3);
            var point3 = new PointF(point4.X, point2.Y);
            graphics.FillPolygon(brush, new PointF[] { point1, point2, point3, point4 });

            // 1-2-3 квадрат.
            point1 = new PointF(p2.X, (p1.Y - p2.Y) / 3 + p2.Y);
            point3 = new PointF(p2.X + (p3.X - p2.X) / 3, p2.Y);
            point2 = new PointF(p2.X, p2.Y);
            point4 = new PointF(p2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + p2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            point1 = point4;
            point2 = point3;
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, p2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + p2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            point1 = point4;
            point2 = point3;
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, p2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + p2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            PrintSecondAndThirdLine(p1, p2, p3, n, out point2, out point4, out point1, out point3);
        }


        private void PrintSecondAndThirdLine(PointF p1, PointF p2, PointF p3, int n, out PointF point2,
            out PointF point4, out PointF point1, out PointF point3)
        {
            // 4-5-6 квадрат.
            point1 = new PointF(p2.X, (p1.Y - p2.Y) * 2 / 3 + p2.Y);
            point2 = new PointF(p2.X, (p1.Y - p2.Y) / 3 + p2.Y);
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, point2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + point2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            // пропускаем центральный.
            point1.X += (p3.X - p2.X) * 2 / 3;
            point2.X += (p3.X - p2.X) * 2 / 3;
            point3.X += (p3.X - p2.X) * 2 / 3;
            point4.X += (p3.X - p2.X) * 2 / 3;
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            point1 = new PointF(p1.X, p1.Y);
            point2 = new PointF(point1.X, point1.Y - ((p1.Y - p2.Y) / 3));
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, point2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + point2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            point1 = point4;
            point2 = point3;
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, point2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + point2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);

            point1 = point4;
            point2 = point3;
            point3 = new PointF(point2.X + (p3.X - p2.X) / 3, point2.Y);
            point4 = new PointF(point2.X + (p3.X - p2.X) / 3, (p1.Y - p2.Y) / 3 + point2.Y);
            AlgorithmOfPrint(point1, point2, point3, point4, n - 1);
        }

        /// <summary>
        /// Метод проверяет корректность заданных данных.
        /// </summary>
        /// <param name="str">Входная строка из TextBox</param>
        /// <returns>Глубину рекурсии?</returns>
        protected override bool ValidateData(string str)
        {
            int iterNumber;
            if (!int.TryParse(str, out iterNumber) || iterNumber < 1 || iterNumber > 8)
            {
                MessageBox.Show("Введено некорректное значение глубины рекурсии! (Допустимая глубина: от 1 до 8)",
                    "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            iter = max = iterNumber;
            return true;
        }
    }

    /// <summary>
    /// Класс построения фкрактала "Треугольник Серпинского".
    /// </summary>
    class SerpinskyTriangle : Fractal
    {
        public SerpinskyTriangle(PictureBox box, TextBox text)
        {
            picture = box;
            Fractal.text = text;
        }
        /// <summary>
        /// Метод запускаоет алгорим постройки фрактала.
        /// </summary>
        public override void PrintFractal()
        {
            graphics.Clear(Color.White);
            if (ValidateData(text.Text) && ValidateColor())
            {
                var point1 = new PointF(picture.Width / 2, (float)(picture.Height * 0.02));
                var point2 = new PointF((float)(picture.Width * 0.02), (float)(0.98 * picture.Height));
                var point3 = new PointF((float)(0.98 * picture.Width), (float)(0.98 * picture.Height));

                AlgorithmOfPrint(point1, point2, point3, iter);
            }
            
        }

        /// <summary>
        /// Алгоритм построения Треугольника Серпинского.
        /// </summary>
        /// <param name="n"> Номер итерации.</param>
        /// <returns></returns>
        protected override int AlgorithmOfPrint(PointF point1, PointF point2, PointF point3, int n)
        {
            if (n == max)
            {
                graphics.DrawPolygon(new Pen(ColorGradient.GetColor(0)), new PointF[] { point1, point2, point3 });

                AlgorithmOfPrint(point1, point2, point3, n - 1);

            }
            else if (n > 0)
            {
                var pointLeft = new PointF((point2.X + point1.X) / 2, (point1.Y + point2.Y) / 2);
                var pointRight = new PointF((point3.X + point1.X) / 2, (point3.Y + point1.Y) / 2);
                var pointBotom = new PointF((point3.X + point2.X) / 2, point2.Y);

                graphics.DrawPolygon(new Pen(ColorGradient.GetColor(max - n)), new PointF[] { pointLeft, pointRight,
                    pointBotom });

                AlgorithmOfPrint(pointLeft, point2, pointBotom, n - 1);
                AlgorithmOfPrint(point1, pointLeft, pointRight, n - 1);
                AlgorithmOfPrint(pointRight, pointBotom, point3, n - 1);

            }
            return n;
        }

        /// <summary>
        /// Метод проверяет корректность заданных данных.
        /// </summary>
        /// <param name="str">Входная строка из TextBox</param>
        /// <returns>Глубину рекурсии?</returns>
        protected override bool ValidateData(string str)
        {
            int iterNumber;
            if (!int.TryParse(str, out iterNumber) || iterNumber < 1 || iterNumber > 10)
            {
                MessageBox.Show("Введено некорректное значение глубины рекурсии! (Допустимая глубина: от 1 до 10)", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            iter = max = iterNumber;
            return true;
        }
    }

    /// <summary>
    /// Класс построения фрактала "Множество Кантора".
    /// </summary>
    class Kantor : Fractal
    {
        public Kantor(PictureBox box, TextBox text, TextBox text1)
        {
            picture = box;
            Fractal.text = text;
            Fractal.text1 = text1;
        }
        public override void PrintFractal()
        {
            graphics.Clear(Color.White);
            if (ValidateData(text.Text, text1.Text) && ValidateColor())
            {
                var point1 = new PointF((float)(0.05 * picture.Width), (float)(0.05 * picture.Height));
                var point2 = new PointF((float)(0.95 * picture.Width), (float)(0.05 * picture.Height));
                AlgorithmOfPrint(point1, point2, DistanceInKantor, iter);
            }
            
        }

        /// <summary>
        /// Метод проверяет корректность заданных данных.
        /// </summary>
        /// <param name="str">Входная строка из TextBox</param>
        /// <returns>Глубину рекурсии.</returns>
        protected override bool ValidateData(string str, string str1)
        {
            int iterNumber;
            int length;
            if (!int.TryParse(str, out iterNumber) || iterNumber < 1 || iterNumber > 10)
            {
                MessageBox.Show("Введено некорректное значение глубины рекурсии! (Допустимая глубина: от 1 до 10)",
                    "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            if (!int.TryParse(str1, out length) || length < 20 || length > 100)
            {
                MessageBox.Show("Введено некорректное значение расстояния между отрезками! (Допустимая длина: от" +
                    " 20 до 100)", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
            iter = max = iterNumber;
            DistanceInKantor = length;
            return true;
        }

        /// <summary>
        /// Рекурсивный алгоритм пстроения Множества Кантора.
        /// </summary>
        /// <param name="step">Расстояние между линиями.</param>
        /// <param name="n">Номер итерации.</param>
        /// <returns></returns>
        protected override int AlgorithmOfPrint(PointF point1, PointF point2, int step, int n)
        {
            if (n == max)
            {   

                graphics.DrawLine(new Pen(ColorGradient.GetColor(0), 10), point1, point2);

                AlgorithmOfPrint(point1, point2, step, n - 1);
            }
            else if (n > 0)
            {
                var pointLeft = new PointF(point1.X, point1.Y + step);
                var pointMiddleLeft = new PointF((point2.X + 2 * point1.X) / 3, point1.Y + step);
                var pointMiddleRight = new PointF((2 * point2.X + point1.X) / 3, point1.Y + step);
                var pointRight = new PointF(point2.X, point1.Y + step);

                graphics.DrawLine(new Pen(ColorGradient.GetColor(max - n), 10), pointLeft, pointMiddleLeft);
                graphics.DrawLine(new Pen(ColorGradient.GetColor(max - n), 10), pointMiddleRight, pointRight);

                AlgorithmOfPrint(pointLeft, pointMiddleLeft, step, n - 1);

                AlgorithmOfPrint(pointMiddleRight, pointRight, step, n - 1);
            }
            return n;
        }
    }

    /// <summary>
    /// Класс с созданием градиента.
    /// </summary>
    class ColorGradient
    {
        private static List<Color> colorList;

        /// <summary>
        /// Метод получения градиента.
        /// </summary>
        /// <param name="iteration"></param>
        /// <returns></returns>
        public static Color GetColor(int iteration)
        {
            if (iteration >= Fractal.max + Fractal.iteration * 2)
                return Fractal.endColor;
            else
                return colorList[iteration];
        } 

        /// <summary>
        /// Метод создает палитру градиентов.
        /// </summary>
        /// <param name="max"></param>
        public static void GenarateColors(int max)
        {
            int rMax = Fractal.endColor.R;
            int rMin = Fractal.startColor.R;
            int gMin = Fractal.startColor.G;
            int gMax = Fractal.endColor.G;
            int bMin = Fractal.startColor.B;
            int bMax = Fractal.endColor.B;

            colorList = new List<Color>();
            for (int i = 0; i < max + Fractal.iteration * 2; i++)
            {
                var rAverage = Math.Abs(rMin + (int)((rMax - rMin) * i / max)) % 256;
                var gAverage = Math.Abs(gMin + (int)((gMax - gMin) * i / max)) % 256;
                var bAverage = Math.Abs(bMin + (int)((bMax - bMin) * i / max)) % 256;
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
        }


    }
}

