
namespace Storage
{
    partial class ChangeStatusOfOrder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExecutedStatus = new System.Windows.Forms.RadioButton();
            this.UncoiledStatus = new System.Windows.Forms.RadioButton();
            this.ProcessedStatus = new System.Windows.Forms.RadioButton();
            this.KeepStay = new System.Windows.Forms.RadioButton();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.KeepStay);
            this.groupBox1.Controls.Add(this.ExecutedStatus);
            this.groupBox1.Controls.Add(this.UncoiledStatus);
            this.groupBox1.Controls.Add(this.ProcessedStatus);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статус заказа";
            // 
            // ExecutedStatus
            // 
            this.ExecutedStatus.AutoSize = true;
            this.ExecutedStatus.Location = new System.Drawing.Point(12, 76);
            this.ExecutedStatus.Name = "ExecutedStatus";
            this.ExecutedStatus.Size = new System.Drawing.Size(81, 19);
            this.ExecutedStatus.TabIndex = 2;
            this.ExecutedStatus.TabStop = true;
            this.ExecutedStatus.Text = "Исполнен";
            this.ExecutedStatus.UseVisualStyleBackColor = true;
            this.ExecutedStatus.CheckedChanged += new System.EventHandler(this.KeepStay_CheckedChanged);
            // 
            // UncoiledStatus
            // 
            this.UncoiledStatus.AutoSize = true;
            this.UncoiledStatus.Location = new System.Drawing.Point(12, 51);
            this.UncoiledStatus.Name = "UncoiledStatus";
            this.UncoiledStatus.Size = new System.Drawing.Size(79, 19);
            this.UncoiledStatus.TabIndex = 1;
            this.UncoiledStatus.TabStop = true;
            this.UncoiledStatus.Text = "Отгружен";
            this.UncoiledStatus.UseVisualStyleBackColor = true;
            this.UncoiledStatus.CheckedChanged += new System.EventHandler(this.KeepStay_CheckedChanged);
            // 
            // ProcessedStatus
            // 
            this.ProcessedStatus.AutoSize = true;
            this.ProcessedStatus.Location = new System.Drawing.Point(12, 26);
            this.ProcessedStatus.Name = "ProcessedStatus";
            this.ProcessedStatus.Size = new System.Drawing.Size(86, 19);
            this.ProcessedStatus.TabIndex = 0;
            this.ProcessedStatus.TabStop = true;
            this.ProcessedStatus.Text = "Обработан";
            this.ProcessedStatus.UseVisualStyleBackColor = true;
            this.ProcessedStatus.CheckedChanged += new System.EventHandler(this.KeepStay_CheckedChanged);
            // 
            // KeepStay
            // 
            this.KeepStay.AutoSize = true;
            this.KeepStay.Location = new System.Drawing.Point(12, 102);
            this.KeepStay.Name = "KeepStay";
            this.KeepStay.Size = new System.Drawing.Size(94, 19);
            this.KeepStay.TabIndex = 3;
            this.KeepStay.TabStop = true;
            this.KeepStay.Text = "Не изменять";
            this.KeepStay.UseVisualStyleBackColor = true;
            this.KeepStay.CheckedChanged += new System.EventHandler(this.KeepStay_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(161, 152);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ChangeStatusOfOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 189);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChangeStatusOfOrder";
            this.Text = "ChangeStatusOfOrder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ProcessedStatus;
        private System.Windows.Forms.RadioButton ExecutedStatus;
        private System.Windows.Forms.RadioButton UncoiledStatus;
        private System.Windows.Forms.RadioButton KeepStay;
        private System.Windows.Forms.Button OKButton;
    }
}