using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Storage
{
    public partial class ViewAllOrders : Form
    {
        public ViewAllOrders(Form1 mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            listView1.Items.Clear();
            listView1.Items.AddRange(ConvertToListView(mainForm.Storage.AllOrders));
        }

        /// <summary>
        /// Ссылка на главную форму.
        /// </summary>
        private Form1 MainForm { get; set; }

        /// <summary>
        /// Конвертация заказов в ListViewItem массив.
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
        /// Отображения заказов: активные/все.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            if (OnlyActive.Checked == false)
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(ConvertToListView(MainForm.Storage.AllOrders));
            }
            else
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(ConvertToListViewActiv(MainForm.Storage.AllOrders));
            }

        }

        /// <summary>
        /// Конвертация активных товаров в массив ListViewItem.
        /// </summary>
        /// <param name="allOrders"></param>
        /// <returns></returns>
        private ListViewItem[] ConvertToListViewActiv(List<Order> allOrders)
        {
            List<ListViewItem> items = new List<ListViewItem>();
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (!allOrders[i].Status.HasFlag(Status.Executed))
                {
                    ListViewItem item = new ListViewItem(allOrders[i].ID.ToString());
                    var subitems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, allOrders[i].OrderDate.ToString()),
                    new ListViewItem.ListViewSubItem(item, allOrders[i].Price.ToString()),
                    new ListViewItem.ListViewSubItem(item, allOrders[i].Status.ToString())
                    };
                    item.SubItems.AddRange(subitems);
                    items.Add(item);
                }
            }
            return items.ToArray();
        }
    }
}
