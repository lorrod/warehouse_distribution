namespace test_Task
{
    partial class ChangeGoods
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
            this.room_num = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.picket_num = new System.Windows.Forms.TextBox();
            this.notify = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.goods_volume = new System.Windows.Forms.TextBox();
            this.add_button = new System.Windows.Forms.Button();
            this.goods_weight = new System.Windows.Forms.TextBox();
            this.goods_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.room_num);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.picket_num);
            this.panel1.Controls.Add(this.notify);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.goods_volume);
            this.panel1.Controls.Add(this.add_button);
            this.panel1.Controls.Add(this.goods_weight);
            this.panel1.Controls.Add(this.goods_name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 426);
            this.panel1.TabIndex = 0;
            // 
            // room_num
            // 
            this.room_num.AutoSize = true;
            this.room_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.room_num.Location = new System.Drawing.Point(150, 22);
            this.room_num.Name = "room_num";
            this.room_num.Size = new System.Drawing.Size(27, 29);
            this.room_num.TabIndex = 44;
            this.room_num.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label4.Location = new System.Drawing.Point(25, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 29);
            this.label4.TabIndex = 43;
            this.label4.Text = "Склад №";
            // 
            // picket_num
            // 
            this.picket_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.picket_num.Location = new System.Drawing.Point(400, 65);
            this.picket_num.Multiline = true;
            this.picket_num.Name = "picket_num";
            this.picket_num.Size = new System.Drawing.Size(68, 39);
            this.picket_num.TabIndex = 42;
            this.picket_num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.picket_num.TextChanged += new System.EventHandler(this.picket_num_TextChanged);
            // 
            // notify
            // 
            this.notify.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.notify.ForeColor = System.Drawing.Color.Red;
            this.notify.Location = new System.Drawing.Point(74, 269);
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(320, 65);
            this.notify.TabIndex = 41;
            this.notify.Text = "Ошибка: в диапазоне площадке находится несуществующий пикет";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label12.Location = new System.Drawing.Point(21, 219);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(165, 29);
            this.label12.TabIndex = 40;
            this.label12.Text = "Объем груза";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label11.Location = new System.Drawing.Point(21, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 29);
            this.label11.TabIndex = 39;
            this.label11.Text = "Вес груза";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label10.Location = new System.Drawing.Point(21, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 29);
            this.label10.TabIndex = 38;
            this.label10.Text = "Тип груза";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label3.Location = new System.Drawing.Point(425, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 29);
            this.label3.TabIndex = 31;
            this.label3.Text = "кг.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label2.Location = new System.Drawing.Point(425, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 29);
            this.label2.TabIndex = 30;
            this.label2.Text = "м^3";
            // 
            // goods_volume
            // 
            this.goods_volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.goods_volume.Location = new System.Drawing.Point(199, 216);
            this.goods_volume.Multiline = true;
            this.goods_volume.Name = "goods_volume";
            this.goods_volume.Size = new System.Drawing.Size(220, 39);
            this.goods_volume.TabIndex = 29;
            this.goods_volume.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.goods_volume.TextChanged += new System.EventHandler(this.goods_volume_TextChanged);
            // 
            // add_button
            // 
            this.add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.add_button.Location = new System.Drawing.Point(77, 337);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(317, 41);
            this.add_button.TabIndex = 28;
            this.add_button.Text = "Добавить";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // goods_weight
            // 
            this.goods_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.goods_weight.Location = new System.Drawing.Point(199, 168);
            this.goods_weight.Multiline = true;
            this.goods_weight.Name = "goods_weight";
            this.goods_weight.Size = new System.Drawing.Size(220, 39);
            this.goods_weight.TabIndex = 25;
            this.goods_weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.goods_weight.TextChanged += new System.EventHandler(this.goods_weight_TextChanged);
            // 
            // goods_name
            // 
            this.goods_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.goods_name.Location = new System.Drawing.Point(199, 123);
            this.goods_name.Multiline = true;
            this.goods_name.Name = "goods_name";
            this.goods_name.Size = new System.Drawing.Size(220, 39);
            this.goods_name.TabIndex = 24;
            this.goods_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.goods_name.TextChanged += new System.EventHandler(this.goods_name_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label1.Location = new System.Drawing.Point(23, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Изменение груза на пикете №";
            // 
            // ChangeGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 427);
            this.Controls.Add(this.panel1);
            this.Name = "ChangeGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeGoods";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox goods_volume;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.TextBox goods_weight;
        private System.Windows.Forms.TextBox goods_name;
        private System.Windows.Forms.Label notify;
        private System.Windows.Forms.TextBox picket_num;
        private System.Windows.Forms.Label room_num;
        private System.Windows.Forms.Label label4;
    }
}