using System;
using System.Text;
using System.Windows.Forms;

namespace Storage
{
    public partial class SighUpForm : Form
    {
        public SighUpForm(AuthorizationPage page, Form1 mainForm)
        {
            InitializeComponent();
            parentForm = page;
            this.mainForm = mainForm;
        }

        /// <summary>
        /// Ссылка на главную форму.
        /// </summary>
        private AuthorizationPage parentForm;
        private Form1 mainForm;

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // проверка введенных значений
            StringBuilder stringBuilder = ValidateInput();

            if (stringBuilder.Length > 0)
                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK);
            else
            {
                // выполенение регистрации пользователя.
                if (!mainForm.Storage.CleintAlreadyExist(EmailBox.Text.Trim()))
                {
                    if (AdressBox.Text == null)
                        AdressBox.Text = string.Empty;
                    if (PatronumicBox.Text == null)
                        PatronumicBox.Text = string.Empty;
                    try
                    {
                        Client currentClient = new Client(SurNameBox.Text.Trim(), NameBox.Text.Trim(),
                        PatronumicBox.Text.Trim(), PhoneNumberBox.Text.Trim(), EmailBox.Text.Trim(),
                        AdressBox.Text.Trim(), HashCode.GetHash(PasswordBox.Text.Trim(), EmailBox.Text.Trim()));
                        mainForm.Storage.Clients.Add(currentClient);

                        // закрытие окна и передача управления родительскому
                        MessageBox.Show("Вы зарегистрировались!", "Info", MessageBoxButtons.OK);
                        mainForm.CurrentUser = currentClient;
                        mainForm.SetActiveClientButtons();
                        this.Close();
                        parentForm.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                    }

                }
                else
                    MessageBox.Show("Пользователь с таким логином уже существует", "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Проерка корректности входных данных.
        /// </summary>
        /// <returns></returns>
        private StringBuilder ValidateInput()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(NameBox.Text.Trim()))
                stringBuilder.Append($"\"Имя\" - обязательное поле.{Environment.NewLine}");

            if (string.IsNullOrEmpty(SurNameBox.Text.Trim()))
                stringBuilder.Append($"\"Фамилия\" - обязательное поле.{Environment.NewLine}");

            if (string.IsNullOrEmpty(PhoneNumberBox.Text.Trim()))
                stringBuilder.Append($"\"Телефон\" - обязательное поле.{Environment.NewLine}");

            if (string.IsNullOrEmpty(EmailBox.Text.Trim()))
                stringBuilder.Append($"\"Email\" - обязательное поле.{Environment.NewLine}");

            if (string.IsNullOrEmpty(PasswordBox.Text.Trim()))
                stringBuilder.Append($"\"Пароль\" - обязательное поле.{Environment.NewLine}");

            if (!parentForm.ValidatePassword(PasswordBox.Text.Trim()))
                stringBuilder.Append($"Пароль должен содержать минимум 8 символов (также одну цифру и одну заглавную букву).{Environment.NewLine}");
            if (ConfirmPasswordBox.Text.Trim() != PasswordBox.Text.Trim())
                stringBuilder.Append($"Введенные пароли не совпадают.{Environment.NewLine}");

            return stringBuilder;
        }
    }
}
