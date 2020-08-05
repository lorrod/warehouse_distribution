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
    public partial class CreatePicket : Form
    {
        
        public CreatePicket(int room)
        {
            InitializeComponent();

            picket_num.Text = "пример - 101";
            picket_num.ForeColor = Color.Gray;

            field_name.Text = "пример - 101-103";
            field_name.ForeColor = Color.Gray;

            goods_name.Text = "пример - Уголь";
            goods_name.ForeColor = Color.Gray;

            goods_weight.Text = "пример - 10000";
            goods_weight.ForeColor = Color.Gray;

            max_weight.Text = "пример - 15000";
            max_weight.ForeColor = Color.Gray;

            max_volume.Text = "пример - 100";
            max_volume.ForeColor = Color.Gray;

            goods_volume.Text = "пример - 50";
            goods_volume.ForeColor = Color.Gray;

            room_num.Text = room.ToString();

        }
        //Проверка на формат и ввод необходимых параметров 
        private Boolean check_user_type()
        {
            if (!checkIntFormat(picket_num.Text))
            {
                MessageBox.Show("Введите номер пикета в числовом формате!");
                return false;
            }
            else if (!checkIntFormat(max_weight.Text))
            {
                MessageBox.Show("Введите максимальный вес груза для пикета в числовом формате!");
                return false;
            }
            else if (!checkIntFormat(max_volume.Text))
            {
                MessageBox.Show("Введите максимальный объем груза для пикета в числовом формате!");
                return false;
            }
            else if (!checkIntFormat(room_num.Text))
            {
                MessageBox.Show("Необходимо ввести номер склада в числовом формате!");
                return false;
            }
            
            //Если поля остались незаполненными, то ставим значения 0 | Неизвестно | для поля (знач пикета-знач пикета)
            if (field_name.Text == "пример - 101-103")
            {
                field_name.Text = picket_num.Text + "-" + picket_num.Text;
                field_name.ForeColor = Color.Black;
            }
            if (goods_name.Text == "пример - Уголь")
            {
                goods_name.Text = "Неизвестно";
                goods_name.ForeColor = Color.Black;
            }
            if (goods_weight.Text == "пример - 10000")
            {
                goods_weight.Text = "0";
                goods_weight.ForeColor = Color.Black;
            }
            if (goods_volume.Text == "пример - 50")
            {
                goods_volume.Text = "0";
                goods_volume.ForeColor = Color.Black;
            }

            string[] field_name_list = field_name.Text.Split('-');


            

                if (field_name_list.Length != 2)
            {
                MessageBox.Show("Неверный формат ввода площадки! \nПример правильного ввода: '101-104'");
                return false;
            }

            if (!checkIntFormat(goods_weight.Text)
                   || !checkIntFormat(goods_volume.Text)
                   || !checkIntFormat(field_name_list[0])
                   || !checkIntFormat(field_name_list[1]))
            {
                return false;
            }
            if (field_name_list[0] == field_name_list[1])
            {
                field_name.Text = picket_num.Text + "-" + picket_num.Text;
            }

            return true;
        }
        private void add_button_Click(object sender, EventArgs e)
        {

            if (!check_user_type())
            {
                return;
            }

            //Проверка, что груз помещается на пикете:
            if (Int32.Parse(max_weight.Text) < Int32.Parse(goods_weight.Text)
                & Int32.Parse(max_volume.Text) < Int32.Parse(goods_volume.Text))
            {
                MessageBox.Show("Объем/вес груза не должен превышать максимальный объем/вес пикета!");
                return;
            }



            //Сделаем тут дамп, потому что если дамп делать в процедуре вставки пикета, то не будет учтено, 
            // что некоторые пикеты могли образовать
            // новую площадку
            DatabaseQueries query = new DatabaseQueries();
            if (!query.dump_to_history())
            {
                MessageBox.Show("Возникла ошибка при сохранении БД до изменений, попробуйте еще раз.");
                return;
            }



            //Проверка, что пикет находится в указаной площадке и пикеты в площадке существуют
            bool picket_in_field = false;
            string[] field_name_list = field_name.Text.Split('-');
            for (int picket = Int32.Parse(field_name_list[0]); picket <= Int32.Parse(field_name_list[1]); picket++)
            {
                //Все пикеты кроме создаваемого должны существовать
                if (picket != Int32.Parse(picket_num.Text))
                {
                    if (!query.picket_exsist(picket, Int32.Parse(room_num.Text)))
                    {
                        MessageBox.Show("Невозможно создать площадку, пикет " + picket.ToString() + " не существует.");
                        return;
                    }
                   
                }
                
                if (picket == Int32.Parse(picket_num.Text))
                {
                    picket_in_field = true;
                }
            }

            
            if (!picket_in_field)
            {
                MessageBox.Show("Создаваемый пикет должен находится в пределах создаваемой площадки!");
                return;
            }


                //Проверка, что пикет новый и добавление в БД
                if (!query.insert_picket(
                Int32.Parse(picket_num.Text),
                Int32.Parse(room_num.Text),
                field_name.Text,
                goods_name.Text,
                Int32.Parse(goods_weight.Text),
                Int32.Parse(goods_volume.Text),
                Int32.Parse(max_weight.Text),
                Int32.Parse(max_volume.Text)
                ))
            {

                MessageBox.Show("Пикет уже находится в базе данных!");
                return;
            }


            //все проверки пройдены, зададим новые площадки для остальных пикетов в площадке
            //Получим функцию разделения пикетов на одиночные площадки для каждого пикета
            MainWindow MainWindow_form = new MainWindow();
            for (int picket = Int32.Parse(field_name_list[0]); picket <= Int32.Parse(field_name_list[1]); picket++)
            {
                if (picket != Int32.Parse(picket_num.Text))
                {
                    //разбиваем старую площадку пикетов, которые входят в новую площадку, чтобы задать новую
                    MainWindow_form.devide_from_area(picket, Int32.Parse(room_num.Text));
                    //задаем новое поле для пикетов
                    if (!query.set_field_picket(picket, field_name.Text, Int32.Parse(room_num.Text)))
                    {
                        //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                        MessageBox.Show("Произошла ошибка при создании площадки для пикета "+picket.ToString() +"Попробуйте  еще раз или задайте другие границы площадки");
                        return;
                    }
                }
            }

            MessageBox.Show("Успешно добавлено в базу данных");




        }
        //Провека на числовое значение
        private Boolean checkIntFormat(string num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                MessageBox.Show("Неверный формат значения: " + num);
                return false;
            }
        }



        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void picket_num_Enter(object sender, EventArgs e)
        {
            if (picket_num.Text == "пример - 101")
            {
                picket_num.Text = "";
                picket_num.ForeColor = Color.Black;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void picket_num_Leave(object sender, EventArgs e)
        {
            if (picket_num.Text == "")
            {
                picket_num.Text = "пример - 101";
                picket_num.ForeColor = Color.Gray;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void field_name_Enter(object sender, EventArgs e)
        {
            if (field_name.Text == "пример - 101-103")
            {
                field_name.Text = "";
                field_name.ForeColor = Color.Black;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void field_name_Leave(object sender, EventArgs e)
        {
            if (field_name.Text == "")
            {
                field_name.Text = "пример - 101-103";
                field_name.ForeColor = Color.Gray;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_name_Enter(object sender, EventArgs e)
        {
            if (goods_name.Text == "пример - Уголь")
            {
                goods_name.Text = "";
                goods_name.ForeColor = Color.Black;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_name_Leave(object sender, EventArgs e)
        {
            if (goods_name.Text == "")
            {
                goods_name.Text = "пример - Уголь";
                goods_name.ForeColor = Color.Gray;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_weight_Enter(object sender, EventArgs e)
        {
            if (goods_weight.Text == "пример - 10000")
            {
                goods_weight.Text = "";
                goods_weight.ForeColor = Color.Black;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_weight_Leave(object sender, EventArgs e)
        {
            if (goods_weight.Text == "")
            {
                goods_weight.Text = "пример - 10000";
                goods_weight.ForeColor = Color.Gray;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void max_weight_Enter(object sender, EventArgs e)
        {
            if (max_weight.Text == "пример - 15000")
            {
                max_weight.Text = "";
                max_weight.ForeColor = Color.Black;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void max_weight_Leave(object sender, EventArgs e)
        {
            if (max_weight.Text == "")
            {
                max_weight.Text = "пример - 15000";
                max_weight.ForeColor = Color.Gray;

            }
        }

        private void max_volume_Enter(object sender, EventArgs e)
        {
            if (max_volume.Text == "пример - 100")
            {
                max_volume.Text = "";
                max_volume.ForeColor = Color.Black;

            }
        }

        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void max_volume_Leave(object sender, EventArgs e)
        {
            if (max_volume.Text == "")
            {
                max_volume.Text = "пример - 100";
                max_volume.ForeColor = Color.Gray;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_volume_Enter(object sender, EventArgs e)
        {
            if (goods_volume.Text == "пример - 50")
            {
                goods_volume.Text = "";
                goods_volume.ForeColor = Color.Black;

            }
        }
        //Функции отвечающие за серый текст, отображение \ скрытие подсказок
        private void goods_volume_Leave(object sender, EventArgs e)
        {
            if (goods_volume.Text == "")
            {
                goods_volume.Text = "пример - 50";
                goods_volume.ForeColor = Color.Gray;

            }
        }

    }
}
