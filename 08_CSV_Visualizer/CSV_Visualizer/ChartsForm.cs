using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CSV_Visualizer
{   
    /// <summary>
    /// Типы отрисовываемых графиков.
    /// </summary>
    enum ChartType
    { 
        FrequencyChart,
        CompareChart,
        LineChart
    }

    public partial class ChartsForm : Form
    {
        /// <summary>
        /// Тип построенного графика.
        /// </summary>
        private ChartType currentType;
        /// <summary>
        /// Словарь текущих значений X->Y.
        /// </summary>
        public Dictionary<string, int> currentDict;
        /// <summary>
        /// Лист текущих значений Y.
        /// </summary>
        public List<double> currentList;
        /// <summary>
        /// Подпись оси X текущего графика.
        /// </summary>
        public string currentXlabel;

        /// <summary>
        /// Преобразование в коллекцию ChartValues.
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns>ChartValues<int></returns>
        private ChartValues<int> AddValuesChart(Dictionary<string, int>.ValueCollection pairs)
        {
            var charts = new ChartValues<int>();
            charts.AddRange(pairs);
            return charts;
        }

        public ChartsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод отрисовки графика частотного анализа.
        /// </summary>
        /// <param name="pairs"></param>
        public void PrintCharts(Dictionary<string, int> pairs)
        {
            currentType = ChartType.FrequencyChart;

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = AddValuesChart(pairs.Values),
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {   
                Labels = pairs.Keys.ToList<string>()
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Frequency of occurrence",
                
            });
        }

        /// <summary>
        /// Преобразование List в ChartValues.
        /// </summary>
        /// <param name="numbersY"></param>
        /// <returns></returns>
        private ChartValues<double> AddValuesChart(List<double> numbersY)
        {
            var chart = new ChartValues<double>();
            chart.AddRange(numbersY);
            return chart;
        }

        /// <summary>
        /// Метод отрисовки линейного графика зависимости X->Y.
        /// </summary>
        /// <param name="numbersY">Список значений Y.</param>
        /// <param name="stringX">Список значений X.</param>
        /// <param name="titleY">Ось Y.</param>
        /// <param name="titleX">Ось X.</param>
        internal void PrintLineChats(List<double> numbersY, List<string> stringX, string titleY, string titleX)
        {
            currentType = ChartType.LineChart;

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = titleY,
                    LineSmoothness = 1,
                    Values = AddValuesChart(numbersY)
                }
            };

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = titleY,
            });
            

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = titleX,
                Labels = stringX
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
        }

        /// <summary>
        /// Метод построения графика числового сравнения.
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="titleX"></param>
        internal void PrintCompareCharts(List<double> numbers, string titleX)
        {
            currentType = ChartType.CompareChart;

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = AddValuesChart(numbers),
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = titleX
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Compare",
            });
        }

        /// <summary>
        /// Перерисовка графика с заданной величиной диапазона значений.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {   
            if (currentType == ChartType.FrequencyChart)
            {
                Dictionary<string, int> newdict = ProcessDataForRedrawFrequency();
                ClearCharts();
                try
                {
                    PrintCharts(newdict);
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно изменить ширину колонок графика!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }
            else if (currentType == ChartType.CompareChart)
            {
                List<double> newList = ProcessDataForRedrawCompare();
                ClearCharts();
                try
                {
                    PrintCompareCharts(newList, currentXlabel);
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно изменить ширину колонок графика!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Для линейного графика объединение в колонки недосутпно!", "Warning", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Метод очистки подписей осей.
        /// </summary>
        private void ClearCharts()
        {
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
        }

        /// <summary>
        /// Перевычисление значений для графика числового сравнения.
        /// </summary>
        /// <returns></returns>
        private List<double> ProcessDataForRedrawCompare()
        {
            List<double> newList = new List<double>();

            for (int i = 0; i < currentList.Count; i += (int)numericUpDown1.Value)
            {   
                // пробегаем по numericUpDown1.Value столбиков за раз.
                double sum = 0;
                for (int j = i; j < i + numericUpDown1.Value && j < currentList.Count; j++)
                {
                    sum += currentList[j];
                }
                newList.Add(sum / (int)numericUpDown1.Value);
            }

            // Добавляем остаток данных, если кол-во данных в листе не делится нацело на numericUpDown.Value.
            int remainder = currentList.Count % (int)numericUpDown1.Value;
            if (remainder > 0)
            {
                double sum = 0;
                for (int i = currentList.Count - remainder; i < currentList.Count; i++)
                {
                    sum += currentList[i];
                }
                newList.Add(sum / remainder);
            }

            return newList;
        }

        /// <summary>
        /// Перевычисление значений для графика частотного анализа.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> ProcessDataForRedrawFrequency()
        {
            Dictionary<string, int> newdict = new Dictionary<string, int>();
            List<int> freq = new List<int>();
            freq.AddRange(currentDict.Values);
            List<string> names = new List<string>();
            names.AddRange(currentDict.Keys);

            for (int i = 0; i < freq.Count; i += (int)numericUpDown1.Value)
            {   
                // пробегаем по numericUpDown1.Value столбиков за раз
                int sum = 0;
                for (int j = i; j < i + numericUpDown1.Value && j < freq.Count; j++)
                {
                    sum += freq[j];
                }
                newdict.Add($"{names[i]}", (int)(sum / numericUpDown1.Value));
            }

            // Добавляем остаток данных, если кол-во данных в словаре не делится нацело на numericUpDown.Value.
            int remainder = freq.Count % (int)numericUpDown1.Value;
            if (remainder > 0)
            {
                int sum = 0;
                for (int i = freq.Count - remainder; i < freq.Count; i++)
                {
                    sum += freq[i];
                }
                newdict.Add($"{names[names.Count - 1]}(1)", sum / remainder);
            }

            return newdict;
        }

        /// <summary>
        /// Смена цвета при клике на график.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="chartPoint"></param>
        private void cartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (currentType == ChartType.LineChart)
                    {
                        System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(colorDialog1.Color.A, colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);

                        ((LineSeries)chartPoint.SeriesView).Fill = new SolidColorBrush(color);
                    }
                    else
                    {
                        System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(colorDialog1.Color.A, colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);

                        ((ColumnSeries)chartPoint.SeriesView).Fill = new SolidColorBrush(color);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно поменять цвет графика!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        /// Метод сохранения графика в png формате.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cartesianChart1 != null)
            {
                Bitmap bmp = new Bitmap(cartesianChart1.Width, cartesianChart1.Height);
                cartesianChart1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                try
                {
                    saveFileDialog1.FileName = String.Concat(currentType.ToString(), ".png");
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно сохранить диаграмму!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("График пуст!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
            }
        }
    }
}

