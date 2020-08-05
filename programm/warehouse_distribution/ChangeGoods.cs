using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Task
{
    public partial class ChangeGoods : Form
    {
        DatabaseQueries query = new DatabaseQueries();


        public ChangeGoods(int picket, int room)
        {
            InitializeComponent();

            picket_num.Text = picket.ToString();
            room_num.Text = room.ToString();

            //Ограничение на максимальное кол-во символов в строке
            picket_num.MaxLength = 3;
            goods_name.MaxLength = 10;
            goods_volume.MaxLength = 10;
            goods_weight.MaxLength = 10;

            query.get_picket_info(picket, "last_info");
            Dictionary<string, string> picket_info = query.get_picket_info(picket, "last_info");//получаем информацию по пикету

            notify.Text = "";


            try
            {
                goods_name.Text = picket_info["Тип груза: "];
                goods_volume.Text = picket_info["Объем груза: "].Split(' ')[0];
                goods_weight.Text = picket_info["Вес: "].Split(' ')[0];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                notify.Text = "Неверный ответ от БД\nИнформация по пикету не найдена!";
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            //Проверяем вписанные значения
            if (!checkIntFormat(goods_volume.Text)
                || !checkIntFormat(goods_weight.Text)
                || !checkIntFormat(picket_num.Text))
            {
                return;
            }

            //проверяем верный ответ с бд
            Dictionary<string, string> picket_info = query.get_picket_info(Int32.Parse(picket_num.Text), "last_info");
            try
            {
                _ = picket_info["Макс. вес: "];
                _ = picket_info["Макс. объем: "];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                notify.Text = "Неверный ответ от БД\nИнформация по пикету не найдена!";
                return;
            }

            //Проверяем, что вес груза не превышает лимитов пикета
            string max_weight = picket_info["Макс. вес: "].Split(' ')[0];
            string max_volume = picket_info["Макс. объем: "].Split(' ')[0];
            if (Int32.Parse(max_weight) < Int32.Parse(goods_weight.Text))
            {
                notify.Text = "Вес груза превшает допустимый вес пикета!\nМаксимальный вес пикета: "+ max_weight;
                return;
            }
            else if (Int32.Parse(max_volume) < Int32.Parse(goods_volume.Text))
            {
                notify.Text = "Объем груза превышает допустимый объем пикета!\nМаксимальный объем пикета: "+ max_volume;
                return;
            }
                
            if (query.update_picket_goods(Int32.Parse(picket_num.Text), Int32.Parse(room_num.Text), goods_name.Text, Int32.Parse(goods_volume.Text), Int32.Parse(goods_weight.Text)))
            {
                notify.Text = "Груз успешно изменен!";
            }
            else
            {
                notify.Text = "Ошибка при обащении к БД, попробуйте еще раз.";
            }
                 


            
        }

        //Проверка на число
        private Boolean checkIntFormat(string num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                notify.Text = "Неверный формат: " + num + " \nОжидается числовое значение.";
                return false;
            }
        }


        //Далее следуют фунции обнуления вспомогательного текста об ошибках, при вводе
        private void picket_num_TextChanged(object sender, EventArgs e)
        {
            if (notify.Text != "")
            {
                notify.Text = "";
            }
        }

        private void goods_name_TextChanged(object sender, EventArgs e)
        {
            if (notify.Text != "")
            {
                notify.Text = "";
            }
        }

        private void goods_weight_TextChanged(object sender, EventArgs e)
        {
            if (notify.Text != "")
            {
                notify.Text = "";
            }
        }

        private void goods_volume_TextChanged(object sender, EventArgs e)
        {
            if (notify.Text != "")
            {
                notify.Text = "";
            }
        }
    }
}
