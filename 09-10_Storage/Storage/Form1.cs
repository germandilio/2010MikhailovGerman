using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using CsvHelper;
using System.Globalization;

namespace Storage
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Текущий клиент приложения.
        /// </summary>
        internal Client CurrentUser;
        /// <summary>
        /// Режим приложения (для продавца)/(для покупателя). 
        /// </summary>
        internal Mode ApplicationMode;

        public Form1()
        {
            InitializeComponent();

            Storage = new Storage();

            LastSaveName = "Cars";
            DeserializeStorage("Cars");

            // Сделать некоторые кнопки неактиывнми.
            SetEnableStatusToControl(false);
            saveAsToolStripMenuItem.Enabled = false;
            SaveStorageButton.Enabled = false;
            //
            openToolStripMenuItem.Enabled = false;
            //
            AddParentNode.Enabled = false;
            Basket.Enabled = false;
            ClientsButton.Visible = false;
        }

        /// <summary>
        /// Последнее веденное имя пользователем для изменения(создания) treeNode.
        /// </summary>
        public string LastInputName { get; set; }

        /// <summary>
        /// Главный объект: Хранилище.
        /// </summary>
        internal Storage Storage { get; private set; }

        /// <summary>
        /// TreeNode - родитель.
        /// </summary>
        public bool IsParent;

        /// <summary>
        /// Добавить подраздел.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChildNode_Click(object sender, EventArgs e)
        {
            IsParent = false;
            AddNode addForm = new AddNode(this, ChangeOptions.AddNode);
            addForm.Show();
        }

        /// <summary>
        /// Добавить родительский раздел.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParentNode_Click(object sender, EventArgs e)
        {
            IsParent = true;
            AddNode addForm = new AddNode(this, ChangeOptions.AddNode);
            addForm.Show();
        }

        /// <summary>
        /// Удалить подраздел (событие клик).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameNode_Click(object sender, EventArgs e)
        {
            AddNode addForm = new AddNode(this, ChangeOptions.ChangeNode);
            addForm.Show();
        }

        /// <summary>
        /// Удаление подраздела (обработка).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSection_Click(object sender, EventArgs e)
        {
            // Selected remove.
            // If haschild то предупредить.
            // Найти в глобальном списке. потом во всех остальных.
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Nodes.Count == 0)
            {
                treeView1.Update();
                treeView1.Nodes.Remove(treeView1.SelectedNode);

                if (treeView1.Nodes.Count == 0)
                    SetEnableStatusToControl(false);

                treeView1.EndUpdate();

                // запустить удалние всез товаров в этом Node и самого Section 
                if (treeView1.SelectedNode != null)
                {
                    Section removableSection = FindSectionByName(Storage.AllTreeSections, treeView1.SelectedNode.Text, out List<Section> collection);
                    collection.Remove(removableSection);
                }
            }
            else
            {
                MessageBox.Show(text: "Удаляемый раздел содержит подразделы. Удаление невозможно в целях безопасности.", caption: "Warning",
                        buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Найти подраздел на складе по имени.
        /// </summary>
        /// <param name="collection">Первоначальная коллекция</param>
        /// <param name="Name">Имя для поиска</param>
        /// <param name="sections"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        internal static Section FindSectionByName(List<Section> collection, string Name, out List<Section> sections, StringComparison comparison = StringComparison.InvariantCulture)
        {
            var foundNode = collection.FirstOrDefault(tn => string.Equals(tn.Name, Name, comparison));
            if (null == foundNode)
            {
                foreach (var childNode in collection)
                {
                    var foundChildNode = FindSectionByName(childNode.childSections, Name, comparison);
                    if (null != foundChildNode)
                    {
                        sections = childNode.childSections;
                        return foundChildNode;
                    }
                }
            }
            sections = collection;
            return foundNode;
        }

        /// <summary>
        /// Добавить TreeNode
        /// </summary>
        public void AddNodeToTree()
        {
            if (!string.IsNullOrEmpty(LastInputName) && treeView1.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Text == LastInputName) == null)
            {
                treeView1.BeginUpdate();

                if (treeView1.Nodes.Count == 0)
                    SetEnableStatusToControl(true);
                UpdateTree();
                treeView1.EndUpdate();
            }
            else
            {
                MessageBox.Show(text: "Раздел с таким имененем уже существует!", caption: "Error",
                    buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }

            void UpdateTree()
            {
                if (IsParent)
                {
                    treeView1.Nodes.Add(new TreeNode(LastInputName));
                    Storage.AllTreeSections.Add(new Section(LastInputName));
                }
                else
                {
                    TreeNode parentNode = treeView1.SelectedNode ?? treeView1.Nodes[0];

                    if (parentNode != null)
                    {
                        parentNode.Nodes.Add(LastInputName);
                        Section section = FindSectionByName(Storage.AllTreeSections, parentNode.Text);
                        section.childSections.Add(new Section(LastInputName));
                        treeView1.ExpandAll();
                    }
                }
            }
        }

        /// <summary>
        /// Изменить имя TreeNode.
        /// </summary>
        internal void ChangeNodeTree()
        {
            if (treeView1.SelectedNode != null)
            {
                var oldNode = treeView1.SelectedNode;
                Section result = FindSectionByName(Storage.AllTreeSections, oldNode.Text);
                if (result != null)
                {
                    result.Name = LastInputName;
                    RenameAllTreePath(oldNode, LastInputName, result);
                }

                treeView1.BeginUpdate();
                treeView1.SelectedNode.Text = LastInputName;
                treeView1.EndUpdate();
            }
        }

        /// <summary>
        /// Перименовать все пути, которые содержали переименованный подраздел классификатора.
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newName"></param>
        /// <param name="section"></param>
        private void RenameAllTreePath(TreeNode oldNode, string newName, Section section)
        {
            List<Product> products = Storage.Allproducts.FindAll(prdct => prdct.FullTreePath.Contains(oldNode.Text));

            for (int i = 0; i < products.Count; i++)
                products[i].ChangeFullPaths(products[i].FullTreePath, newName);
        }

        /// <summary>
        /// Найти подраздел на складе по имени.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="Name"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        internal static Section FindSectionByName(List<Section> collection, string Name, StringComparison comparison = StringComparison.InvariantCulture)
        {
            var foundNode = collection.FirstOrDefault(tn => string.Equals(tn.Name, Name, comparison));
            if (null == foundNode)
            {
                foreach (var childNode in collection)
                {
                    var foundChildNode = FindSectionByName(childNode.childSections, Name, comparison);
                    if (null != foundChildNode)
                    {
                        return foundChildNode;
                    }
                }
            }
            return foundNode;
        }

        /// <summary>
        /// Последний добавленный товар.
        /// </summary>
        internal Product LastAddedProduct { get; set; }

        /// <summary>
        /// Добавить продукт (событие клик).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, EventArgs e)
        {
            // Вызов доп формы для заполнения полей.
            // Создание продукта.
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                ProductForm productform = new ProductForm(this, selectedNode.FullPath, selectedNode);
                productform.Show();
            }

        }

        internal void DisplayProductList(TreeNode treeNode)
        {
            // clear ListView
            // создание в цикле всех объектов из section.Products
            listView1.Items.Clear();

            foreach (var product in Storage.Allproducts)
            {
                if (treeNode != null && product.FullTreePath == treeNode.FullPath)
                {
                    ListViewItem item = new ListViewItem(product.Name);
                    var subitems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, product.ID),
                    new ListViewItem.ListViewSubItem(item, product.Count.ToString()),
                    new ListViewItem.ListViewSubItem(item, product.Price.ToString())
                    };
                    item.SubItems.AddRange(subitems);
                    listView1.Items.Add(item);
                }
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Показ товаров в выбранном подразделе.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // показ всех товаров в listView
            if (e.Node != null)
            {
                //Section section = FindSectionByName(Storage.AllTreeSections, e.Node.Text);
                DisplayProductList(e.Node);
            }
        }

        /// <summary>
        /// Показ карточки товара.
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
                        OpenProductForm(e, i);
                    else if (e.Button == MouseButtons.Right)
                        RemoveProductNew(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Открыть форму с информацие о продукте.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="i"></param>
        private void OpenProductForm(MouseEventArgs e, int i)
        {
            // listView1.Items[i] - текущий выбор
            Product currentProduct = Storage.Allproducts.Find(product => product.Name == listView1.Items[i].Text && product.ID == listView1.Items[i].SubItems[1].Text);
            // вызвать форму с карточкой товара с заполненными данными
            if (currentProduct != null)
            {
                ProductForm productForm = new ProductForm(this, ref currentProduct, treeView1.SelectedNode);
                productForm.Show();
            }
        }

        /// <summary>
        /// Добавить TreeNode.
        /// </summary>
        /// <param name="product"></param>
        internal void AddTreeProduct(Product product)
        {
            if (Storage.Allproducts.Find(prdct => prdct.Name == product.Name && prdct.ID == product.ID) == null)
                Storage.Allproducts.Add(product);
            else
            {
                MessageBox.Show("Товар с таким именем и артикулом уже есть на складе!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить проукт.
        /// </summary>
        /// <param name="i"></param>
        internal void RemoveProductNew(int i)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этот товар,", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Storage.Allproducts.RemoveAll(product => product.Name == listView1.Items[i].Text && product.ID == listView1.Items[i].SubItems[1].Text);
                if (treeView1.SelectedNode != null)
                    DisplayProductList(treeView1.SelectedNode);
            }
        }

        /// <summary>
        /// Загрузить склад (событие клик).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialize(object sender, EventArgs e)
        {
            AddNode inputUserForm = new AddNode(this, ChangeOptions.RestoreFile);
            inputUserForm.Show();
        }

        /// <summary>
        /// Десериализация склада.
        /// </summary>
        internal void DeserializeStorage()
        {
            if (string.IsNullOrEmpty(LastSaveName))
                return;

            string directory = $"..\\..\\..\\SavedStorages\\{LastSaveName}";
            string filename1 = $"{directory}\\Storage.json";
            string filename2 = $"{directory}\\TreeView.bin";

            try
            {
                using (FileStream stream = new FileStream(filename1, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Storage = JsonConvert.DeserializeObject<Storage>(reader.ReadToEnd());
                }

                using (FileStream stream = new FileStream(filename2, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    treeView1.Nodes.Clear();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    var nodes = (List<TreeNode>)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                    treeView1.Nodes.AddRange(nodes.ToArray());
                    treeView1.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки склада:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SetEnableStatusToControl(true);

        }

        /// <summary>
        /// Установить статус Enabled кнопкам.
        /// </summary>
        /// <param name="status"></param>
        private void SetEnableStatusToControl(bool status)
        {
            RemoveSection.Enabled = status;
            RenameNode.Enabled = status;
            AddProduct.Enabled = status;
            AddChildNode.Enabled = status;
            //SaveStorageButton.Enabled = status;
            MakeCSV.Enabled = status;
            ClientsButton.Enabled = status;
        }

        /// <summary>
        /// Последнее введенное имя папки.
        /// </summary>
        public string LastSaveName { get; set; }

        /// <summary>
        /// Сохранение состояния склада (событие клик).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serialize(object sender, EventArgs e)
        {
            AddNode inputUserForm = new AddNode(this, ChangeOptions.SaveFile);
            inputUserForm.Show();
        }

        /// <summary>
        /// Сохранение состояния склада (обработка данных в json и binary).
        /// </summary>
        public void SerializeStorage()
        {
            try
            {
                string directory = $"..\\..\\..\\SavedStorages\\{LastSaveName}";
                Directory.CreateDirectory(directory);

                string filename1 = $"{directory}\\Storage.json";
                string filename2 = $"{directory}\\TreeView.bin";

                if (LastSaveName == null || File.Exists(filename1) || File.Exists(filename2))
                {
                    MessageBox.Show("В этой папке уже имеются файлы Json. Для успешного сохранения следует ввести имя для создания новой папки!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Storage.
                using (FileStream stream = new FileStream(filename1, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string storageJson = JsonConvert.SerializeObject(this.Storage, Newtonsoft.Json.Formatting.Indented);
                    writer.Write(storageJson);
                }
                // TreeView.
                using (FileStream stream = new FileStream(filename2, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    formatter.Serialize(stream, treeView1.Nodes.Cast<TreeNode>().ToList());
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Создать CSV-отчет (событие клик).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeCSV_Click(object sender, EventArgs e)
        {
            AddNode inputForm = new AddNode(this, ChangeOptions.NumberToCSV);
            inputForm.Show();
        }

        /// <summary>
        ///Создать CSV-отчет(обработка данных).
        /// </summary>
        /// <param name="number"></param>
        public void MakeCSVFile(int number)
        {
            saveFileDialog1.Title = "Save As";
            saveFileDialog1.Filter = "CSV File(*.csv)|*.csv";
            saveFileDialog1.FileName = String.Empty;
            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog1.FileName;

            try
            {
                using (var writer = new StreamWriter(filename))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<ObjectMap>();
                    csv.WriteRecords(ParseProducts(number));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла: {filename}. {ex.Message} ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Выбрать со склада товары, количество которых меньше чем указано пользователем.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<CSVProduct> ParseProducts(int number)
        {
            List<Product> products = Storage.Allproducts.FindAll(product => product.Count < number);

            List<CSVProduct> csvProducts = new List<CSVProduct>();
            foreach (var item in products)
                csvProducts.Add(new CSVProduct(item.FullTreePath, item.ID, item.Name, item.Count.ToString()));
            return csvProducts;
        }

        /// <summary>
        /// Выход из программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// About click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Описание программы находится в фале README", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
        }

        /// <summary>
        /// Login пользователя или его регистрация.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Click(object sender, EventArgs e)
        {
            AuthorizationPage authorizationPage = new AuthorizationPage(this);
            authorizationPage.Show();
            // выставление режима работы приложенния и тд
            ApplicationMode = Mode.ClientMode;
        }

        /// <summary>
        /// Режим клиента.
        /// </summary>
        public void SetActiveClientButtons()
        {
            SetEnableStatusToControl(false);
            Basket.Enabled = true;
            saveAsToolStripMenuItem.Enabled = false;
            SaveStorageButton.Enabled = false;
            AddParentNode.Enabled = false;
            ClientsButton.Visible = false;
        }

        /// <summary>
        /// Режим продавца.
        /// </summary>
        public void SetActiveSupplierButtons()
        {
            SetEnableStatusToControl(true);
            Basket.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            SaveStorageButton.Enabled = false;
            AddParentNode.Enabled = true;
            ClientsButton.Visible = true;
        }

        /// <summary>
        /// Показать корзину пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Basket_Click(object sender, EventArgs e)
        {
            if (CurrentUser != null)
            {
                // показ корзины и заказов
                Basket basketForm = new Basket(CurrentUser, this);
                basketForm.Show();
            }

        }

        /// <summary>
        /// Просмотр списка клиентов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientsButton_Click(object sender, EventArgs e)
        {
            AllOrders allOrderForms = new AllOrders(this);
            allOrderForms.Show();
        }


        /// <summary>
        /// Открытие склада (в версии для 10 пиргрейда).
        /// </summary>
        /// <param name="currentName"></param>
        internal void DeserializeStorage(string currentName)
        {
            if (string.IsNullOrEmpty(LastSaveName))
                return;

            string directory = $"..\\..\\..\\SavedStorages\\{currentName}";
            string filename1 = $"{directory}\\Storage.bin";
            string filename2 = $"{directory}\\TreeView.bin";

            try
            {
                using (FileStream stream = new FileStream(filename1, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    Storage = (Storage)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                }

                using (FileStream stream = new FileStream(filename2, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    treeView1.Nodes.Clear();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    var nodes = (List<TreeNode>)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                    treeView1.Nodes.AddRange(nodes.ToArray());
                    treeView1.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки склада:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Сбросить статусы кнопок.
            SetEnableStatusToControl(true);

        }

        /// <summary>
        /// Сохранения состояния склада (в версии для 10 пиргрейда)
        /// </summary>
        /// <param name="currentName"></param>
        public void SerializeStorage(string currentName)
        {
            try
            {
                string directory = $"..\\..\\..\\SavedStorages\\{currentName}";
                Directory.CreateDirectory(directory);

                string filename1 = $"{directory}\\Storage.bin";
                string filename2 = $"{directory}\\TreeView.bin";

                // Storage.
                using (FileStream stream = new FileStream(filename1, FileMode.Truncate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    formatter.Serialize(stream, this.Storage);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                }
                // TreeView.
                using (FileStream stream = new FileStream(filename2, FileMode.Truncate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    formatter.Serialize(stream, this.treeView1.Nodes.Cast<TreeNode>().ToList());
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранение склада при закрытии формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializeStorage("Cars");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
