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
    public partial class CreateArea : Form
    {
        public CreateArea(int room)
        {
            InitializeComponent();

            room_num.Text = room.ToString();

            range_field.Text = "пример - 101-104";
            range_field.ForeColor = Color.Gray;
            notify.Text = "";
        }

        //удаляем вспомогательный текст
        private void range_field_Enter(object sender, EventArgs e)
        {
            if (range_field.Text == "пример - 101-104")
            {
                range_field.Text = "";
                range_field.ForeColor = Color.Black;
            }
        }
        //Выводим вспомогательнй текст
        private void range_field_Leave(object sender, EventArgs e)
        {
            if (range_field.Text == "")
            {
                range_field.Text = "пример - 101-104";
                notify.Text = "";
                range_field.ForeColor = Color.Gray;
            }
        }

        //Проверка на формат и ввод необходимых параметров 
        private Boolean check_user_type()
        {
            if (range_field.Text == "пример - 101-104")
            {
                notify.Text = "Поле обязательно для заполнения!";
                return false;
            }
            //проверка на правильный ввод диапазона
            string[] field_list = range_field.Text.Split('-');
            try
            {
                Int32.Parse(field_list[0]);
                Int32.Parse(field_list[1]);
                Int32.Parse(room_num.Text);
            }
            catch
            {
                notify.Text = "Неверный формат: " + range_field.Text;
                return false;
            }
            return true;
        }


        private void create_button_Click(object sender, EventArgs e)
        {
           if (!check_user_type())
            {
                return;
            }

            DatabaseQueries query = new DatabaseQueries();


            //Проверка, что пикеты существуют и находятся на одном и том же складе
            string[] field_list = range_field.Text.Split('-');
            if (!query.picket_exsist(Int32.Parse(field_list[0]), Int32.Parse(room_num.Text)))
            {
                notify.Text = "Пикет " + field_list[0] + " не существует или находится в другом складе";
                return;
            }
            else if (!query.picket_exsist(Int32.Parse(field_list[1]), Int32.Parse(room_num.Text)))
            {
                notify.Text = "Пикет " + field_list[1] + " не существует или находится в другом складе";
                return;
            }



            //Сохраняем
            if (!query.dump_to_history())
            {
                MessageBox.Show("Возникла ошибка при сохранении БД до изменений, попробуйте еще раз.");
                return;
            }

            //Все проверки пройдены -> Если пикеты состояли в площадках - разделяем на одиночные
            MainWindow MainWindow_form = new MainWindow();
            for (int picket = Int32.Parse(field_list[0]); picket <= Int32.Parse(field_list[1]); picket++)
            {
                
                MainWindow_form.devide_from_area(picket, Int32.Parse(room_num.Text));
                //задаем новое поле для пикетов
                if (!query.set_field_picket(picket, range_field.Text, Int32.Parse(room_num.Text)))
                {
                    //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                    return;
                }
            }




            //Задаем новую площадку для пикетов
            for (int i = Int32.Parse(field_list[0]); i <= Int32.Parse(field_list[1]); i++)
            {
                if (!query.set_field_picket(i, range_field.Text, Int32.Parse(room_num.Text)))
                {
                    //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                    return;
                }
            }
            notify.Text = "Площадка "+ range_field.Text+" успешно создана!";
            
        }

        //удаляем предупреждающий об ошибке текст
        private void range_field_TextChanged(object sender, EventArgs e)
        {
            if (notify.Text != "")
            {
                notify.Text = "";
            }
            
        }

    }
}
