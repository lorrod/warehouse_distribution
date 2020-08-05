namespace test_Task
{
    partial class MainWindow
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
            this.picket_field = new System.Windows.Forms.Panel();
            this.control_panel = new System.Windows.Forms.Panel();
            this.delete_picket = new System.Windows.Forms.Button();
            this.search_name = new System.Windows.Forms.TextBox();
            this.create_area = new System.Windows.Forms.Button();
            this.create_picket = new System.Windows.Forms.Button();
            this.picket_info_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.history = new System.Windows.Forms.Button();
            this.search_load = new System.Windows.Forms.Button();
            this.set_weight = new System.Windows.Forms.Button();
            this.devide_area = new System.Windows.Forms.Button();
            this.storage_select = new System.Windows.Forms.Panel();
            this.delete_history = new System.Windows.Forms.Label();
            this.previous = new System.Windows.Forms.Label();
            this.next_storage = new System.Windows.Forms.Label();
            this.name_store = new System.Windows.Forms.Label();
            this.control_panel.SuspendLayout();
            this.storage_select.SuspendLayout();
            this.SuspendLayout();
            // 
            // picket_field
            // 
            this.picket_field.AutoScroll = true;
            this.picket_field.AutoSize = true;
            this.picket_field.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.picket_field.Location = new System.Drawing.Point(1, 107);
            this.picket_field.Name = "picket_field";
            this.picket_field.Size = new System.Drawing.Size(759, 502);
            this.picket_field.TabIndex = 0;
            // 
            // control_panel
            // 
            this.control_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.control_panel.AutoSize = true;
            this.control_panel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.control_panel.Controls.Add(this.delete_picket);
            this.control_panel.Controls.Add(this.search_name);
            this.control_panel.Controls.Add(this.create_area);
            this.control_panel.Controls.Add(this.create_picket);
            this.control_panel.Controls.Add(this.picket_info_label);
            this.control_panel.Controls.Add(this.label1);
            this.control_panel.Controls.Add(this.history);
            this.control_panel.Controls.Add(this.search_load);
            this.control_panel.Controls.Add(this.set_weight);
            this.control_panel.Controls.Add(this.devide_area);
            this.control_panel.Location = new System.Drawing.Point(759, 0);
            this.control_panel.Name = "control_panel";
            this.control_panel.Size = new System.Drawing.Size(327, 609);
            this.control_panel.TabIndex = 1;
            // 
            // delete_picket
            // 
            this.delete_picket.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.delete_picket.ForeColor = System.Drawing.Color.Maroon;
            this.delete_picket.Location = new System.Drawing.Point(54, 543);
            this.delete_picket.Name = "delete_picket";
            this.delete_picket.Size = new System.Drawing.Size(223, 37);
            this.delete_picket.TabIndex = 9;
            this.delete_picket.Text = "Удалить пикет";
            this.delete_picket.UseVisualStyleBackColor = true;
            this.delete_picket.Click += new System.EventHandler(this.delete_picket_Click);
            // 
            // search_name
            // 
            this.search_name.BackColor = System.Drawing.Color.WhiteSmoke;
            this.search_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.search_name.Location = new System.Drawing.Point(54, 320);
            this.search_name.Multiline = true;
            this.search_name.Name = "search_name";
            this.search_name.Size = new System.Drawing.Size(152, 38);
            this.search_name.TabIndex = 8;
            this.search_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // create_area
            // 
            this.create_area.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.create_area.Location = new System.Drawing.Point(54, 409);
            this.create_area.Name = "create_area";
            this.create_area.Size = new System.Drawing.Size(223, 39);
            this.create_area.TabIndex = 7;
            this.create_area.Text = "Создать площадку";
            this.create_area.UseVisualStyleBackColor = true;
            this.create_area.Click += new System.EventHandler(this.create_area_Click);
            // 
            // create_picket
            // 
            this.create_picket.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.create_picket.Location = new System.Drawing.Point(54, 500);
            this.create_picket.Name = "create_picket";
            this.create_picket.Size = new System.Drawing.Size(223, 37);
            this.create_picket.TabIndex = 6;
            this.create_picket.Text = "Создать пикет";
            this.create_picket.UseVisualStyleBackColor = true;
            this.create_picket.Click += new System.EventHandler(this.create_picket_Click);
            // 
            // picket_info_label
            // 
            this.picket_info_label.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picket_info_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.picket_info_label.Location = new System.Drawing.Point(34, 42);
            this.picket_info_label.Name = "picket_info_label";
            this.picket_info_label.Size = new System.Drawing.Size(271, 213);
            this.picket_info_label.TabIndex = 5;
            this.picket_info_label.Text = "Пример инф о пикете";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Информация о пикете";
            // 
            // history
            // 
            this.history.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.history.Location = new System.Drawing.Point(54, 274);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(223, 39);
            this.history.TabIndex = 3;
            this.history.Text = "История";
            this.history.UseVisualStyleBackColor = true;
            this.history.Click += new System.EventHandler(this.history_Click);
            // 
            // search_load
            // 
            this.search_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.search_load.Location = new System.Drawing.Point(212, 319);
            this.search_load.Name = "search_load";
            this.search_load.Size = new System.Drawing.Size(65, 39);
            this.search_load.TabIndex = 2;
            this.search_load.Text = "Поиск";
            this.search_load.UseVisualStyleBackColor = true;
            this.search_load.Click += new System.EventHandler(this.search_load_Click);
            // 
            // set_weight
            // 
            this.set_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.set_weight.Location = new System.Drawing.Point(54, 364);
            this.set_weight.Name = "set_weight";
            this.set_weight.Size = new System.Drawing.Size(223, 39);
            this.set_weight.TabIndex = 1;
            this.set_weight.Text = "Изменение груза";
            this.set_weight.UseVisualStyleBackColor = true;
            this.set_weight.Click += new System.EventHandler(this.set_weight_Click);
            // 
            // devide_area
            // 
            this.devide_area.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.devide_area.Location = new System.Drawing.Point(54, 454);
            this.devide_area.Name = "devide_area";
            this.devide_area.Size = new System.Drawing.Size(223, 39);
            this.devide_area.TabIndex = 0;
            this.devide_area.Text = "Разделить площадку";
            this.devide_area.UseVisualStyleBackColor = true;
            this.devide_area.Click += new System.EventHandler(this.create_devide_area_Click);
            // 
            // storage_select
            // 
            this.storage_select.AutoSize = true;
            this.storage_select.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.storage_select.Controls.Add(this.delete_history);
            this.storage_select.Controls.Add(this.previous);
            this.storage_select.Controls.Add(this.next_storage);
            this.storage_select.Controls.Add(this.name_store);
            this.storage_select.Location = new System.Drawing.Point(1, 0);
            this.storage_select.Name = "storage_select";
            this.storage_select.Size = new System.Drawing.Size(762, 110);
            this.storage_select.TabIndex = 2;
            // 
            // delete_history
            // 
            this.delete_history.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_history.ForeColor = System.Drawing.Color.Maroon;
            this.delete_history.Location = new System.Drawing.Point(0, 94);
            this.delete_history.Name = "delete_history";
            this.delete_history.Size = new System.Drawing.Size(759, 16);
            this.delete_history.TabIndex = 3;
            this.delete_history.Text = "Очистить историю";
            this.delete_history.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.delete_history.Click += new System.EventHandler(this.delete_history_Click);
            // 
            // previous
            // 
            this.previous.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.previous.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.previous.Location = new System.Drawing.Point(3, 0);
            this.previous.Name = "previous";
            this.previous.Size = new System.Drawing.Size(292, 110);
            this.previous.TabIndex = 2;
            this.previous.Text = "Предыдущий";
            this.previous.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.previous.Click += new System.EventHandler(this.previous_Click);
            // 
            // next_storage
            // 
            this.next_storage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.next_storage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.next_storage.Location = new System.Drawing.Point(495, 0);
            this.next_storage.Name = "next_storage";
            this.next_storage.Size = new System.Drawing.Size(260, 110);
            this.next_storage.TabIndex = 1;
            this.next_storage.Text = "Следующий";
            this.next_storage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.next_storage.Click += new System.EventHandler(this.next_storage_Click);
            // 
            // name_store
            // 
            this.name_store.Dock = System.Windows.Forms.DockStyle.Fill;
            this.name_store.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_store.Location = new System.Drawing.Point(0, 0);
            this.name_store.Name = "name_store";
            this.name_store.Size = new System.Drawing.Size(762, 110);
            this.name_store.TabIndex = 0;
            this.name_store.Text = "Склад №1";
            this.name_store.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 610);
            this.Controls.Add(this.storage_select);
            this.Controls.Add(this.control_panel);
            this.Controls.Add(this.picket_field);
            this.MinimumSize = new System.Drawing.Size(1108, 649);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.control_panel.ResumeLayout(false);
            this.control_panel.PerformLayout();
            this.storage_select.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel picket_field;
        private System.Windows.Forms.Panel control_panel;
        private System.Windows.Forms.Panel storage_select;
        private System.Windows.Forms.Label previous;
        private System.Windows.Forms.Label next_storage;
        private System.Windows.Forms.Label name_store;
        private System.Windows.Forms.Label picket_info_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button history;
        private System.Windows.Forms.Button search_load;
        private System.Windows.Forms.Button set_weight;
        private System.Windows.Forms.Button devide_area;
        private System.Windows.Forms.Button create_picket;
        private System.Windows.Forms.Button create_area;
        private System.Windows.Forms.TextBox search_name;
        private System.Windows.Forms.Button delete_picket;
        private System.Windows.Forms.Label delete_history;
    }
}

