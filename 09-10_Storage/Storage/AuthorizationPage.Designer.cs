
namespace Storage
{
    partial class AuthorizationPage
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SignUp = new System.Windows.Forms.LinkLabel();
            this.SignIn = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SwitchClient = new System.Windows.Forms.RadioButton();
            this.ModeBox = new System.Windows.Forms.GroupBox();
            this.SwitchSupplier = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.ModeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(410, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 115);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(410, 23);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // SignUp
            // 
            this.SignUp.AutoSize = true;
            this.SignUp.Location = new System.Drawing.Point(462, 102);
            this.SignUp.Name = "SignUp";
            this.SignUp.Size = new System.Drawing.Size(160, 15);
            this.SignUp.TabIndex = 4;
            this.SignUp.TabStop = true;
            this.SignUp.Text = "Еще не зарегистировались?";
            this.SignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SignUp_LinkClicked);
            // 
            // SignIn
            // 
            this.SignIn.Location = new System.Drawing.Point(194, 308);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(410, 24);
            this.SignIn.TabIndex = 5;
            this.SignIn.Text = "Войти";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(176, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 182);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вход";
            // 
            // SwitchClient
            // 
            this.SwitchClient.AutoSize = true;
            this.SwitchClient.Checked = true;
            this.SwitchClient.Location = new System.Drawing.Point(283, 22);
            this.SwitchClient.Name = "SwitchClient";
            this.SwitchClient.Size = new System.Drawing.Size(64, 19);
            this.SwitchClient.TabIndex = 7;
            this.SwitchClient.TabStop = true;
            this.SwitchClient.Text = "Клиент";
            this.SwitchClient.UseVisualStyleBackColor = true;
            this.SwitchClient.CheckedChanged += new System.EventHandler(this.SwitchSupplier_CheckedChanged);
            // 
            // ModeBox
            // 
            this.ModeBox.Controls.Add(this.SwitchSupplier);
            this.ModeBox.Controls.Add(this.SwitchClient);
            this.ModeBox.Location = new System.Drawing.Point(12, 12);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.Size = new System.Drawing.Size(776, 58);
            this.ModeBox.TabIndex = 8;
            this.ModeBox.TabStop = false;
            this.ModeBox.Text = "Режим";
            // 
            // SwitchSupplier
            // 
            this.SwitchSupplier.AutoSize = true;
            this.SwitchSupplier.Location = new System.Drawing.Point(410, 22);
            this.SwitchSupplier.Name = "SwitchSupplier";
            this.SwitchSupplier.Size = new System.Drawing.Size(79, 19);
            this.SwitchSupplier.TabIndex = 8;
            this.SwitchSupplier.Text = "Продавец";
            this.SwitchSupplier.UseVisualStyleBackColor = true;
            this.SwitchSupplier.CheckedChanged += new System.EventHandler(this.SwitchSupplier_CheckedChanged);
            // 
            // AuthorizationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SignIn);
            this.Controls.Add(this.SignUp);
            this.Name = "AuthorizationPage";
            this.Text = "AuthorizationPage";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ModeBox.ResumeLayout(false);
            this.ModeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel SignUp;
        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SwitchClient;
        private System.Windows.Forms.GroupBox ModeBox;
        private System.Windows.Forms.RadioButton SwitchSupplier;
    }
}