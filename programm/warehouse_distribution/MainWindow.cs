using Npgsql;
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
    public partial class MainWindow : Form
    {

        DatabaseQueries query = new DatabaseQueries();

        //отвечает за показ конкретного склада
        int selected_room = 1;
        int selected_picket = 0;
        //используется для выбора таблицы из БД || last_info - отображает текущее положение на складе
        string current_table = "last_info";

        //ограничивает до (color_picker.Count) площадок на экране пользователя
        //если пикет не состовляет площадку с другими пикетами - пикет считается одиночной площадкой
        List<String> color_picker = new List<String>()
        {
            "Pink", "Yellow", "Green", "Wheat", "RoyalBlue", "PaleGreen", "OrangeRed", "MediumSlateBlue",
            "MintCream", "LightYellow", "LightPink", "Turquoise", "LightBlue", "LightRed"
        };
        public MainWindow()
        {
            InitializeComponent();
            if (query.get_history_list("last_info")[0] != "last_info")
            {
                if (!query.create_main_table())
                {
                    MessageBox.Show("Ошибка при подключении к БД. Неудалось создать таблицу.");
                    return;
                }
            }
            update_info(selected_room, current_table);
            //кнопка удаления истории
            delete_history.Hide();
            
            this.control_panel.BringToFront();
        }

        //Задаем размеры окон
        private void panel_size()
        {
            //размер окна пикетов
            var picket_field_width = this.Width - this.control_panel.Width;
            var picket_field_height = this.Height - this.storage_select.Height;

            var storage_select_width = this.Width - this.control_panel.Width;

            this.picket_field.Size = new Size(picket_field_width, picket_field_height);
            this.storage_select.Size = new Size(storage_select_width, this.storage_select.Height);
            this.control_panel.BringToFront();

            update_info(selected_room, current_table);
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            panel_size();
        }

        //Переключение между складами
        private void next_storage_Click(object sender, EventArgs e)
        {
            selected_picket = 0;
            picket_info_label.Text = "";
            selected_room += 1;
            update_info(selected_room, current_table);
        }

        private void previous_Click(object sender, EventArgs e)
        {
            selected_picket = 0;
            picket_info_label.Text = "";
            selected_room -= 1;
            update_info(selected_room, current_table);
        }

        //Используется для динамической вставки пикета на поле
        // в случае если будут склады с различным количеством пикетов или их количество увеличится
        private void insert_picket(int name, int field, int row, int column)
        {
            //размер пикета
            var picket_width = this.picket_field.Width / 8;
            var picket_height = this.picket_field.Height / 8;
            //координаты начала каждой ячейки
            var width_Coordinate = this.picket_field.Width / 6 * row - picket_width/2;
            var height_Coordinate = this.picket_field.Height / 6 * column - picket_height / 2;
            Button pick_but = new Button();
            pick_but.Location = new System.Drawing.Point(width_Coordinate, height_Coordinate);
            pick_but.Size = new Size(picket_width, picket_height);
            pick_but.TabIndex = 0;
            pick_but.Text = name.ToString();
            pick_but.UseVisualStyleBackColor = true;
            //выбор цвета для пикета
            try
            {
                pick_but.BackColor = Color.FromName(color_picker[field]);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Добавить цветов в массив color_picker или исп. др. способ");//ОГРАНИЧЕНО цветов
            }
            pick_but.Click += new EventHandler(pick_but_click);
            picket_field.Controls.Add(pick_but);

        }
        //Ловим нажатие на пикет и показываем информацию о нем
        public void pick_but_click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            picket_info_label.Text = "";//сбрасываем предыдущее значение
            selected_picket = Int32.Parse(clickedButton.Text);
            var picket_info = query.get_picket_info(selected_picket, current_table); // узнаем характеристики пикета
            foreach (KeyValuePair<string, string> keyValue in picket_info)
            {
                picket_info_label.Text += keyValue.Key + keyValue.Value + "\n";
            }

        }



        

        //Функция для обновления информации на экране по выбранному складу и количеству складов
        private void update_info(int room, string table)
        {
            picket_field.Controls.Clear();//очищаем пикеты с поля, которые были созданы ранее

            //Функция для получения данных с базы дынных по складу 
            //возвращает словарь с массивом {"Складов":[<int>количество], "Площадка n":[101,..105]..}
            //Используем table чтобы обращаться к данным из истории
            Dictionary<string, List<int>> dictionary = query.get_room_info(room, table);



            //Отображение верхней панели
            name_store.Text = "Склад №" + room.ToString();
            int count_rooms = dictionary["Складов"][0];
            if (room == 1)
            {
                previous.Hide();
                next_storage.Show();
            }
            else if (room == count_rooms)
            {
                next_storage.Hide();
                previous.Show();
            }
            else
            {
                previous.Show();
                next_storage.Show();
            }


            //Проходим по словарю от БД, в поиске значения ключей "Площадка" 
            // Если в ключе находится [пробел] значит площадка
            // словарь - {"Складов":[<int>количество], "Площадка n":[101,..105]..} 
            List<List<int>> store_info = new List<List<int>>();
            foreach (KeyValuePair<string, List<int>> keyValue in dictionary)
            {

                // Создаем двумерный массив с соответсвием площадка - пикет
                // store info [[номер площадки, номер пикет],..[номер площадки N, номер пикет N]
                string[] devide = keyValue.Key.Split(' ');
                if (devide.Length != 1)
                {
                    for (int i = 0; i < keyValue.Value.Count; i++)
                    {

                        List<int> field_info = new List<int>();

                        field_info.Add(keyValue.Value[i]);//номер пикета
                        field_info.Add(Int32.Parse(devide[1])); //номер площадки пикета

                        store_info.Add(field_info);

                    }

                }
            }
           
            //Фильтруем массив по возрастанию пикетов
            for (int i = 0; i < store_info.Count; i++)
            {
                for (int k = 0; k < store_info.Count-1; k++)
                {
                    if (store_info[k][0] > store_info[k+1][0])
                    {
                        List<int> help = store_info[k];
                        store_info[k] = store_info[k + 1];
                        store_info[k + 1] = help;
                    }
                }
            }
            

            //Динамическое отображение -> Посчитаем требуемое число строк если столбцов - 5
            int countrows = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(store_info.Count) / 5));
            //вспомогательная для прохождения всх элпементов массива store_info
            int picket = 0;
            for (int row = 1; row <= countrows; row++)
            {

                for (int column = 1; column < 6; column++)
                {
                    if (picket <= store_info.Count - 1)
                    {

                        insert_picket(store_info[picket][0], store_info[picket][1], column, row);
                        picket += 1;
                    }

                }
            }

        }

        //Открытие окна создания пикета
        private void create_picket_Click(object sender, EventArgs e)
        {

            CreatePicket CreatePicket_form = new CreatePicket(selected_room);
            CreatePicket_form.FormClosed += new FormClosedEventHandler(f_FormClosed);
            CreatePicket_form.Show();

        }

        //Кнопка отделения пикета от площадки
        private void create_devide_area_Click(object sender, EventArgs e)
        {
            
            //проверка, что был выбран пикет
            if (selected_picket == 0)
            {
                picket_info_label.Text = "Выберите пикет!";
                return;
            }
            //создаем таблицу истории изменений
            if (!query.dump_to_history())
            {
                MessageBox.Show("Возникла ошибка при сохранении БД до изменений, попробуйте еще раз.");
                return;
            }
            if (devide_from_area(selected_picket, selected_room))
            {
                //получаем изменения из базы
                update_info(selected_room, current_table);
            }
            else
            {
                picket_info_label.Text = "Не удалось задать новую площадку одному из пикетов поля\nПикет не существует";
            }
        }

        //Функция отделения пикета от площадки
    public bool devide_from_area(int selected_picket, int selected_room)
    {
        //Узнаем к какой площадке принадлежит пикет
            string field = query.get_field_of_picket(selected_picket, selected_room);
        //Получим два числа, начало площадки и конец, надо рассмотреть 3 ситуации
        //                                  -отделяемый пикет находится в начале площадки
        //                                  -отделяемый пикет находится в конце площадки
        //                                  -отделяемый пикет находится не в начале и не в конце площадки
        String[] borders_field = field.Split('-');
            if (borders_field.Length != 2)
            {
                return false;
            }
            
        if (borders_field[0] == selected_picket.ToString())
        {
            //Создаем площадку, граница которой сдвинулась на +1 от начала
            //для selected_picket задаем площадку "selected_picket-selected_picket"
            string new_field = (Int32.Parse(borders_field[0]) + 1).ToString() + "-" + borders_field[1];
            //Для того, чтобы было меньше обращений к базе отправим команду на изменение площадки всем пикетам
            // которые присутствовали в данной площадке, а не каждому пикету

                if (!query.set_field_where_field(field, new_field, selected_room))
                {
                    //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                    MessageBox.Show("Произошла ошибка при создании площадки для пикета " + selected_picket.ToString() + "Попробуйте  еще раз или задайте другие границы площадки");
                    return false;
                }
                //и отдельную комманду для изменения площадки выбранного пикета
                new_field = selected_picket.ToString() + "-" + selected_picket.ToString();
            if (!query.set_field_picket(selected_picket, new_field, selected_room))
            {
                //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                MessageBox.Show("Произошла ошибка при создании площадки для пикета " + selected_picket.ToString() + "Попробуйте  еще раз или задайте другие границы площадки");
                return false;
            }
            }
        else if (borders_field[1] == selected_picket.ToString())
        {
            //аналогично 1 случаю только с другой границей
            string new_field = borders_field[0] + "-" + (Int32.Parse(borders_field[1]) - 1).ToString();
                if (!query.set_field_where_field(field, new_field, selected_room))
                {
                    //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                    MessageBox.Show("Произошла ошибка при создании площадки для пикета " + selected_picket.ToString() + "Попробуйте  еще раз или задайте другие границы площадки");
                    return false;
                }
                new_field = selected_picket.ToString() + "-" + selected_picket.ToString();
            if (!query.set_field_picket(selected_picket, new_field, selected_room))
            {
                //TODO вызов функции возвращения к последнему дампу и удаление последнего дампа
                MessageBox.Show("Произошла ошибка при создании площадки для пикета " + selected_picket.ToString() + "Попробуйте  еще раз или задайте другие границы площадки");
                return false;
            }
            }
        else
        {
            //в этом случае нам нужно разбить площадку на 3 площадки
            // до пикета | одиночная площадка пикета | после пикета
            List<int> before_selected = new List<int>();
            List<int> after_selected = new List<int>();
            for (int i = Int32.Parse(borders_field[0]); i <= Int32.Parse(borders_field[1]); i++)
            {
                if (i < selected_picket)
                {
                    before_selected.Add(i);
                }
                else if (i > selected_picket)
                {
                    after_selected.Add(i);
                }
            }
            /*
            try
            {
                _ = before_selected[0];
                _ = before_selected[before_selected.Count - 1];
            }
            catch (System.ArgumentOutOfRangeException)
            {

                return false;
            }
            */
                //получаем границы новых площадок и задаем каждому пикету в этой площадке
                string new_field = before_selected[0].ToString() + "-" + before_selected[before_selected.Count - 1].ToString();
            for (int picket = before_selected[0]; picket <= before_selected[before_selected.Count - 1]; picket++)
            {
                query.set_field_picket(picket, new_field, selected_room);
            }
            new_field = after_selected[0].ToString() + "-" + after_selected[after_selected.Count - 1].ToString();
            for (int picket = after_selected[0]; picket <= after_selected[after_selected.Count - 1]; picket++)
            {
                query.set_field_picket(picket, new_field, selected_room);
            }

            new_field = selected_picket.ToString() + "-" + selected_picket.ToString();
            query.set_field_picket(selected_picket, new_field, selected_room);

        }
        return true;
    }

        private void create_area_Click(object sender, EventArgs e)
        {
            CreateArea CreateArea_form = new CreateArea(selected_room);
            CreateArea_form.FormClosed += new FormClosedEventHandler(f_FormClosed);
            CreateArea_form.Show();
        }

        private void set_weight_Click(object sender, EventArgs e)
            {
                if (selected_picket != 0)
                {
                    ChangeGoods ChangeGoods_form = new ChangeGoods(selected_picket, selected_room);
                    ChangeGoods_form.FormClosed += new FormClosedEventHandler(f_FormClosed);
                    ChangeGoods_form.Show();
                }
                else
                {
                    picket_info_label.Text = "Пожалуйста, выберете пикет сначала!";
                }
            }

        //Cрабатывает когда дочерние элементы закрываются
        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            update_info(selected_room, current_table);
        }

        private void search_load_Click(object sender, EventArgs e)
        {
            if (search_name.Text == "")
            {
                picket_info_label.Text = "Не заданы критерии поиска!";
                return;
            }

            //.
            Dictionary<int, List<int>> find_result = query.find_by_goods_name(search_name.Text, current_table);

            picket_info_label.Text = "";
            foreach (KeyValuePair<int, List<int>> keyValue in find_result)
            {
                
                for (int i = 0; i < keyValue.Value.Count; i++)
                {
                    
                    if (keyValue.Key == -1)
                    {
                        picket_info_label.Text = "Ошибка при обращении к БД " + keyValue.Key.ToString();
                        return;
                    }
                    else if (keyValue.Key == -2)
                    {
                        picket_info_label.Text = "Ошибка при обращении к БД " + keyValue.Key.ToString();
                        return;
                    }
                    else if (keyValue.Key != selected_room)
                    {
                        picket_info_label.Text += "Склад №" + keyValue.Key.ToString() + " Пикет: " + keyValue.Value[i].ToString() + "\n";
                    }
                    else if (keyValue.Key == selected_room)
                    {
                        picket_info_label.Text += "В текущем складе пикет: " + keyValue.Value[i].ToString() + "\n";
                    }
                }
            }
            


        }

        //Открываем или закрываем исотрию
        private void history_Click(object sender, EventArgs e)
        {
            picket_field.Controls.Clear();//очищаем пикеты с поля, чтобы показать выбор истории
            if (history.Text == "История")

            {
                //подготавливаем пользовательский экран к показу истории
                
                set_weight.Hide();
                create_area.Hide();
                devide_area.Hide();
                create_picket.Hide();
                delete_picket.Hide();
                delete_history.Show();
                history.Text = "Закрыть историю";

                show_history_tables();


            }
            else
            {
                set_weight.Show();
                create_area.Show();
                devide_area.Show();
                create_picket.Show();
                delete_picket.Show();
                delete_history.Hide();
                history.Text = "История";
                current_table = "last_info";
                selected_room = 1;
                update_info(selected_room, current_table);

            }

            
        }

        private void show_history_tables()
        {
            //вызываем функцию которая даст массив для выбор table
            //.
            List<string> history_list = new List<string>();

            history_list = query.get_history_list(":");//на будущее можно будет кидать сюда поисковый запрос

            for (int i = 1; i <= history_list.Count; i++)
            {
                table_picker(history_list[i - 1], i);
            }

            //Отображаем построчно выбор table /  истории
        }

        private void table_picker(string name, int i)
        {
            var width_Coordinate =  20;
            var height_Coordinate = this.picket_field.Height / 8 * i  + 10;
            Button table_select = new Button();
            table_select.Location = new System.Drawing.Point(width_Coordinate , height_Coordinate - this.picket_field.Height / 12);
            table_select.Size = new Size(this.picket_field.Width - 40 , this.picket_field.Height / 12);
            table_select.TabIndex = 0;
            table_select.Text = name;
            table_select.UseVisualStyleBackColor = true; //effect activex visual
            table_select.BackColor = Color.FromName(color_picker[1]);
            table_select.Click += new EventHandler(date_select);
            picket_field.Controls.Add(table_select);
        }

        
        private void date_select(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            picket_info_label.Text = "";//сбрасываем предыдущее значение
            current_table = clickedButton.Text;
            delete_history.Hide();
            update_info(selected_room, current_table);
        }
        //Удаляем историю из БД
        private void delete_history_Click(object sender, EventArgs e)
        {
            //.

            //можно будет удалять по ключу
            string key = "%:";//очищает всю историю
            
            if (!query.delete_history(key))
            {
                MessageBox.Show("Возникла ошибка, попробуйте еще раз.");
                return;
            }
            picket_field.Controls.Clear();
            show_history_tables();
        }

        //Удаление пикета
        private void delete_picket_Click(object sender, EventArgs e)
        {
             
            //проверка, что был выбран пикет 
            if (selected_picket == 0)
            {
                picket_info_label.Text = "Пикет не выбран!";
                return;
            }

            if (!devide_from_area(selected_picket, selected_room))
            {
                picket_info_label.Text = "Не удалось задать новую площадку одному из пикетов поля\nПикет не существует";
                return;
            }

            if (!query.dump_to_history())
            {
                MessageBox.Show("Возникла ошибка при сохранении БД до изменений, попробуйте еще раз.");
                return;
            }

            query.delete_picket(selected_picket, selected_room);
            update_info(selected_room, "last_info");
        }

        
    }
}

