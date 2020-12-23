using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace TypeManShip
{
    public class DBController
    {
        TableRelay users_relay;
        TableRelay passw_entries_relay;
        Dictionary<string, TableRelay> relays;
        MySqlConnection connection;
        public DataSet data;
        public DBController()
        {
            getConnection();
            ConfigureAdapters();
            data = new DataSet();
        }
        public void getConnection()
        {
            string connect_string = "server=localhost;user=root;database=typemanship_db;port=3306;password=HyperPolaris1";
            connection = new MySqlConnection(connect_string);
        }
        public void ConfigureAdapters()
        {
            connection.Open();
            relays = new Dictionary<string, TableRelay>();
            users_relay = new TableRelay(connection, "users");
            passw_entries_relay = new TableRelay(connection, "password_entries");

            users_relay.adapter.InsertCommand = new MySqlCommand("user_entry", connection);
            users_relay.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
            users_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("login", MySqlDbType.VarString, 40, "login"));
            users_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("password", MySqlDbType.VarString, 40, "password"));
            users_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("complexity", MySqlDbType.VarString, 40, "complexity"));
            users_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("expected_speed", MySqlDbType.Float, 40, "expected_speed"));
            users_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("dispersion", MySqlDbType.Float, 40, "dispersion"));
            MySqlParameter passw_parameter = users_relay.adapter.InsertCommand.Parameters.Add("id", MySqlDbType.Int32, 0, "id");

            passw_parameter.Direction = ParameterDirection.Output;
            passw_entries_relay.adapter.InsertCommand = new MySqlCommand("password_entry", connection);
            passw_entries_relay.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("userid", MySqlDbType.Int32, 50, "userid"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("speed", MySqlDbType.Float, 0, "speed"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("total_time", MySqlDbType.Int32, 0, "total_time"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("entry_date", MySqlDbType.DateTime, 0, "entry_date"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("t_vecktor", MySqlDbType.VarString, 300, "t_vecktor"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("tau_vecktor", MySqlDbType.VarString, 300, "tau_vecktor"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("type1_imp", MySqlDbType.Int16, 0, "type1_imp"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("type2_imp", MySqlDbType.Int16, 0, "type2_imp"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("type3_imp", MySqlDbType.Int16, 0, "type3_imp"));
            passw_entries_relay.adapter.InsertCommand.Parameters.Add(new MySqlParameter("bio_vector", MySqlDbType.VarString, 300, "bio_vector"));
            MySqlParameter parameter = passw_entries_relay.adapter.InsertCommand.Parameters.Add("id", MySqlDbType.Int32, 0, "id");
            parameter.Direction = ParameterDirection.Output;
            relays.Add("users", users_relay);
            relays.Add("password_entries", passw_entries_relay);
            connection.Close();
        }
        public void GetData(string tablename, int keyidx)
        {
            connection.Open();
            //data = new DataSet("un_groups");
            relays[tablename].adapter.Fill(data);
            data.Tables[tablename].Columns[keyidx].AutoIncrement = true;
            data.Tables[tablename].PrimaryKey = new DataColumn[] { data.Tables[tablename].Columns[0] };
            data.AcceptChanges();
            connection.Close();
        }

        //public void GetStudentsData()
        //{
        //    connection.Open();
        //    groups_relay.adapter.Fill(data);
        //    data.Tables["students"].Columns[0].AutoIncrement = true;
        //    data.Tables["students"].PrimaryKey = new DataColumn[] { data.Tables["students"].Columns[0] };
        //    data.AcceptChanges();
        //    connection.Close();
        //}

        public void dataSearch()
        {
            data.Tables[0].Select();
        }

        public void UpdateDataTable(string tablename)
        {
            if (data.HasChanges())
            {
                connection.Open();
                DataTable changes = data.Tables[tablename].GetChanges();
                relays[tablename].adapter.Update(changes);
                data.Tables[tablename].Clear();
                relays[tablename].adapter.Fill(data);
                //data.Merge(changes);
                data.Tables[tablename].AcceptChanges();
                connection.Close();
            }

        }

        public void UpdateGroupsUsers()
        {
            if (data.HasChanges())
            {
                connection.Open();
                foreach (KeyValuePair<string, TableRelay> relay in relays)
                {
                    DataTable changes = data.Tables[relay.Key].GetChanges();
                    if (changes != null)
                    {
                        relay.Value.adapter.Update(changes);
                        data.Tables[relay.Key].Merge(changes,false);
                        //data.Tables[relay.Key].Clear();
                        //relay.Value.adapter.Fill(data);
                    }
                }

                //data.Merge(changes);
                data.AcceptChanges();
                connection.Close();
            }
        }


        public void FillSet()
        {

            GetData("users", 0);
            GetData("password_entries", 0);
            data.Tables["users"].Columns[0].ReadOnly = true;
            data.Tables["password_entries"].Columns[0].ReadOnly = true;
          
            //ForeignKeyConstraint foreignKey = new ForeignKeyConstraint(data.Tables["un_groups"].Columns[0], data.Tables["students"].Columns[2])
            //{
            //    ConstraintName = "PhonesCompaniesForeignKey",
            //    DeleteRule = Rule.SetNull,
            //    UpdateRule = Rule.Cascade
            //};
            //data.Tables["students"].Constraints.Add(foreignKey);
            //data.EnforceConstraints = true;
            //data.Relations.Add("student_group", data.Tables["un_groups"].Columns[0], data.Tables["students"].Columns[2]);


        }

        

        internal void AddPasswordEntry(PhraseInputStat cur_entry, int userid)
        {
            string t_vecktor = GetTVector(cur_entry.press_data);
            string tau_vecktor = GetTauVector(cur_entry.press_data);
            string date = cur_entry.Entry_time.ToString("yyyy-MM-dd hh:mm:ss");
            string speed = cur_entry.TypeSpeed.ToString("F4", new CultureInfo("en-US", false));
            string bio_vector = GetBioVector(cur_entry.bio_vecktor);
            string sqlExpression = string.Format(
                "INSERT INTO password_entries(userid, speed, total_time, entry_date, t_vecktor, tau_vecktor, type1_imp, type2_imp, type3_imp, bio_vector)" +
                " Values({0},{1:f4},{2},'{3}','{4}','{5}',{6},{7},{8},'{9}');",
                userid, speed, cur_entry.PressTime, date, t_vecktor, tau_vecktor,
                cur_entry.type1_impos, cur_entry.type2_impos, cur_entry.type3_impos, bio_vector);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            int i = command.ExecuteNonQuery();
            connection.Close();

        }

        internal int FindUser(PhraseInputStat entry)
        {
            int userid = 0;
            string speed = entry.TypeSpeed.ToString("F4", new CultureInfo("en-US", false));
            string sqlExpression = string.Format(
                "SELECT id FROM users WHERE password = '{0}' AND (ABS(expected_speed-{1})<0.5);"
                , entry.password, speed);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                userid = reader.GetInt32(0);
            }
            else
            {
                userid = -1;
            }
            connection.Close();
            return userid;
        }


        internal int VerifyUser(PhraseInputStat entry)
        {
            int code = 0;
            string sqlExpression = string.Format(
                "SELECT * FROM users WHERE login = '{0}'"
                ,entry.login);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                float exp_speed = reader.GetFloat(4);
                string password = reader.GetString(2);
                if ((Math.Abs(exp_speed - entry.TypeSpeed) < 0.5f) && (password == entry.password))
                    code = 1;
                else
                    code = -2;
            }
            else
            {
                code = -1;
            }
            connection.Close();
            return code;
        }

        internal int GetEntriesCount(int userid)
        {
            string sqlExpression = string.Format(
                "SELECT COUNT(id) FROM password_entries WHERE userid = {0}", userid);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            connection.Close();
            return count;
        }

        private string GetTauVector(List<KeyPressData> press_data)
        {
            string tau_vecktor = "";
            for(int i = 0; i< press_data.Count-1; i++)
            {
                tau_vecktor += string.Format("{0} ",
                    press_data[i + 1].key_down_time - press_data[i].key_up_time);
            }
            return tau_vecktor;
        }

        private string GetTVector(List<KeyPressData> press_data)
        {
            string t_vecktor = "";
            for (int i = 0; i < press_data.Count; i++)
            {
                t_vecktor += string.Format("{0} ",
                    press_data[i].key_up_time - press_data[i].key_down_time);
            }
            return t_vecktor;
        }

        private string GetBioVector(float[] vector)
        {
            string bio_vector = "";
            for (int i = 0; i < vector.Length; i++)
            {
                string point = vector[i].ToString("F4", new CultureInfo("en-US", false));
                bio_vector += string.Format("{0} ",
                    point);
            }
            return bio_vector;
        }

        private List<KeyPressData> DecodeVectors(int[] tau_vecktor, int[] t_vecktor)
        {
            List<KeyPressData> press_data = new List<KeyPressData>();
            int cur_time = 0;
            press_data.Clear();
            for (int i = 0; i< t_vecktor.Length; i++)
            {
                KeyPressData press = new KeyPressData();
                press.key_down_time = cur_time;
                press.key_up_time = cur_time + t_vecktor[i];
                press_data.Add(press);
                if (i == t_vecktor.Length-1)
                    continue;
                cur_time += t_vecktor[i] + tau_vecktor[i];
            }
            return press_data;
        }

        private int[] SplitIntString(string int_str)
        {
            string[] arr = int_str.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);
            int[] int_arr = new int[arr.Length];
            for(int i = 0; i< arr.Length; i++)
            {
                int_arr[i] = Convert.ToInt32(arr[i]);
            }
            return int_arr;
        }

        private float[] SplitFloatString(string int_str)
        {
            string[] arr = int_str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            float[] float_arr = new float[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                float_arr[i] = Convert.ToSingle(arr[i]);
            }
            return float_arr;
        }

        internal void RecountStat(int userid)
        {
            CultureInfo format = new CultureInfo("en-US", false);
            //getting exp_speed
            string sqlExpression = string.Format("SELECT AVG(speed) FROM password_entries WHERE userid = {0} ;",
                userid);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            float exp_speed = reader.GetFloat(0);
            reader.Close();
            //
            //getting dispersion
            string DispersionExpression = string.Format("SELECT AVG(POWER(speed - {0}, 2)) FROM password_entries WHERE userid = {1} ;",
                exp_speed.ToString("F4", format), userid);
            command = new MySqlCommand(DispersionExpression, connection);
            reader = command.ExecuteReader();
            reader.Read();
            float dispersion = reader.GetFloat(0);
            reader.Close();
            //
            //updating the database 
            string updateExpression = string.Format("UPDATE users SET expected_speed = {0}, dispersion = {1} WHERE id = {2}",
                exp_speed.ToString("F4", format), dispersion.ToString("F4", format), userid);
            MySqlCommand update_command = new MySqlCommand(updateExpression, connection);
            int i = update_command.ExecuteNonQuery();
            connection.Close();
        }

        internal List<PhraseInputStat> GetUserData(int userid)
        {
            string sqlExpression = string.Format("SELECT * FROM password_entries WHERE userid = {0} ;",
                userid);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<PhraseInputStat> data_sample = new List<PhraseInputStat>();
            while (reader.Read())
            {
                PhraseInputStat phrase_entry = new PhraseInputStat();
                int[] tau_vecktor = SplitIntString(reader.GetString(6));
                int[] t_vecktor = SplitIntString(reader.GetString(5));
                phrase_entry.press_data = DecodeVectors(tau_vecktor, t_vecktor);
                phrase_entry.Entry_time = reader.GetDateTime(4);
                phrase_entry.PressTime = reader.GetInt32(3);
                phrase_entry.TypeSpeed = reader.GetFloat(2);
                phrase_entry.type1_impos = reader.GetInt16(7);
                phrase_entry.type2_impos = reader.GetInt16(8);
                phrase_entry.type3_impos = reader.GetInt16(9);
                phrase_entry.bio_vecktor = SplitFloatString(reader.GetString(10));
                data_sample.Add(phrase_entry);
            }
            connection.Close();
            return data_sample;

        }

        internal int GetUser(string login, string password)
        {
            int userid;
            string sqlExpression = string.Format("SELECT id FROM users WHERE login = '{0}' AND password= '{1}' ;",
                login, password);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                userid = reader.GetInt32(0);
                if (reader.Read())
                {
                    userid = - 1;
                }
                
            }
            else
            {
                userid = -1;
            }
            connection.Close();
            return userid;
        }


        public void AddNewUser(string login, string passw, string complexity)
        {

            string sqlExpression = string.Format(
                "INSERT INTO users(login, password, complexity) Values('{0}','{1}','{2}');",
                login, passw, complexity);
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlExpression, connection);
            int i = command.ExecuteNonQuery();
            connection.Close();
            //DataRow row = data.Tables["users"].NewRow();
            //row["login"] = login;
            //row["password"] = passw;
            //row["complexity"] = complexity;
            //data.Tables["users"].Rows.Add(row);
        }


        public void AddNewUser(int userid, float speed, int total_time, DateTime entry_date, string t_vecktor,
            string tau_vecktor, int type1_imp, int type2_imp, int type3_imp)
        {
            DataRow row = data.Tables["password_entries"].NewRow();
            row["userid"] = userid;
            row["speed"] = speed;
            row["total_time"] = total_time;
            row["entry_date"] = entry_date;
            row["t_vecktor"] = t_vecktor;
            row["tau_vecktor"] = tau_vecktor;
            row["type1_imp"] = type1_imp;
            row["type2_imp"] = type2_imp;
            row["type3_imp"] = type3_imp;
            data.Tables["password_entries"].Rows.Add(row);
        }

        public void TestGroupsInsert()
        {
            DataRow row = data.Tables["un_groups"].NewRow();
            row["groupnum"] = data.Tables["un_groups"].Rows.Count;
            row["course_year"] = data.Tables["un_groups"].Rows.Count;
            data.Tables["un_groups"].Rows.Add(row);

        }
        public void TestUserInsert()
        {
            DataRow row = data.Tables["students"].NewRow();
            row["login"] = "posix";
            row["password"] = "passw";
            row["role"] = "student";
            data.Tables["users"].Rows.Add(row);
        }
    }
}
