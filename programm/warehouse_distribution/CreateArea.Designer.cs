namespace test_Task
{
    partial class CreateArea
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.room_num = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.notify = new System.Windows.Forms.Label();
            this.create_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.range_field = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.room_num);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.notify);
            this.panel1.Controls.Add(this.create_button);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.range_field);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 296);
            this.panel1.TabIndex = 0;
            // 
            // room_num
            // 
            this.room_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.room_num.Location = new System.Drawing.Point(64, 81);
            this.room_num.Multiline = true;
            this.room_num.Name = "room_num";
            this.room_num.Size = new System.Drawing.Size(98, 33);
            this.room_num.TabIndex = 6;
            this.room_num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(60, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "На складе:";
            // 
            // notify
            // 
            this.notify.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.notify.ForeColor = System.Drawing.Color.Red;
            this.notify.Location = new System.Drawing.Point(65, 180);
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(244, 51);
            this.notify.TabIndex = 4;
            this.notify.Text = "Ошибка: в диапазоне площадке находится несуществующий пикет";
            // 
            // create_button
            // 
            this.create_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.create_button.Location = new System.Drawing.Point(65, 234);
            this.create_button.Name = "create_button";
            this.create_button.Size = new System.Drawing.Size(244, 33);
            this.create_button.TabIndex = 3;
            this.create_button.Text = "Создать";
            this.create_button.UseVisualStyleBackColor = true;
            this.create_button.Click += new System.EventHandler(this.create_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(60, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Введите диапазон пикетов:";
            // 
            // range_field
            // 
            this.range_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.range_field.Location = new System.Drawing.Point(63, 140);
            this.range_field.Multiline = true;
            this.range_field.Name = "range_field";
            this.range_field.Size = new System.Drawing.Size(245, 33);
            this.range_field.TabIndex = 1;
            this.range_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.range_field.TextChanged += new System.EventHandler(this.range_field_TextChanged);
            this.range_field.Enter += new System.EventHandler(this.range_field_Enter);
            this.range_field.Leave += new System.EventHandler(this.range_field_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label1.Location = new System.Drawing.Point(74, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Создать площадку";
            // 
            // CreateArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 302);
            this.Controls.Add(this.panel1);
            this.Name = "CreateArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateArea";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label notify;
        private System.Windows.Forms.Button create_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox range_field;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox room_num;
        private System.Windows.Forms.Label label3;
    }
}