using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Storage
{
    public partial class AuthorizationPage : Form
    {
        public AuthorizationPage(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        /// <summary>
        /// Ссылка на главную форму.
        /// </summary>
        private Form1 parentForm;

        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // вызов SignUpForm
            SighUpForm sighUpForm = new SighUpForm(this, parentForm);
            this.Close();
            sighUpForm.Show();
        }

        /// <summary>
        /// Вход пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();

            if (textBox1.TextLength <= 0)
            {
                errorProvider1.SetError(textBox1, "Введите логин.");
                return;
            }
            if (textBox2.TextLength <= 0)
            {
                errorProvider2.SetError(textBox1, "Введите пароль.");
                return;
            }


            if (parentForm.ApplicationMode == Mode.ClientMode)
                LoginClent();
            else if (parentForm.ApplicationMode == Mode.SupplierMode)
                LoginSupplier();
        }

        /// <summary>
        /// Вход админа(продавца).
        /// </summary>
        private void LoginSupplier()
        {
            if (Properties.Resources.AdminSupplierLogin == textBox1.Text.Trim() && Properties.Resources.AdminSupplierPassword == HashCode.GetHash(textBox2.Text.Trim(), textBox1.Text.Trim()))
            {
                MessageBox.Show("Вы администратор этого склада, теперь вам доступно изменение товаров и категорий, создание CSV отчета и сохранения склада!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                parentForm.SetActiveSupplierButtons();
                this.Close();
            }
        }

        /// <summary>
        /// Логин клиента.
        /// </summary>
        private void LoginClent()
        {
            // выполенение входа пользователя.
            // проверить в базе данных: если совпадает то отобразить заказы и корзину
            var user = parentForm.Storage.Clients.Find(client => client.EMail == textBox1.Text.Trim() && client.Password == HashCode.GetHash(textBox2.Text.Trim(), textBox1.Text.Trim()));
            if (user != null)
            {
                parentForm.CurrentUser = user;
                MessageBox.Show($"Вы успешно вошли, {user.Name}!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                parentForm.SetActiveClientButtons();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Проверка корректности пароля на длину по мере ввода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ValidatePassword(textBox2.Text.Trim()))
                errorProvider1.Clear();
            else
                errorProvider1.SetError(textBox2, "Пароль должен содержать минимум 8 символов (также одну цифру и одну заглавную букву).");
        }

        /// <summary>
        /// Проерка корректности пароля в соответствии требованиям безопасности.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool ValidatePassword(string password)
        {
            // проверка на корректность ввода пароля
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            return isValidated;
        }

        /// <summary>
        /// Проверка логина на допустимую длину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
                errorProvider2.SetError(textBox1, "Обязательное поле!");
            else
                errorProvider2.Clear();
        }

        /// <summary>
        /// Выбор режима приложения клиент/продавец.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton radioButton = (RadioButton)sender;
                if (radioButton.Checked)
                {
                    //MessageBox.Show("Вы выбрали " + radioButton.Text);
                    if (radioButton.Text == "Продавец")
                    {
                        parentForm.ApplicationMode = Mode.SupplierMode;
                        SignUp.Enabled = false;
                        textBox1.Text = Properties.Resources.AdminSupplierLogin;
                        textBox2.Text = "Admin001";
                    }
                    else
                    {
                        parentForm.ApplicationMode = Mode.ClientMode;
                        SignUp.Enabled = true;
                        textBox1.Text = String.Empty;
                        textBox2.Text = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
