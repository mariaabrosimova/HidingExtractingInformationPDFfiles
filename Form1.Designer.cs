namespace CURS_PDF_Encoder
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Search_pdf = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.Button();
            this.TB1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Pas1 = new System.Windows.Forms.TextBox();
            this.Pas2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Decrypt = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.GB = new System.Windows.Forms.GroupBox();
            this.RB2 = new System.Windows.Forms.RadioButton();
            this.RB1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.P3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1560, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Search_pdf
            // 
            this.Search_pdf.Location = new System.Drawing.Point(52, 81);
            this.Search_pdf.Name = "Search_pdf";
            this.Search_pdf.Size = new System.Drawing.Size(201, 63);
            this.Search_pdf.TabIndex = 1;
            this.Search_pdf.Text = "Выбрать PDF-файл";
            this.Search_pdf.UseVisualStyleBackColor = true;
            this.Search_pdf.Click += new System.EventHandler(this.Search_pdf_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Location = new System.Drawing.Point(971, 217);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(201, 63);
            this.Encrypt.TabIndex = 2;
            this.Encrypt.Text = "Скрыть сообщение";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // TB1
            // 
            this.TB1.Location = new System.Drawing.Point(259, 269);
            this.TB1.Name = "TB1";
            this.TB1.Size = new System.Drawing.Size(265, 26);
            this.TB1.TabIndex = 5;
            this.TB1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Выбрать данные для скрытия";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(567, 176);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(208, 24);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Зашифровать данные";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Pas1
            // 
            this.Pas1.Location = new System.Drawing.Point(763, 213);
            this.Pas1.Name = "Pas1";
            this.Pas1.PasswordChar = '*';
            this.Pas1.Size = new System.Drawing.Size(185, 26);
            this.Pas1.TabIndex = 8;
            // 
            // Pas2
            // 
            this.Pas2.Location = new System.Drawing.Point(763, 273);
            this.Pas2.Name = "Pas2";
            this.Pas2.PasswordChar = '*';
            this.Pas2.Size = new System.Drawing.Size(185, 26);
            this.Pas2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(563, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(563, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Подтверждение пароля";
            // 
            // Decrypt
            // 
            this.Decrypt.Location = new System.Drawing.Point(971, 444);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(201, 64);
            this.Decrypt.TabIndex = 13;
            this.Decrypt.Text = "Извлечь сообщение";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // GB
            // 
            this.GB.Controls.Add(this.RB2);
            this.GB.Controls.Add(this.RB1);
            this.GB.Location = new System.Drawing.Point(53, 213);
            this.GB.Name = "GB";
            this.GB.Size = new System.Drawing.Size(200, 100);
            this.GB.TabIndex = 14;
            this.GB.TabStop = false;
            this.GB.Text = "Источник сообщения";
            // 
            // RB2
            // 
            this.RB2.AutoSize = true;
            this.RB2.Location = new System.Drawing.Point(7, 57);
            this.RB2.Name = "RB2";
            this.RB2.Size = new System.Drawing.Size(77, 24);
            this.RB2.TabIndex = 1;
            this.RB2.TabStop = true;
            this.RB2.Text = "Текст";
            this.RB2.UseVisualStyleBackColor = true;
            this.RB2.CheckedChanged += new System.EventHandler(this.RB2_CheckedChanged);
            // 
            // RB1
            // 
            this.RB1.AutoSize = true;
            this.RB1.Location = new System.Drawing.Point(7, 26);
            this.RB1.Name = "RB1";
            this.RB1.Size = new System.Drawing.Size(77, 24);
            this.RB1.TabIndex = 0;
            this.RB1.TabStop = true;
            this.RB1.Text = "Файл";
            this.RB1.UseVisualStyleBackColor = true;
            this.RB1.CheckedChanged += new System.EventHandler(this.RB1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(563, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Пароль";
            // 
            // P3
            // 
            this.P3.Location = new System.Drawing.Point(763, 462);
            this.P3.Name = "P3";
            this.P3.PasswordChar = '*';
            this.P3.Size = new System.Drawing.Size(185, 26);
            this.P3.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(48, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(458, 29);
            this.label5.TabIndex = 18;
            this.label5.Text = "Скрытие информации в PDF-файле";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(47, 364);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(520, 29);
            this.label6.TabIndex = 19;
            this.label6.Text = "Извлечение информации из PDF-файла";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1560, 642);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.P3);
            this.Controls.Add(this.GB);
            this.Controls.Add(this.Decrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Pas2);
            this.Controls.Add(this.Pas1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB1);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.Search_pdf);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Скрытие и извлечение информации";
            this.GB.ResumeLayout(false);
            this.GB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button Search_pdf;
        private System.Windows.Forms.Button Encrypt;
        private System.Windows.Forms.TextBox TB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox Pas1;
        private System.Windows.Forms.TextBox Pas2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Decrypt;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox GB;
        private System.Windows.Forms.RadioButton RB2;
        private System.Windows.Forms.RadioButton RB1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox P3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

