using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Storage
{
    public partial class AddNode : Form
    {
        /// <summary>
        /// Универсальная форма для сбора информации.
        /// </summary>
        public AddNode()
        {
            InitializeComponent();
        }

        internal AddNode(Form1 form, ChangeOptions options)
        {
            InitializeComponent();
            parentForm = form;
            NodeOptions = options;
        }

        /// <summary>
        /// В зависимости от задачи: изменение текста пользователю.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNode_Load(object sender, EventArgs e)
        {
            if (NodeOptions == ChangeOptions.AddNode)
            {
                NameLanel.Text = "Имя раздела:";
                this.Text = "Добавление раздела";
            }
            else if (NodeOptions == ChangeOptions.ChangeNode)
            {
                NameLanel.Text = "Новое имя раздела:";
                this.Text = "Изменение раздела";
            }
            else if (NodeOptions == ChangeOptions.SaveFile)
            {
                NameLanel.Text = "Имя папки:";
                this.Text = "Сохранение склада";
            }
            else if (NodeOptions == ChangeOptions.RestoreFile)
            {
                NameLanel.Text = "Имя папки:";
                this.Text = "Загрузка склада";
            }
            else
            {
                NameLanel.Text = "Мин кол-во:";
                this.Text = "Создание CSV-отчета";
            }

        }

        /// <summary>
        /// Главная форма.
        /// </summary>
        public Form1 parentForm { get; private set; }

        /// <summary>
        /// Тип задачи для формы.
        /// </summary>
        internal ChangeOptions NodeOptions { get; private set; }

        /// <summary>
        /// Выполнить обработку для данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNameSection.Text.Trim()))
            {
                if (NodeOptions == ChangeOptions.AddNode)
                {
                    parentForm.LastInputName = textBoxNameSection.Text.Trim();
                    this.Close();
                    parentForm.AddNodeToTree();
                }
                else if (NodeOptions == ChangeOptions.ChangeNode)
                {
                    parentForm.LastInputName = textBoxNameSection.Text.Trim();
                    this.Close();
                    parentForm.ChangeNodeTree();
                }
                else if (NodeOptions == ChangeOptions.SaveFile)
                {
                    if (IsValidFilename(textBoxNameSection.Text.Trim()))
                    {
                        parentForm.LastSaveName = textBoxNameSection.Text.Trim();
                        this.Close();
                        parentForm.SerializeStorage();
                    }
                }
                else if (NodeOptions == ChangeOptions.RestoreFile)
                {
                    parentForm.LastSaveName = textBoxNameSection.Text.Trim();
                    this.Close();
                    parentForm.DeserializeStorage();
                }
                else
                {
                    if (ParseInput(textBoxNameSection.Text.Trim(), out int number))
                        parentForm.MakeCSVFile(number);
                }
                
            }
        }

        /// <summary>
        /// Проверка ввода.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool ParseInput(string text, out int amount)
        {
            if (!int.TryParse(text, out int number))
            {
                MessageBox.Show(this, "Недопустимое минимальное значение кол-ва товаров!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNameSection.Focus();
                amount = 0;
                return false;
            }
            else
            {
                amount = number;
                this.Close();
                return true;
            }

        }

        /// <summary>
        /// Проверка имени папки на корректность.
        /// </summary>
        /// <param name="testName"></param>
        /// <returns></returns>
        bool IsValidFilename(string testName)
        {
            Regex regex = new Regex("^([a-zA-Z0-9][^*/><?\"|:]*)$");
            if (!regex.IsMatch(testName))
            {
                MessageBox.Show(this, "Неверный формат имени папки", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxNameSection.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Событие ввода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNameSection_Enter(object sender, EventArgs e)
        {
            buttonOk_Click(sender, e);
        }

        private void textBoxNameSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                buttonOk_Click(sender, e);
            }
        }
    }
}
