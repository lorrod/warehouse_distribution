using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_Task
{
    class DatabaseQueries
    {

        NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=verysecret; Database=store_data;");

        private void open_db_connection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                try
                {
                    connection.Open();
                }
                catch (System.TimeoutException)
                {
                    //на будущее сделать появление окна сообщающее об ошибке и заверение приложения
                    Console.WriteLine("Ошибка подключения к БД. Проверьте правильность ввода  данных к БД (ip, port)");
                }
                catch (Npgsql.PostgresException)
                {
                    Console.WriteLine("Ошибка подключения к БД. Проверьте правильность ввода  данных к БД");
                }
        }
        private void close_db_connection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        private NpgsqlConnection getConnection()
        {
            return connection;
        }

        public Boolean create_main_table()
        {

            string command = "CREATE TABLE last_info ( picket_num INT, store_num INT, field_name CHAR(10), goods_name CHAR(30),goods_weight INT, goods_volume INT, max_weight INT, max_volume INT);";
            NpgsqlCommand last_info_create_command = new NpgsqlCommand(command, connection);
            open_db_connection();

            try
            {
                last_info_create_command.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }



            close_db_connection();

            return true;
        }

        public Boolean insert_picket(int picket_num, int store_num, string field_name, string goods_name, int goods_weight, int goods_volume, int max_weight, int max_volume)
        {
            //возможно создание доп функции БД для снижения колва запросов
            if (!picket_exsist(picket_num, store_num))
            {


                NpgsqlCommand insert_command = new NpgsqlCommand("insert_picket", connection);
                insert_command.CommandType = CommandType.StoredProcedure;
                insert_command.Parameters.AddWithValue("@picket_num", picket_num);
                insert_command.Parameters.AddWithValue("@store_num", store_num);
                insert_command.Parameters.AddWithValue("@field_name", field_name);
                insert_command.Parameters.AddWithValue("@goods_name", goods_name);
                insert_command.Parameters.AddWithValue("@goods_weight", goods_weight);
                insert_command.Parameters.AddWithValue("@goods_volume", goods_volume);
                insert_command.Parameters.AddWithValue("@max_weight", max_weight);
                insert_command.Parameters.AddWithValue("@max_volume", max_volume);

                open_db_connection();

                insert_command.ExecuteNonQuery();

                close_db_connection();
                return true;
            }
            else
            {
                return false;
            }

        }

        //проверка пикета на существование
        public Boolean picket_exsist(int picket_num, int store_num)
        {

            NpgsqlCommand exsist_command = new NpgsqlCommand("picket_exsist", connection);
            exsist_command.CommandType = CommandType.StoredProcedure;
            exsist_command.Parameters.AddWithValue("@_picket_num", picket_num);
            exsist_command.Parameters.AddWithValue("@_store_num", store_num);

            open_db_connection();

            var check_exsist = exsist_command.ExecuteScalar();

            close_db_connection();

            if (check_exsist.ToString() == "True")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Функция для получения данных с базы дынных по складу 
        //возвращает словарь с массивом {"Складов":[<int>количество], "Площадка n":[101,..105]}
        //Используем table чтобы обращаться к данным из истории
        public Dictionary<string, List<int>> get_room_info(int selected, string table)
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            //Npgsql.PostgresException: "42601: ошибка синтаксиса (примерное положение: "$1")"
            //Не получается сделать prepared statement для указания table
            string count_rooms_query = String.Format("SELECT COUNT(DISTINCT store_num) FROM \"{0}\";", table);

            string get_fields_query = String.Format("SELECT DISTINCT field_name FROM \"{0}\" WHERE store_num = @selected;", table);

            NpgsqlCommand cnt_room_cmd = new NpgsqlCommand(count_rooms_query, connection);
            //cnt_room_cmd.Parameters.AddWithValue("table", table); "42601: ошибка синтаксиса (примерное положение: "$1")"

            NpgsqlCommand get_fields_cmd = new NpgsqlCommand(get_fields_query, connection);
            //get_fields_cmd.Parameters.AddWithValue("table", table); "42601: ошибка синтаксиса (примерное положение: "$1")"
            get_fields_cmd.Parameters.AddWithValue("selected", selected);

            dict["Складов"] = new List<int>();
            open_db_connection();
            //Получаем результат количества складов: результат команды COUNT -> int
            var count = cnt_room_cmd.ExecuteScalar();


            NpgsqlDataReader fields = get_fields_cmd.ExecuteReader();


            try
            {
                if (count != null)
                {
                    dict["Складов"].Add(Int32.Parse(count.ToString()));
                }
            }
            catch (Exception ex)
            {
                //ошибка если один пользователь очистит историю, а второй пользователь попробует обратиться к таблицы, которой уже не существует
                dict["Складов"].Add(0);
                close_db_connection();
                return dict;
            }

            //Блок try используется для освобождения reader'a
            try
            {
                if (fields != null)
                {
                    int i = 1;
                    while (fields.Read())//в fields получили значения площадок пикетов 109-115 | 115-155...
                    {
                        var cache = fields[0].ToString().Split('-');//раздляем два числа
                                                                    //Таким образом передача пикетов на экран пользователя осуществляется
                                                                    //благодаря диапазону площадки

                        dict["Площадка " + i.ToString()] = new List<int>();
                        for (int k = Int32.Parse(cache[0]); k <= Int32.Parse(cache[1]); k++)//добавляем в массив каждый пикет по номеру его площадки
                        {

                            dict["Площадка " + i.ToString()].Add(k);

                        }

                        i++;

                    }
                }
            }
            finally
            {
                fields.Dispose();
            }
           
            close_db_connection();

            return dict;
        }


        //Функция для получения данных о характеристиках конкретного пикета вернет массив со всей инф о пикете[вес, размер...]
        //Используем table чтобы обращаться к данным из истории
        public Dictionary<string, string> get_picket_info(int selected, string table)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();

            //Npgsql.PostgresException: "42601: ошибка синтаксиса (примерное положение: "$1")"
            //Не получается сделать prepared statement для указания table, поэтому форматированная строка может представить угрозу
            //решается путем создания функции с входными параметрами
            string picket_query = String.Format("SELECT * FROM \"{0}\" WHERE picket_num = @name;", table);
            NpgsqlCommand picket_info_cmd = new NpgsqlCommand(picket_query, connection);
            picket_info_cmd.Parameters.AddWithValue("name", selected);


            open_db_connection();

            NpgsqlDataReader picket_options = picket_info_cmd.ExecuteReader();

            
            try
            {
                if (picket_options != null)
                {
                    if (picket_options.HasRows)
                    {
                        for (int i = 0; picket_options.Read(); i++)
                        {
                            info["Тип груза: "] = picket_options[3].ToString();
                            info["Вес: "] = picket_options[4].ToString() + " тонн";
                            info["Объем груза: "] = picket_options[5].ToString() + " м ^3";
                            info["В площадке: "] = picket_options[2].ToString();
                            info["Макс. вес: "] = picket_options[6].ToString() + " тонн";
                            info["Макс. объем: "] = picket_options[7].ToString() + " м ^3";
                        }
                    }
                    else
                    {
                        info["Пикет не существует: "] = "отображение несуществующего пикета происходит за счет неверно созданной площадки, в которой присутствует данный пикет";
                    }

                }
                else
                {
                    info["Ошибка БД: "] = "Пикет не найден";
                }
            }
            catch (Exception ex)
            {
                info["Ошибка БД: "] = ex.ToString();
            }
            finally
            {
                //Освободаем reader
                picket_options.Dispose();
            }
            

            close_db_connection();

            //параметры пикета: груз который лежит || количество груза || к какой площадке принадлежит || максимальный вес || объем
            return info;
        }

        //Возвращает информацю о пощадке в виду string (101-104)
        public string get_field_of_picket(int selected, int room)
        {


            NpgsqlCommand field_picket_command = new NpgsqlCommand("get_field_picket", connection);
            field_picket_command.CommandType = CommandType.StoredProcedure;
            field_picket_command.Parameters.AddWithValue("@picket_name", selected);
            field_picket_command.Parameters.AddWithValue("@room", room);

            open_db_connection();

            var field_name = field_picket_command.ExecuteScalar();
            if (field_name == null)
            {
                field_name = "Ошибка БД";
            }

            close_db_connection();
            return field_name.ToString();
        }

        //Задает площадку для всех пикетов, которые  создавали опеределнную площадку
        public Boolean set_field_where_field(string old_field, string new_field, int room)
        {
            NpgsqlCommand set_field_command = new NpgsqlCommand("set_field_where_field", connection);
            set_field_command.CommandType = CommandType.StoredProcedure;
            set_field_command.Parameters.AddWithValue("@_old_field", old_field);
            set_field_command.Parameters.AddWithValue("@_new_field", new_field);
            set_field_command.Parameters.AddWithValue("@room", room);

            open_db_connection();

            try
            {
                set_field_command.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }

            close_db_connection();
            return true;
        }


        //Задает площадку для пикета
        public Boolean set_field_picket(int picket_name, string new_field, int room)
        {
            NpgsqlCommand set_field_command = new NpgsqlCommand("set_field_picket", connection);
            set_field_command.CommandType = CommandType.StoredProcedure;
            set_field_command.Parameters.AddWithValue("@_picket_name", picket_name);
            set_field_command.Parameters.AddWithValue("@_new_field", new_field);
            set_field_command.Parameters.AddWithValue("@room", room);

            open_db_connection();

            try
            {
                set_field_command.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }

            close_db_connection();
            return true;
        }

        //Обновляет данные о грузе лежащем в пикете
        public Boolean update_picket_goods(int picket_name, int room, string goods_name, int goods_volume, int goods_weight)
        {
            NpgsqlCommand update_goods_command = new NpgsqlCommand("update_goods", connection);
            update_goods_command.CommandType = CommandType.StoredProcedure;
            update_goods_command.Parameters.AddWithValue("@_picket_num", picket_name);
            update_goods_command.Parameters.AddWithValue("@_store_num", room);
            update_goods_command.Parameters.AddWithValue("@_goods_name", goods_name);
            update_goods_command.Parameters.AddWithValue("@_goods_volume", goods_volume);
            update_goods_command.Parameters.AddWithValue("@_goods_weight", goods_weight);

            open_db_connection();

            try
            {
                update_goods_command.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }

            close_db_connection();
            return true;
        }


        //Поиск по названию груза в пикете -> возвращает словарь (ключ номер склада, значение клча массив с номерами пикетов)
        public Dictionary<int,List<int>> find_by_goods_name(string goods_name, string table)
        {
            string find_query = String.Format("select picket_num, store_num from \"{0}\" WHERE goods_name = @_goods_name;", table);
            NpgsqlCommand find_cmd = new NpgsqlCommand(find_query, connection);
            find_cmd.Parameters.AddWithValue("_goods_name", goods_name);

            Dictionary<int, List<int>> search_result = new Dictionary<int, List<int>>();

            open_db_connection();

            NpgsqlDataReader result = find_cmd.ExecuteReader();

            
            try
            {
                if (result != null)
                {
                    if (result.HasRows)
                    {
                        for (int i = 0; result.Read(); i++)
                        {
                            //Во избежание лишних запросов к базе, пробуем обратится к ключу, если не создан - создаем
                            //  result[1] - это номер склада
                            //  result[0] - это номер пикета
                            try
                            {
                                search_result[Int32.Parse(result[1].ToString())].Add(Int32.Parse(result[0].ToString()));
                            }
                            catch (System.Collections.Generic.KeyNotFoundException)
                            {
                                search_result[Int32.Parse(result[1].ToString())] = new List<int>();
                                search_result[Int32.Parse(result[1].ToString())].Add(Int32.Parse(result[0].ToString()));
                            }
                        }
                    }
                    else
                    {
                        //Ничего не найден
                        search_result[-1] = new List<int>();
                        search_result[-1].Add(-1);
                    }
                }
                else
                {
                    
                    search_result[-2] = new List<int>();
                    search_result[-2].Add(-2);
                }
            }
            catch
            {
                //передаем сообщение об ошибке
                    search_result[-3] = new List<int>();
                    search_result[-3].Add(-3);
            }
            finally
            {
                //закрываем reader
                result.Dispose();
            }
            


            close_db_connection();

            return search_result;
        }
            
        //получаем список таблиц дампов
        //++поиск таблицы last_info
        public List<string> get_history_list(string search_for)
        {
            //для запроса с параметрами
            //string find_history = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND table_name LIKE '%:%' AND table_name LIKE '@search_details';";

            string find_history = String.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND table_name LIKE '%{0}%';", search_for);

            NpgsqlCommand find_cmd = new NpgsqlCommand(find_history, connection);

            //для запроса с параметрами
            //find_cmd.Parameters.AddWithValue("search_details", search_for);

            List<string> search_result = new List<string>();

            open_db_connection();

            NpgsqlDataReader result = find_cmd.ExecuteReader();

            try
            {
                
                //Проверка что результат пришел
                if (result != null)
                {
                    //проверка что результат содержит полезную информацию
                    if (result.HasRows)
                    {
                        //формирование выходного массива
                        for (int i = 0; result.Read(); i++)
                        {
                            search_result.Add(result[0].ToString());
                        }
                    }
                    else
                    {
                        search_result.Add("История изменений пуста.");
                    }
                }
                else
                {
                    search_result.Add("Результат обращения к БД - null");
                }
            }
            catch (Exception ex)
            {
                search_result.Add("Ошибка из БД "+ex);
                close_db_connection();
                return search_result;
            }
            finally
            {
                //закрываем reader
                result.Dispose();
            }
            
            close_db_connection();
            return search_result;
        }


        //Удаляет пикет из БД и делает дамп в таблицу БД с именем текущего времени и даты
        //ИЗМЕНЕНО: для отслеивания дамп был вынесен в отдельную функцию
        public Boolean delete_picket(int picket_num, int store_num)
        {

            NpgsqlCommand delete_picket_cmd = new NpgsqlCommand("delete_picket", connection);
            delete_picket_cmd.CommandType = CommandType.StoredProcedure;
            delete_picket_cmd.Parameters.AddWithValue("@_picket_num", picket_num);
            delete_picket_cmd.Parameters.AddWithValue("@_store_num", store_num);

            open_db_connection();

            try
            {
                delete_picket_cmd.ExecuteNonQuery();
            }
            
            catch
            {
                close_db_connection();
                return false;
            }


            close_db_connection();
            return true;
        } 


        // сделает дамп основной таблицы БД (last_info) в таблицу с именем текущего времени и даты
        public Boolean dump_to_history()
        {
            NpgsqlCommand dump_cmd = new NpgsqlCommand("to_history", connection);
            dump_cmd.CommandType = CommandType.StoredProcedure;

            open_db_connection();

            try
            {
                dump_cmd.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }
            

            close_db_connection();
            return true;
        }

        // Удаление истории последних изменений на складах по ключевому слову | если %: на вход - удалит всю историб 
        public Boolean delete_history(string key_value)
        {
            NpgsqlCommand delete_history_cmd = new NpgsqlCommand("drop_table", connection);
            delete_history_cmd.CommandType = CommandType.StoredProcedure;
            delete_history_cmd.Parameters.AddWithValue("@_key_word", key_value);

            open_db_connection();

            try
            {
                delete_history_cmd.ExecuteNonQuery();
            }
            catch
            {
                close_db_connection();
                return false;
            }
            

            close_db_connection();
            return true;
        }

    }

}

