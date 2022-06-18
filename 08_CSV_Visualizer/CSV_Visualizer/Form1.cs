using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSV_Visualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открытие CSV файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "CSV |*.csv";
            openFileDialog1.CheckFileExists = true;

            if (result == DialogResult.OK)
            {
                // Загрузка файла в таблицу.
                try
                {
                    ParseCSVFile(openFileDialog1.FileName);
                    toolStripStatusLabel1.Text = openFileDialog1.FileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия файла!", "Warning", MessageBoxButtons.OK,
                       MessageBoxIcon.Warning,
                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        /// Обработка файла и запись в таблицу графического интерфейса.
        /// </summary>
        /// <param name="fileName"></param>
        private void ParseCSVFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new ArgumentException("File not found");

            using (StreamReader reader = new StreamReader(fileName))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Концигурация чтения таблицы.
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null,
                    TrimOptions = TrimOptions.Trim
                };

                // Загрузка таблицы в DataViewGrid.
                using (var dr = new CsvDataReader(csvReader))
                {
                    var dataTable = new DataTable();
                    dataTable.Load(dr);

                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    dataGridView1.DataSource = dataTable;

                    // Установка NotSortable mode.
                    dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(column => column.SortMode = DataGridViewColumnSortMode.NotSortable);
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;

                    foreach (DataGridViewColumn item in dataGridView1.Columns)
                        item.MinimumWidth = 100;
                }
            }
        }

        /// <summary>
        /// Словарь для графика зависимости X->Y.
        /// </summary>
        public Dictionary<string, int> dict;

        /// <summary>
        /// Вызов контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ContextMenuStrip = contextMenu1;
            }
        }

        /// <summary>
        /// Обработка выбранных столбцов для графика X->Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedColumns != null && dataGridView1.SelectedColumns.Count == 2)
            {
                List<double> numbersY = new List<double>();
                List<string> stringX = new List<string>();
                string titleX = dataGridView1.SelectedColumns[1].HeaderText;
                string titleY = dataGridView1.SelectedColumns[0].HeaderText;

                int firstIndex = dataGridView1.SelectedCells[0].ColumnIndex;
                if (CheckDataInCloumns(firstIndex, dataGridView1.SelectedRows.Count))
                {
                    ProcessDataForPlot(ref numbersY, ref stringX, ref titleX, ref titleY, ref firstIndex);

                    // Отрисовка графика.
                    ChartsForm form = new ChartsForm();
                    form.PrintLineChats(numbersY, stringX, titleY, titleX);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Столбец Y не является числовым! Необходимо сначала выбрать столбец X, затем Y.", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Необхордимо выбрать 2 столбца!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Обработка(парсинг) выбранных числовых ячеек.
        /// </summary>
        /// <param name="numbersY"></param>
        /// <param name="stringX"></param>
        /// <param name="titleX"></param>
        /// <param name="titleY"></param>
        /// <param name="firstIndex"></param>
        private void ProcessDataForPlot(ref List<double> numbersY, ref List<string> stringX, ref string titleX, ref string titleY, ref int firstIndex)
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.Value != null)
                {
                    if (cell.ColumnIndex == firstIndex)
                    {
                        if (double.TryParse(cell.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cash))
                            numbersY.Add(cash);
                        else
                            numbersY.Add(0);
                    }
                    else
                    {
                        stringX.Add(cell.Value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Провекрка что столбец является числовым.
        /// </summary>
        /// <param name="columnY"></param>
        /// <param name="countOfRows"></param>
        /// <returns></returns>
        private bool CheckDataInCloumns(int columnY, int countOfRows)
        {
            double percent = 0.8;
            // Если не парсится больше (percent) процентов, то колонки выбраны в неправильном формате.
            int counter = 0;
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.Value != null && cell.ColumnIndex == columnY)
                    if (!double.TryParse(cell.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cash))
                        counter++;

            }
            if (counter > countOfRows * percent)
                return false;
            return true;
        }

        /// <summary>
        /// Метод расчета данных для частотного анализа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedColumns != null)
            {
                // словарь для частотного анализа.
                dict = new Dictionary<string, int>();

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (cell.Value != null)
                    {
                        if (!dict.ContainsKey(cell.Value.ToString()))
                            dict.Add(cell.Value.ToString(), 1);
                        else
                            dict[cell.Value.ToString()]++;
                    }
                }

                ChartsForm form = new ChartsForm();
                form.currentDict = dict;
                form.PrintCharts(dict);
                form.Show();
            }
            else
            {
                MessageBox.Show("Необхордимо выбрать более одной ячейки!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Метод вывода аналитики.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnaliticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count >= 1)
            {
                int index = dataGridView1.SelectedCells[0].ColumnIndex;

                if (CheckDataInCloumns(index, dataGridView1.SelectedRows.Count))
                {
                    double average, median, dispersion, CKO;
                    GetAnalitics(out average, out median, out dispersion, out CKO);

                    string analitics = $"Average: {average:F3}. {Environment.NewLine}Median: {median:F3}." +
                        $" {Environment.NewLine}Dispersion: {dispersion:F3}. {Environment.NewLine}Average square deviation {CKO:F3}.";

                    MessageBox.Show(analitics, "Analitics", MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("Необхордимо выбрать 1 числовой столбец!", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        /// Метод подсчета аналитических(статистических) показателей.
        /// </summary>
        /// <param name="average"></param>
        /// <param name="median"></param>
        /// <param name="dispersion"></param>
        /// <param name="CKO"></param>
        private void GetAnalitics(out double average, out double median, out double dispersion, out double CKO)
        {
            List<double> numbers = new List<double>();
            double sum = 0;
            int counter = 0;

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.Value != null)
                    if (double.TryParse(cell.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cash))
                    {
                        numbers.Add(cash);
                        sum += cash;
                        counter++;
                    }
            }
            numbers.Sort();

            if (numbers.Count != 0)
                median = numbers[numbers.Count / 2];
            else
                median = 0;

            if (counter != 0)
                average = sum / counter;
            else
                average = 0;

            double sumD = 0;
            numbers.ForEach(x => sumD += Math.Pow(x, 2));

            dispersion = sumD / numbers.Count - Math.Pow(sum / numbers.Count, 2);
            CKO = Math.Sqrt(dispersion);
        }

        /// <summary>
        /// Метод подготовки данных для построения графика числового сравнения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedColumns != null)
            {
                List<double> numbers = new List<double>();

                int firstIndex = dataGridView1.SelectedCells[0].ColumnIndex;
                string titleX = dataGridView1.Columns[firstIndex].HeaderText;

                if (CheckDataInCloumns(firstIndex, dataGridView1.SelectedRows.Count))
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.Value != null)
                            if (double.TryParse(cell.Value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double cash))
                                numbers.Add(cash);
                    }

                    ChartsForm form = new ChartsForm();
                    form.currentList = numbers;
                    form.currentXlabel = titleX;
                    form.PrintCompareCharts(numbers, titleX);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Столбец не является числовым!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Установка режима растяжения таблицы до размера окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
    }
}
