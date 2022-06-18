using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Storage
{
    public partial class AllOrders : Form
    {
        public AllOrders(Form1 parentForm)
        {
            InitializeComponent();
            MainForm = parentForm;
            LoadAllClients();

        }

        /// <summary>
        /// Закрузка списка всех клиентов.
        /// </summary>
        internal void LoadAllClients()
        {
            if (MainForm.Storage.Clients != null)
            {
                listView2.Items.Clear();
                listView1.Items.Clear();
                SumByClient.Text = "0";
                listView2.Items.AddRange(ConvertToListView(MainForm.Storage.Clients));
            }
        }

        /// <summary>
        /// Ссылка на основную форму.
        /// </summary>
        private Form1 MainForm { get; set; }

        /// <summary>
        /// Конвертация клиентов в массив ListViewItem.
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        private ListViewItem[] ConvertToListView(List<Client> clients)
        {
            ListViewItem[] items = new ListViewItem[clients.Count];
            for (int i = 0; i < items.Length; i++)
            {
                ListViewItem item = new ListViewItem(clients[i].Surname);
                var subitems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, clients[i].Name),
                    new ListViewItem.ListViewSubItem(item, clients[i].Patronymic),
                    };
                item.SubItems.AddRange(subitems);
                items[i] = item;
            }
            return items;
        }

        /// <summary>
        /// Обаботчик клика мышкой по списку клиентов.
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
                        ShowAllOrders(e, i);
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Метод отображения всех заказов.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="i"></param>
        private void ShowAllOrders(MouseEventArgs e, int i)
        {
            // listView1.Items[i] - текущий выбор
            Client currentClient = MainForm.Storage.Clients.Find(client => client.Surname == listView2.Items[i].SubItems[0].Text && client.Name == listView2.Items[i].SubItems[1].Text && client.Patronymic == listView2.Items[i].SubItems[2].Text);
            if (currentClient != null)
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(ConvertToListView(currentClient.Orders, out double price));
                SumByClient.Text = price.ToString();
            }
        }

        /// <summary>
        /// Список заказов и рассчет суммы оплаченных заказов.
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        private ListViewItem[] ConvertToListView(List<Order> orders, out double totalPrice)
        {
            ListViewItem[] items = new ListViewItem[orders.Count];
            totalPrice = 0.0;
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

                if (orders[i].Status.HasFlag(Status.Paid))
                    totalPrice += orders[i].Price;
            }
            return items;
        }

        /// <summary>
        /// Обработчик клика мышки по списку заказов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var rectangle = listView1.GetItemRect(i);
                if (rectangle.Contains(e.Location))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ChangeStatus(e, i);
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Метод смены статуса заказа.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="i"></param>
        private void ChangeStatus(MouseEventArgs e, int i)
        {
            var order = MainForm.Storage.AllOrders.Find(order => order.ID.ToString() == listView1.Items[i].SubItems[0].Text && order.OrderDate.ToString() == listView1.Items[i].SubItems[1].Text);
            if (order != null)
            {
                ChangeStatusOfOrder changeStatusOfOrderForm = new ChangeStatusOfOrder(order, this);
                changeStatusOfOrderForm.Show();
            }

        }

        /// <summary>
        /// Показ списка всех заказов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllOrdersView_Click(object sender, EventArgs e)
        {
            ViewAllOrders allOrdersForm = new ViewAllOrders(MainForm);
            allOrdersForm.Show();
            this.Close();
        }
    }
}
