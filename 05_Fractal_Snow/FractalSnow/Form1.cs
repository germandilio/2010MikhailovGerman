using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace FractalSnow
{
    public partial class Form1 : Form
    {   
        /// <summary>
        /// Корректность выбранного фрактала.
        /// </summary>
        static private bool ItemIsCorrect { get; set; }
        /// <summary>
        /// Изображение, на котором рисуются фракталы.
        /// </summary>
        static internal Bitmap buffer;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if (pictureBox1.Width <= 10 || pictureBox1.Height <= 10)
                buffer = new Bitmap(10,10);
            else
                buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Fractal.graphics = Graphics.FromImage(buffer);
            Fractal.pen1 = new Pen(Color.Black, 1);
            Fractal.pen2 = new Pen(Color.White, 1);
            Fractal.graphics.Clear(Color.White);
        }
        /// <summary>
        /// Перерисовка фрактала, при изменении размеров окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            button1_Click(sender, e);
        }
        /// <summary>
        /// Нажатие на кнопку "Нарисовать".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemIsCorrect != false)
            {
                switch (listBox.SelectedIndex)
                {
                    case 0:
                        Pifagor pifagor = new Pifagor(pictureBox1, textBox1, textBox2, textBox3);
                        pifagor.PrintFractal();
                        pictureBox1.Image = buffer;
                        break;
                    case 1:
                        Koch koch = new Koch(pictureBox1, textBox1);
                        koch.PrintFractal();
                        pictureBox1.Image = buffer;
                        break;
                    case 2:
                        SerpinskyCarpet serpinskyCarpet = new SerpinskyCarpet(pictureBox1, textBox1);
                        serpinskyCarpet.PrintFractal();
                        pictureBox1.Image = buffer;

                        break;
                    case 3:
                        SerpinskyTriangle serpinskyTriangle = new SerpinskyTriangle(pictureBox1, textBox1);
                        serpinskyTriangle.PrintFractal();
                        pictureBox1.Image = buffer;
                        break;
                    case 4:
                        Kantor kantor = new Kantor(pictureBox1, textBox1, textBox2);
                        kantor.PrintFractal();
                        pictureBox1.Image = buffer;
                        break;
                }
            }

        }
        /// <summary>
        /// Метод, который релойдит форму, при полноэкранном режиме.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
        /// <summary>
        /// Проверка и определение выбранного фрактала.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GUI();
            if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex <= 4)
                ItemIsCorrect = true;
            else
            {
                ItemIsCorrect = false;
                MessageBox.Show("Не выбран объект для рисования!", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Метод, отвечающий за видимость объектов интерфейса.
        /// </summary>
        private void GUI()
        {
            if (listBox.SelectedIndex == 0)
            {
                textBox3.Visible = true;
                label3.Visible = true;
                textBox2.Visible = true;
                label2.Visible = true;
                label1.Text = "Угол наклона первого отрезка";
                label2.Text = "Угол наклона второго отрезка";
                TextBoxWatermarkExtension.SetWatermark(textBox1, "от 0 до 45");
                TextBoxWatermarkExtension.SetWatermark(textBox2, "от 0 до 45");
                TextBoxWatermarkExtension.SetWatermark(textBox3, "от 1.4 до 5");
            }
            else if (listBox.SelectedIndex == 4)
            {
                textBox3.Visible = false;
                label3.Visible = false;
                label1.Text = "Глубина рекурсии";
                textBox2.Visible = true;
                label2.Text = "Расстояние между отрезками";
                label2.Visible = true;
                TextBoxWatermarkExtension.SetWatermark(textBox1, "от 1 до 10");
                TextBoxWatermarkExtension.SetWatermark(textBox2, "от 20 до 100");
            }
            else
            {
                label1.Text = "Глубина рекурсии";
                if (listBox.SelectedIndex == 2)
                    TextBoxWatermarkExtension.SetWatermark(textBox1, "от 1 до 8");
                else
                    TextBoxWatermarkExtension.SetWatermark(textBox1, "от 1 до 10");

                textBox2.Visible = false;
                label2.Visible = false;
                textBox3.Visible = false;
                label3.Visible = false;
            }
        }

        /// <summary>
        /// Сохранение картинки на компьютер.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Cоздание диалогового окна "Сохранить как..".
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                // Cписок форматов файла, отображаемый в поле "Тип файла".
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|" +
                    "*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(savedialog.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
        }
        /// <summary>
        /// Метод выбора начального цвета.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.BackColor = colorDialog1.Color;
                Fractal.startColor = colorDialog1.Color;
            }
        }
        /// <summary>
        /// Метод выбора конечного цвета.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.BackColor = colorDialog1.Color;
                Fractal.endColor = colorDialog1.Color;
            }
        }
    }

    /// <summary>
    /// Класс расширения TextBox, для реализации подсказок.
    /// (источник: cyberforum).
    /// </summary>
    public static class TextBoxWatermarkExtension
    {
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(this TextBox textBox, string watermarkText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
        }

    }
}
