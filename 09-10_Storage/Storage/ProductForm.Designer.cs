
namespace Storage
{
    partial class ProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Amount = new System.Windows.Forms.Label();
            this.RUB = new System.Windows.Forms.Label();
            this.AmountProduct = new System.Windows.Forms.TextBox();
            this.PriceProduct = new System.Windows.Forms.TextBox();
            this.IDProduct = new System.Windows.Forms.TextBox();
            this.ProductName = new System.Windows.Forms.TextBox();
            this.AmountofProductLabel = new System.Windows.Forms.Label();
            this.PriceofProductLabel = new System.Windows.Forms.Label();
            this.IDofProductLabel = new System.Windows.Forms.Label();
            this.NameofProductLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DescriptiponProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PictureProduct = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Ok = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AddToBusket = new System.Windows.Forms.Button();
            this.AmountToBusket = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmountToBusket)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.76866F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.23135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 258F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.PictureProduct, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.712231F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.28777F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.Amount);
            this.groupBox1.Controls.Add(this.RUB);
            this.groupBox1.Controls.Add(this.AmountProduct);
            this.groupBox1.Controls.Add(this.PriceProduct);
            this.groupBox1.Controls.Add(this.IDProduct);
            this.groupBox1.Controls.Add(this.ProductName);
            this.groupBox1.Controls.Add(this.AmountofProductLabel);
            this.groupBox1.Controls.Add(this.PriceofProductLabel);
            this.groupBox1.Controls.Add(this.IDofProductLabel);
            this.groupBox1.Controls.Add(this.NameofProductLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "  ";
            // 
            // Amount
            // 
            this.Amount.AutoSize = true;
            this.Amount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Amount.Location = new System.Drawing.Point(238, 116);
            this.Amount.Name = "Amount";
            this.Amount.Size = new System.Drawing.Size(26, 15);
            this.Amount.TabIndex = 9;
            this.Amount.Text = "шт.";
            // 
            // RUB
            // 
            this.RUB.AutoSize = true;
            this.RUB.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.RUB.Location = new System.Drawing.Point(238, 86);
            this.RUB.Name = "RUB";
            this.RUB.Size = new System.Drawing.Size(29, 15);
            this.RUB.TabIndex = 8;
            this.RUB.Text = "RUB";
            // 
            // AmountProduct
            // 
            this.AmountProduct.Location = new System.Drawing.Point(127, 113);
            this.AmountProduct.Name = "AmountProduct";
            this.AmountProduct.Size = new System.Drawing.Size(104, 23);
            this.AmountProduct.TabIndex = 7;
            // 
            // PriceProduct
            // 
            this.PriceProduct.Location = new System.Drawing.Point(91, 83);
            this.PriceProduct.Name = "PriceProduct";
            this.PriceProduct.Size = new System.Drawing.Size(140, 23);
            this.PriceProduct.TabIndex = 6;
            // 
            // IDProduct
            // 
            this.IDProduct.Location = new System.Drawing.Point(91, 53);
            this.IDProduct.Name = "IDProduct";
            this.IDProduct.Size = new System.Drawing.Size(176, 23);
            this.IDProduct.TabIndex = 5;
            // 
            // ProductName
            // 
            this.ProductName.Location = new System.Drawing.Point(91, 23);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(441, 23);
            this.ProductName.TabIndex = 4;
            // 
            // AmountofProductLabel
            // 
            this.AmountofProductLabel.AutoSize = true;
            this.AmountofProductLabel.Location = new System.Drawing.Point(10, 116);
            this.AmountofProductLabel.Name = "AmountofProductLabel";
            this.AmountofProductLabel.Size = new System.Drawing.Size(110, 15);
            this.AmountofProductLabel.TabIndex = 3;
            this.AmountofProductLabel.Text = "Остаток на складе:";
            // 
            // PriceofProductLabel
            // 
            this.PriceofProductLabel.AutoSize = true;
            this.PriceofProductLabel.Location = new System.Drawing.Point(10, 86);
            this.PriceofProductLabel.Name = "PriceofProductLabel";
            this.PriceofProductLabel.Size = new System.Drawing.Size(71, 15);
            this.PriceofProductLabel.TabIndex = 2;
            this.PriceofProductLabel.Text = "Цена (RUB):";
            // 
            // IDofProductLabel
            // 
            this.IDofProductLabel.AutoSize = true;
            this.IDofProductLabel.Location = new System.Drawing.Point(10, 56);
            this.IDofProductLabel.Name = "IDofProductLabel";
            this.IDofProductLabel.Size = new System.Drawing.Size(56, 15);
            this.IDofProductLabel.TabIndex = 1;
            this.IDofProductLabel.Text = "Артикул:";
            // 
            // NameofProductLabel
            // 
            this.NameofProductLabel.AutoSize = true;
            this.NameofProductLabel.Location = new System.Drawing.Point(10, 26);
            this.NameofProductLabel.Name = "NameofProductLabel";
            this.NameofProductLabel.Size = new System.Drawing.Size(74, 15);
            this.NameofProductLabel.TabIndex = 0;
            this.NameofProductLabel.Text = "Имя товара:";
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.DescriptiponProduct);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(538, 132);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Описание";
            // 
            // DescriptiponProduct
            // 
            this.DescriptiponProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptiponProduct.Location = new System.Drawing.Point(3, 19);
            this.DescriptiponProduct.Multiline = true;
            this.DescriptiponProduct.Name = "DescriptiponProduct";
            this.DescriptiponProduct.Size = new System.Drawing.Size(532, 110);
            this.DescriptiponProduct.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(547, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Фотография:";
            // 
            // PictureProduct
            // 
            this.PictureProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureProduct.Location = new System.Drawing.Point(547, 30);
            this.PictureProduct.Name = "PictureProduct";
            this.PictureProduct.Size = new System.Drawing.Size(253, 245);
            this.PictureProduct.TabIndex = 4;
            this.PictureProduct.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(547, 419);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Ok);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Apply);
            this.splitContainer1.Size = new System.Drawing.Size(253, 28);
            this.splitContainer1.SplitterDistance = 83;
            this.splitContainer1.TabIndex = 5;
            // 
            // Ok
            // 
            this.Ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ok.Location = new System.Drawing.Point(0, 0);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(83, 28);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Apply
            // 
            this.Apply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Apply.Location = new System.Drawing.Point(0, 0);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(166, 28);
            this.Apply.TabIndex = 0;
            this.Apply.Text = "Применить";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AddToBusket);
            this.groupBox3.Controls.Add(this.AmountToBusket);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(547, 281);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 132);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Добавить в корзину";
            // 
            // AddToBusket
            // 
            this.AddToBusket.Location = new System.Drawing.Point(134, 23);
            this.AddToBusket.Name = "AddToBusket";
            this.AddToBusket.Size = new System.Drawing.Size(109, 23);
            this.AddToBusket.TabIndex = 1;
            this.AddToBusket.Text = "Добавить";
            this.AddToBusket.UseVisualStyleBackColor = true;
            this.AddToBusket.Click += new System.EventHandler(this.AddToBusket_Click);
            // 
            // AmountToBusket
            // 
            this.AmountToBusket.Location = new System.Drawing.Point(7, 23);
            this.AmountToBusket.Name = "AmountToBusket";
            this.AmountToBusket.Size = new System.Drawing.Size(120, 23);
            this.AmountToBusket.TabIndex = 0;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProductForm";
            this.Text = "Спецификация товара";
            this.Load += new System.EventHandler(this.ProductForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureProduct)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AmountToBusket)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label PriceofProductLabel;
        private System.Windows.Forms.Label IDofProductLabel;
        private System.Windows.Forms.Label NameofProductLabel;
        private System.Windows.Forms.TextBox ProductName;
        private System.Windows.Forms.Label AmountofProductLabel;
        private System.Windows.Forms.Label Amount;
        private System.Windows.Forms.Label RUB;
        private System.Windows.Forms.TextBox AmountProduct;
        private System.Windows.Forms.TextBox PriceProduct;
        private System.Windows.Forms.TextBox IDProduct;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox DescriptiponProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PictureProduct;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown AmountToBusket;
        private System.Windows.Forms.Button AddToBusket;
    }
}