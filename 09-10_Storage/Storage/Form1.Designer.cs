
namespace Storage
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddParentNode = new System.Windows.Forms.ToolStripMenuItem();
            this.AddChildNode = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveSection = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameNode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveStorageButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MakeCSV = new System.Windows.Forms.ToolStripButton();
            this.SignIn = new System.Windows.Forms.ToolStripButton();
            this.Basket = new System.Windows.Forms.ToolStripButton();
            this.ClientsButton = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.NameProduct = new System.Windows.Forms.ColumnHeader();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.Amount = new System.Windows.Forms.ColumnHeader();
            this.Price = new System.Windows.Forms.ColumnHeader();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.116505F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.77745F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.10604F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1183, 616);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1183, 31);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 27);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.Deserialize);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.Serialize);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 27);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 69);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(348, 544);
            this.treeView1.TabIndex = 2;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddParentNode,
            this.AddChildNode,
            this.AddProduct,
            this.RemoveSection,
            this.RenameNode});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 114);
            // 
            // AddParentNode
            // 
            this.AddParentNode.Name = "AddParentNode";
            this.AddParentNode.Size = new System.Drawing.Size(163, 22);
            this.AddParentNode.Text = "Add parent node";
            this.AddParentNode.Click += new System.EventHandler(this.AddParentNode_Click);
            // 
            // AddChildNode
            // 
            this.AddChildNode.Name = "AddChildNode";
            this.AddChildNode.Size = new System.Drawing.Size(163, 22);
            this.AddChildNode.Text = "Add child node";
            this.AddChildNode.Click += new System.EventHandler(this.AddChildNode_Click);
            // 
            // AddProduct
            // 
            this.AddProduct.Name = "AddProduct";
            this.AddProduct.ShowShortcutKeys = false;
            this.AddProduct.Size = new System.Drawing.Size(163, 22);
            this.AddProduct.Text = "Add product";
            this.AddProduct.Click += new System.EventHandler(this.AddProduct_Click);
            // 
            // RemoveSection
            // 
            this.RemoveSection.Name = "RemoveSection";
            this.RemoveSection.Size = new System.Drawing.Size(163, 22);
            this.RemoveSection.Text = "Remove node";
            this.RemoveSection.Click += new System.EventHandler(this.RemoveSection_Click);
            // 
            // RenameNode
            // 
            this.RenameNode.Name = "RenameNode";
            this.RenameNode.Size = new System.Drawing.Size(163, 22);
            this.RenameNode.Text = "Rename Node";
            this.RenameNode.Click += new System.EventHandler(this.RenameNode_Click);
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 7, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveStorageButton,
            this.toolStripSeparator2,
            this.MakeCSV,
            this.SignIn,
            this.Basket,
            this.ClientsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1183, 35);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveStorageButton
            // 
            this.SaveStorageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveStorageButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveStorageButton.Image")));
            this.SaveStorageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveStorageButton.Name = "SaveStorageButton";
            this.SaveStorageButton.Size = new System.Drawing.Size(23, 32);
            this.SaveStorageButton.Text = "Save";
            this.SaveStorageButton.Click += new System.EventHandler(this.Serialize);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // MakeCSV
            // 
            this.MakeCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MakeCSV.Image = ((System.Drawing.Image)(resources.GetObject("MakeCSV.Image")));
            this.MakeCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MakeCSV.Name = "MakeCSV";
            this.MakeCSV.Size = new System.Drawing.Size(23, 32);
            this.MakeCSV.Text = "Make CSV";
            this.MakeCSV.Click += new System.EventHandler(this.MakeCSV_Click);
            // 
            // SignIn
            // 
            this.SignIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SignIn.Image = ((System.Drawing.Image)(resources.GetObject("SignIn.Image")));
            this.SignIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(111, 32);
            this.SignIn.Text = "Вход/Регистрация";
            this.SignIn.ToolTipText = "Sign In";
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // Basket
            // 
            this.Basket.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Basket.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Basket.Name = "Basket";
            this.Basket.Size = new System.Drawing.Size(57, 32);
            this.Basket.Text = "Корзина";
            this.Basket.Click += new System.EventHandler(this.Basket_Click);
            // 
            // ClientsButton
            // 
            this.ClientsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClientsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClientsButton.Image")));
            this.ClientsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClientsButton.Name = "ClientsButton";
            this.ClientsButton.Size = new System.Drawing.Size(59, 32);
            this.ClientsButton.Text = "Клиенты";
            this.ClientsButton.Click += new System.EventHandler(this.ClientsButton_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameProduct,
            this.ID,
            this.Amount,
            this.Price});
            this.listView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(357, 69);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(823, 544);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // NameProduct
            // 
            this.NameProduct.Text = "Наименование товара";
            this.NameProduct.Width = 250;
            // 
            // ID
            // 
            this.ID.Text = "Артикул";
            this.ID.Width = 120;
            // 
            // Amount
            // 
            this.Amount.Text = "Количество";
            this.Amount.Width = 90;
            // 
            // Price
            // 
            this.Price.Text = "Цена";
            this.Price.Width = 90;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 616);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddParentNode;
        private System.Windows.Forms.ToolStripMenuItem AddChildNode;
        private System.Windows.Forms.ToolStripMenuItem AddProduct;
        private System.Windows.Forms.ToolStripMenuItem RemoveSection;
        private System.Windows.Forms.ToolStripMenuItem RenameNode;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NameProduct;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Amount;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.ToolStripMenuItem RemoveProduct;
        private System.Windows.Forms.ToolStripMenuItem RemoveProduct2;
        private System.Windows.Forms.ToolStripButton SaveStorageButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton MakeCSV;
        private System.Windows.Forms.ToolStripButton SignIn;
        private System.Windows.Forms.ToolStripButton Basket;
        private System.Windows.Forms.ToolStripButton ClientsButton;
    }
}

