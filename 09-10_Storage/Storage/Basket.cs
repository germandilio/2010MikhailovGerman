using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Storage
{ 
    public partial class Basket : Form
    {
        internal Basket(Client client, Form1 parentForm)
        {
            InitializeComponent();
            CurrentClient = client;
            this.parentForm = parentForm;
            ShowBasketAndOrders();

        }

        /// <summary>
        /// Ссылка на главную форму.
        /// </summary>
        private Form1 parentForm;

        /// <summary>
        /// Загрузка корзины и заказов.
        /// </summary>
        private void ShowBasketAndOrders()
        {
            if (CurrentClient != null)
            {
                listView1.Items.AddRange(ConvertToListView(CurrentClient.Basket));
                listView2.Items.AddRange(ConvertToListView(CurrentClient.Orders));
                if (CurrentClient.Basket.Count == 0)
                    CheckOut.Enabled = false;
            }
        }

        /// <summary>
        /// Конвертация заказов в массив ListViewItem.
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private ListViewItem[] ConvertToListView(List<Order> orders)
        {
            ListViewItem[] items = new ListViewItem[orders.Count];
            for (int i = 0; i < items.Length; i++)
            {
                ListViewItem item = new ListViewItem(orders[i].ID.ToString());
                var subitems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, orders[i].OrderDate.ToString()),
                    new ListViewItem.ListViewSubItem(item, orders[i].Price.ToString()),
                    new ListViewItem.ListViewSubItem(item, orders[i].Status.ToString())
                    };
                item.SubItems.AddRange(subitems);
                items[i] = item;
            }
            return items;
        }

        /// <summary>
        /// Коныертация корзины в отображаемый список.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        private ListViewItem[] ConvertToListView(List<Tuple<int, Product>> basket)
        {
            ListViewItem[] items = new ListViewItem[basket.Count];
            for (int i = 0; i < items.Length; i++)
            {
                ListViewItem item = new ListViewItem(basket[i].Item2.Name);
                var subitems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, basket[i].Item2.ID),
                    new ListViewItem.ListViewSubItem(item, basket[i].Item2.Count.ToString()),
                    new ListViewItem.ListViewSubItem(item, basket[i].Item2.Price.ToString())
                    };
                item.SubItems.AddRange(subitems);
                items[i] = item;
            }
            return items;
        }

        /// <summary>
        /// Текущий клиент приложения.
        /// </summary>
        private Client CurrentClient { get; set; }
        
        /// <summary>
        /// Обработчик кнопки оформить заказ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOut_Click(object sender, EventArgs e)
        {
            // Оформить заказ на текущего клиента и добавить его в список.
            if (CurrentClient.Basket.Count > 0)
            {   
                var order = new Order(CurrentClient.Basket.Select(item => item.Item2).ToList(), CalculatePrice(CurrentClient.Basket), CurrentClient);
                CurrentClient.Orders.Add(order);
                parentForm.Storage.AllOrders.Add(order);

                listView2.Items.Clear();
                listView2.Items.AddRange(ConvertToListView(CurrentClient.Orders));
                // Очистить корзину.
                //listView1.Items.Clear();
            }
        }

        /// <summary>
        /// Метод расчета стоимости корзины.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        private double CalculatePrice(List<Tuple<int, Product>> basket)
        {
            if (basket.Count == 0)
            {
                MessageBox.Show("0 товаров в корзине!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            double price = 0.0;
            for (int i = 0; i < basket.Count; i++)
                price += basket[i].Item1 * basket[i].Item2.Price;
            return price;
        }

        /// <summary>
        /// Обработчик клика мыши по списку заказов(оплата заказа).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                var rectangle = listView2.GetItemRect(i);
                if (rectangle.Contains(e.Location))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        PaymentPageOrder(e, i);
                        listView2.Items.Clear();
                        listView2.Items.AddRange(ConvertToListView(CurrentClient.Orders));
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Показ платежной страницы заказа.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        private void PaymentPageOrder(MouseEventArgs e, int index)
        {
            // listView1.Items[i] - текущий выбор.
            Order currentOrder = CurrentClient.Orders.Find(order => order.ID.ToString() == listView2.Items[index].SubItems[0].Text);
            // Вызвать форму с карточкой товара с заполненными данными.
            if (currentOrder != null)
            {
                if (!currentOrder.Status.HasFlag(Status.Paid) && currentOrder.Status.HasFlag(Status.Processed))
                {
                    DialogResult result = MessageBox.Show($"Желаете оплатить заказ ID={currentOrder.ID}, на сумму: {currentOrder.Price} RUB?",
                        "Оплата заказа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                        currentOrder.Status |= Status.Paid;
                }
            }
        }
    }
}
