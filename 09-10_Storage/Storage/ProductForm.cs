using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Storage
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private Form1 parentForm;
        private string fullPath;
        private TreeNode treeNode;
        private Product product;

        /// <summary>
        /// Конструктор для создания товара.
        /// </summary>
        /// <param name="parentform"></param>
        /// <param name="fullTreePath"></param>
        /// <param name="treeNode"></param>
        internal ProductForm(Form1 parentform, string fullTreePath, TreeNode treeNode)
        {
            InitializeComponent();
            parentForm = parentform;
            fullPath = fullTreePath;
            this.treeNode = treeNode;
            Apply.Enabled = false;
        }

        /// <summary>
        /// Конструктор для редактирования товара.
        /// </summary>
        /// <param name="parentform"></param>
        /// <param name="product"></param>
        /// <param name="treeNode"></param>
        internal ProductForm(Form1 parentform, ref Product product, TreeNode treeNode)
        {
            InitializeComponent();
            parentForm = parentform;
            this.product = product;

            fullPath = product.FullTreePath;
            ProductName.Text = product.Name;
            IDProduct.Text = product.ID;
            PriceProduct.Text = product.Price.ToString();
            AmountProduct.Text = product.Count.ToString();
            DescriptiponProduct.Text = product.Description;
            PictureProduct.Image = product.Image;

            this.treeNode = treeNode;

            Ok.Enabled = false;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            if (product != null)
                AmountToBusket.Maximum = product.Count;
        }

        /// <summary>
        /// Применить изменения к товару.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, EventArgs e)
        {
            StringBuilder warningtext;
            double price;
            int amount;

            ParseDataFromUser(out warningtext, out price, out amount);

            if (warningtext.Length > 0)
                MessageBox.Show(warningtext.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                product.Name = ProductName.Text.Trim();
                product.ID = IDProduct.Text.Trim();
                product.Price = price;
                product.Count = amount;
                product.Description = DescriptiponProduct.Text.Trim();

                parentForm.DisplayProductList(treeNode);

            }
            this.Close();
        }

        /// <summary>
        /// Создать новый товар.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, EventArgs e)
        {
            // закрыть допформу.
            // создание нового объекта.
            // ФОТОГРАФИЯ.
            Image image = Image.FromFile(@"infopng.png");
            StringBuilder warningtext;
            double price;
            int amount;
            ParseDataFromUser(out warningtext, out price, out amount);

            if (warningtext.Length > 0)
                MessageBox.Show(warningtext.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                parentForm.LastAddedProduct = new Product(fullPath, ProductName.Text.Trim(), IDProduct.Text.Trim(), price, amount, DescriptiponProduct.Text.Trim(), image);

                if (parentForm.LastAddedProduct != null)
                {
                    parentForm.AddTreeProduct(parentForm.LastAddedProduct);
                    parentForm.DisplayProductList(treeNode);
                }
            }
            this.Close();
        }

        /// <summary>
        /// Проверка вхожных данных.
        /// </summary>
        /// <param name="warningtext"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        private void ParseDataFromUser(out StringBuilder warningtext, out double price, out int amount)
        {
            warningtext = new StringBuilder();
            var numberformat = new NumberFormatInfo { NumberDecimalSeparator = "." };

            if (!double.TryParse(PriceProduct.Text.Trim(), NumberStyles.AllowDecimalPoint, numberformat, out price) || price < 0)
                warningtext.Append($"Неверный формат цены! (пример: 100.0 ) {Environment.NewLine}");
            if (!int.TryParse(AmountProduct.Text.Trim(), out amount) || amount < 0)
                warningtext.Append($"Количествмо товара может быть целое число, больше либо равное 0! {Environment.NewLine}");
            if (ProductName.Text.Trim().Length == 0)
                warningtext.Append($"Введите имя товара! {Environment.NewLine}");
            if (IDProduct.Text.Trim().Length == 0)
                warningtext.Append($"Введите артикул товара! {Environment.NewLine}");
        }

        private void AddToBusket_Click(object sender, EventArgs e)
        {
            if (AmountToBusket.Value > 0)
            {
                if (parentForm.CurrentUser != null)
                    parentForm.CurrentUser.Basket.Add(new Tuple<int, Product>(Convert.ToInt32(Math.Round(AmountToBusket.Value, 0)), product));
            }
        }
    }
}
