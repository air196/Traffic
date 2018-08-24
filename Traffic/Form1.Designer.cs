namespace Traffic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.from_bus_stop_comboBox = new System.Windows.Forms.ComboBox();
            this.to_bus_stop_comboBox = new System.Windows.Forms.ComboBox();
            this.load_button = new System.Windows.Forms.Button();
            this.hours_comboBox = new System.Windows.Forms.ComboBox();
            this.minutes_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.build_route_button = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_swap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // from_bus_stop_comboBox
            // 
            this.from_bus_stop_comboBox.FormattingEnabled = true;
            this.from_bus_stop_comboBox.Location = new System.Drawing.Point(129, 75);
            this.from_bus_stop_comboBox.Name = "from_bus_stop_comboBox";
            this.from_bus_stop_comboBox.Size = new System.Drawing.Size(100, 21);
            this.from_bus_stop_comboBox.TabIndex = 2;
            // 
            // to_bus_stop_comboBox
            // 
            this.to_bus_stop_comboBox.FormattingEnabled = true;
            this.to_bus_stop_comboBox.Location = new System.Drawing.Point(315, 75);
            this.to_bus_stop_comboBox.Name = "to_bus_stop_comboBox";
            this.to_bus_stop_comboBox.Size = new System.Drawing.Size(100, 21);
            this.to_bus_stop_comboBox.TabIndex = 3;
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(18, 12);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(397, 47);
            this.load_button.TabIndex = 1;
            this.load_button.Text = "Загрузить файл";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // hours_comboBox
            // 
            this.hours_comboBox.FormattingEnabled = true;
            this.hours_comboBox.Location = new System.Drawing.Point(129, 102);
            this.hours_comboBox.Name = "hours_comboBox";
            this.hours_comboBox.Size = new System.Drawing.Size(135, 21);
            this.hours_comboBox.TabIndex = 4;
            // 
            // minutes_comboBox
            // 
            this.minutes_comboBox.FormattingEnabled = true;
            this.minutes_comboBox.Location = new System.Drawing.Point(280, 102);
            this.minutes_comboBox.Name = "minutes_comboBox";
            this.minutes_comboBox.Size = new System.Drawing.Size(135, 21);
            this.minutes_comboBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номера остановок";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Время отправления";
            // 
            // build_route_button
            // 
            this.build_route_button.Enabled = false;
            this.build_route_button.Location = new System.Drawing.Point(18, 142);
            this.build_route_button.Name = "build_route_button";
            this.build_route_button.Size = new System.Drawing.Size(397, 47);
            this.build_route_button.TabIndex = 6;
            this.build_route_button.Text = "Построить маршрут";
            this.build_route_button.UseVisualStyleBackColor = true;
            this.build_route_button.Click += new System.EventHandler(this.build_route_button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(18, 208);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(397, 196);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // button_swap
            // 
            this.button_swap.Location = new System.Drawing.Point(236, 75);
            this.button_swap.Name = "button_swap";
            this.button_swap.Size = new System.Drawing.Size(73, 21);
            this.button_swap.TabIndex = 8;
            this.button_swap.Text = "<=>";
            this.button_swap.UseVisualStyleBackColor = true;
            this.button_swap.Click += new System.EventHandler(this.button_swap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 416);
            this.Controls.Add(this.button_swap);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minutes_comboBox);
            this.Controls.Add(this.hours_comboBox);
            this.Controls.Add(this.build_route_button);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.to_bus_stop_comboBox);
            this.Controls.Add(this.from_bus_stop_comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Построение маршрута";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox from_bus_stop_comboBox;
        private System.Windows.Forms.ComboBox to_bus_stop_comboBox;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.ComboBox hours_comboBox;
        private System.Windows.Forms.ComboBox minutes_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button build_route_button;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_swap;
    }
}

