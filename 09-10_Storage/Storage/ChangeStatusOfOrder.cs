using System;
using System.Windows.Forms;

namespace Storage
{
    public partial class ChangeStatusOfOrder : Form
    {
        internal ChangeStatusOfOrder(Order order, AllOrders allOrdersForm)
        {
            InitializeComponent();
            Order = order;
            AllOrdersForm = allOrdersForm;
        }
        /// <summary>
        /// Текущий заказ.
        /// </summary>
        private Order Order { get; set; }
        /// <summary>
        /// Форма со всеми заказами.
        /// </summary>
        private AllOrders AllOrdersForm { get; set; }

        /// <summary>
        /// Текущий, выбранный пользователем статус заказа.
        /// </summary>
        private Status CurrentStatus;

        /// <summary>
        /// Обработка выбора пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeepStay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton radioButton = (RadioButton)sender;
                if (radioButton.Checked)
                {
                    //MessageBox.Show("Вы выбрали " + radioButton.Text);
                    if (radioButton.Text == "Обработан")
                    {
                        CurrentStatus = Status.Processed;
                    }
                    else if (radioButton.Text == "Отгружен")
                    {
                        CurrentStatus = Status.Uncoiled;
                    }
                    else if (radioButton.Text == "Исполнен")
                    {
                        CurrentStatus = Status.Executed;
                    }
                    else if (radioButton.Text == "Не изменять")
                        CurrentStatus = Status.Default;
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Подтверждения изменяемого значения статуса заказа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (CurrentStatus == Status.Default)
                this.Close();

            if (CurrentStatus == Status.Processed)
                Order.Status = CurrentStatus;
            else
            {
                Order.Status |= CurrentStatus;
            }
            AllOrdersForm.LoadAllClients();
            this.Close();
        }
    }
}
