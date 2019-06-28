namespace Calendar
{
    partial class Calendar
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.button_1_back = new System.Windows.Forms.Button();
            this.button_2_next = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(241, 14);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(333, 21);
            this.comboBox.TabIndex = 0;
            // 
            // button_1_back
            // 
            this.button_1_back.Location = new System.Drawing.Point(82, 14);
            this.button_1_back.Name = "button_1_back";
            this.button_1_back.Size = new System.Drawing.Size(75, 23);
            this.button_1_back.TabIndex = 1;
            this.button_1_back.Text = "back";
            this.button_1_back.UseVisualStyleBackColor = true;
            // 
            // button_2_next
            // 
            this.button_2_next.Location = new System.Drawing.Point(642, 14);
            this.button_2_next.Name = "button_2_next";
            this.button_2_next.Size = new System.Drawing.Size(75, 23);
            this.button_2_next.TabIndex = 2;
            this.button_2_next.Text = "next";
            this.button_2_next.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(13, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 334);
            this.panel1.TabIndex = 3;
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(798, 404);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_2_next);
            this.Controls.Add(this.button_1_back);
            this.Controls.Add(this.comboBox);
            this.Name = "Calendar";
            this.Text = "Calendar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Close_db);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button button_1_back;
        private System.Windows.Forms.Button button_2_next;
        private System.Windows.Forms.Panel panel1;
    }
}

