using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace TypeManShip
{
    public enum Password_Q
    {
        Bad = -1,
        Normal = 0,
        Good = 1,
        Great = 2,
       Safe = 3
    }
    public class MainController
    {
        static int EntriesCountThreshold = 5;
        static float[,] H = new float[,] {
                { 1,  1, 1,  1 , 1 ,  1 , 1 , 1 },
                { 1,  1, 1,  1, -1,  -1, -1, -1 },
                { (float)Math.Sqrt(2), (float)Math.Sqrt(2), -(float)Math.Sqrt(2), -(float)Math.Sqrt(2) , 0 , 0 , 0 , 0 },
                { 0,  0, 0,  0, (float)Math.Sqrt(2), (float)Math.Sqrt(2), -(float)Math.Sqrt(2), -(float)Math.Sqrt(2) },
                { 2, -2, 0,  0,  0 ,  0 , 0 , 0 },
                { 0,  0, 2, -2,  0,   0,  0,  0 },
                { 0,  0, 0,  0,  2 , -2 , 0,  0 },
                { 0,  0, 0,  0,  0 ,  0 , 2, -2 }
            };
        public DBController DataControl;
        LogController log_manager;
        private string password;
        private Keys last_key;
        private long last_key_time;
        private float expected_speed;
        private float dispersion;
        Dictionary<Keys, KeyPressData> pressed_keys;
        List<PhraseInputStat> data_samples;
        Stopwatch timer;
        private PhraseInputStat cur_entry;
        private List<float> day_excpected_speed;
        int press_count;
        public string error;


        static Keys[] special_keys = { Keys.Back };

        public PhraseInputStat CurrentPhraseStat
        {
            get
            {
                return cur_entry;
            }
        }



        public float Expected_speed
        {
            get
            {
                return expected_speed;
            }

            set
            {
                expected_speed = value;
            }
        }

        public float Dispersion
        {
            get
            {
                return dispersion;
            }

            set
            {
                dispersion = value;
            }
        }

        public bool RegisterUser(string login, string passw, string complexity)
        {
            if (login.Length > 40)
            {
                error = "Слишком длинный логин";
                return false;
            }
            if (passw.Length > 40)
            {
                error = "Слишком длинный пароль";
                return false;
            }
            DataControl.AddNewUser(login, passw, complexity);
            log_manager.NewSession(passw);
            return true;
        }

        public MainController()
        {
            pressed_keys = new Dictionary<Keys, KeyPressData>();
            data_samples = new List<PhraseInputStat>();
            timer = new Stopwatch();
            log_manager = new LogController(Directory.GetCurrentDirectory());
            expected_speed = 0;
            password = "";
            last_key = 0;
            press_count = 0;
            DataControl = new DBController();
            cur_entry = new PhraseInputStat();
        }

        public bool SetPassword(string passw)
        {
            password = passw;
            cur_entry = new PhraseInputStat();
            //DataControl.AddNewUser(passw);
            log_manager.NewSession(passw);
            return true;
        }

        public void Key_Down(Keys key, string text)
        {
            last_key = key;
            last_key_time = timer.ElapsedMilliseconds;
            //long timestamp = timer.ElapsedMilliseconds;
            //if (!pressed_keys.ContainsKey(key))
            //{
            //    KeyPressData new_press = new KeyPressData();
            //    press_count++;
            //    new_press.key_down_time = timestamp;
            //    new_press.phrase_key_pos = press_count;
            //    pressed_keys.Add(key, new_press);
            //    if (timer.IsRunning)
            //    {
            //        cur_entry.PressTime = timestamp;
            //    }
            //    else
            //    {
            //        timer.Start();
            //    }

            //}
        }

        public void Text_changed()
        {
            if (!pressed_keys.ContainsKey(last_key)&&(last_key!=Keys.None))
            {
                KeyPressData new_press = new KeyPressData();
                press_count++;
                new_press.key_down_time = last_key_time;
                new_press.phrase_key_pos = press_count;
                new_press.key = last_key;
                if (timer.IsRunning)
                {
                    cur_entry.PressTime = last_key_time;
                }
                else
                {
                    timer.Start();
                }
                log_manager.Press(last_key, last_key_time);
                if(pressed_keys.Count != 0)
                {
                    foreach(KeyValuePair<Keys, KeyPressData> bar in pressed_keys)
                    {
                        if (bar.Value.key_down_time < new_press.key_down_time)
                            cur_entry.type1_impos++;
                    }
                }
                pressed_keys.Add(last_key, new_press);

            }
            
        }

        public void Key_Up(Keys key)
        {

            if (pressed_keys.ContainsKey(key))
            {
                pressed_keys[key].key_up_time = timer.ElapsedMilliseconds;
                KeyPressData released_key = pressed_keys[key];
                cur_entry.press_data.Add(released_key);
                pressed_keys.Remove(key);
                if (pressed_keys.Count != 0)
                {
                    foreach (KeyValuePair<Keys, KeyPressData> bar in pressed_keys)
                    {
                        if (bar.Value.key_down_time > released_key.key_down_time)
                            cur_entry.type2_impos++;
                        else
                        {
                            cur_entry.type1_impos--;
                            cur_entry.type3_impos++;
                        }
                    }
                }
                log_manager.Release(key, released_key.key_up_time);
                
            }
        }

        public bool Enter_button(string entered_phrase, string login)
        {
            cur_entry.login = login;
            cur_entry.password = entered_phrase;
            timer.Stop();
            ForceKeysRelease();
            float speed = (float)press_count / ((float)cur_entry.PressTime / 1000);
            timer.Reset();
            cur_entry.TypeSpeed = speed;
            cur_entry.Entry_time = DateTime.Now;
            press_count = 0;
            last_key = 0;
            last_key_time = 0;
            int userid = GetUser(cur_entry.login, cur_entry.password);
            if (userid != -1)
            {
                BioVecktor();
                DataControl.AddPasswordEntry(cur_entry, userid);
                DataControl.RecountStat(userid);
                //CountExcpectedSpeed();
                log_manager.Enter(true);
                return true;
            }
            log_manager.Enter(false);
            return false;
        }


        public int GetUser(string login, string password)
        {
            int userid = DataControl.GetUser(login, password);
            if (userid == -1)
                error = "no user found";
            else
            {
                
                this.password = password;
            }
            return userid;
        }

        internal int GetUser(string password)
        {
            cur_entry.password = password;
            timer.Stop();
            ForceKeysRelease();
            float speed = (float)press_count / ((float)cur_entry.PressTime / 1000);
            timer.Reset();
            cur_entry.TypeSpeed = speed;
            cur_entry.Entry_time = DateTime.Now;
            press_count = 0;
            last_key = 0;
            last_key_time = 0;
            int userid = DataControl.FindUser(cur_entry);
            if (userid == -1)
                error = "Пользователь с такими данными не найден";
            return userid;
        }

        internal bool VerifyUser(string login, string password)
        {
            cur_entry.login = login;
            cur_entry.password = password;
            timer.Stop();
            ForceKeysRelease();
            float speed = (float)press_count / ((float)cur_entry.PressTime / 1000);
            timer.Reset();
            cur_entry.TypeSpeed = speed;
            cur_entry.Entry_time = DateTime.Now;
            press_count = 0;
            last_key = 0;
            last_key_time = 0;
            int code = DataControl.VerifyUser(cur_entry);
            if (code != -1)
            {
                error = "Нет записи с таким логином";
            }
            if(code == -2)
            {
                error = "Пользователь не прошёл проверку.";
            }
            if(code == 1)
            {
                return true;
            }
            return false;
        }

        public bool LoadUserData(int userid)
        {
            if (DataControl.GetEntriesCount(userid) < EntriesCountThreshold)
            {
                userid = -1;
                error = "Недостаточно данных для построения статистики";
                return false;
            }
            data_samples = DataControl.GetUserData(userid);
            CountExcpectedSpeed();
            CountDispersion();
            return true;
        }

        private void ForceKeysRelease()
        {
            Keys[] keys = new Keys[pressed_keys.Count];
            pressed_keys.Keys.CopyTo(keys,0);
            foreach (Keys key in keys)
            {
                Key_Up(key);
            }
        }

        private void CountExcpectedSpeed()
        {
            float temp = 0;
            for (int i = 0; i < data_samples.Count; i++)
            {
                temp += data_samples[i].TypeSpeed;
            }
            temp = temp / data_samples.Count;
            expected_speed = temp;
        }

        private void CountDispersion()
        {
            float temp = 0;
            float megatemp = 0;
            for (int i = 0; i < data_samples.Count; i++)
            {
                megatemp = (data_samples[i].TypeSpeed - expected_speed);
                temp += megatemp * megatemp;
            }
            temp = temp / data_samples.Count;
            dispersion = temp;
        }

        public Password_Q RatePassword(string passw)
        {
            int rating = 0;
            bool numbool = false;
            bool has_upper = false;
            if(passw.Length < 4)
            {
                return Password_Q.Bad;
            }
            else if(passw.Length > 8)
            {
                rating++;
            }

            for (int i = 0; i < passw.Length; i++)
            {
                if (Char.IsUpper(passw[i])) has_upper = true;
                if (Char.IsDigit(passw[i])) has_upper = true;
            }
            rating += (numbool ? 1 : 0) + (has_upper ? 1 : 0);
            return (Password_Q)rating;
        }

        public void Reset()
        {
            press_count = 0;
            password = "";
            this.ClearCurEntry();
            log_manager.Erase();
        }

        public void ClearCurEntry()
        {
            timer.Reset();
            pressed_keys.Clear();
            cur_entry = new PhraseInputStat();
        }

        public float[] DiscreteTimeFunc()
        {
            float time_stamp = cur_entry.PressTime / 8;
            float[] func_values = new float[8];
            float amp = 0.5f;
            for(int i = 0; i<8; i++)
            {
                float time = time_stamp * i;
                int count = 0;
                for(int z = 0; z < cur_entry.press_data.Count; z++)
                {
                    if ((cur_entry.press_data[z].key_down_time <= z)
                        && (cur_entry.press_data[z].key_up_time >= z))
                        count++;
                }
                if (count > 1)
                    func_values[i] = amp * 2;
                else
                    func_values[i] = amp * count;
            }
            return func_values;
        }

        public void BioVecktor()
        {
            float[] func = DiscreteTimeFunc();
            cur_entry.bio_vecktor = new float[8];
            for(int i = 0;i<8; i++)
            {
                cur_entry.bio_vecktor[i] = 0;
                for (int z = 0; z<8; z++)
                {
                    cur_entry.bio_vecktor[i] += func[z] * H[i, z];
                }
                cur_entry.bio_vecktor[i] = cur_entry.bio_vecktor[i] / 8;
            }
        }

        public List<HistogramBar> PrepareForChart()
        { 
            if(data_samples.Count < 5)
            {
                return new List<HistogramBar>();
            }
            int sectors = 9;
            float min_speed = data_samples.Min(data => data.TypeSpeed);
            float max_speed = data_samples.Max(data => data.TypeSpeed);
            float d = (max_speed - min_speed) / sectors;
            List<HistogramBar> distro = new List<HistogramBar>(11);

            for(int i = 0; i < sectors; i++)
            {
                distro.Add(new HistogramBar());
                distro[i].speed = Math.Round( min_speed + d*i - d / 2,3).ToString(); 
            }
            for (int i = 0; i < data_samples.Count; i++)
            {
                float speed = data_samples[i].TypeSpeed;
                if (speed == max_speed)
                {
                    distro[sectors-1].number++;

                }
                else
                {
                    int remainder = (int)Math.Floor((speed - min_speed) / d);
                    distro[remainder].number++;
                }
                

            }
            return distro;
        }

       
        //убрать password отсюда
        public List<long> PreapareDynType()
        {
            if (data_samples.Count < 5)
            {
                return new List<long>();
            }
            List<long> dynamic_of_type = new List<long>(password.Length-1);
            for (int i = 0; i < password.Length - 1; i++)
            {
                dynamic_of_type.Add(0);
            }
            for(int i = 0; i< data_samples.Count; i++)
            {
                for (int t = 0; t < password.Length - 1; t++)
                {
                    long press_interval = data_samples[i].press_data[t + 1].key_down_time - data_samples[i].press_data[t].key_down_time;
                    dynamic_of_type[t] += press_interval ;
                }
            }
            for (int t = 0; t < password.Length - 1; t++)
            {
                dynamic_of_type[t] = (long)(dynamic_of_type[t] / data_samples.Count);
            }
            return dynamic_of_type;
        }
        public List<long> PreapareDynHold()
        {
            if (data_samples.Count < 5)
            {
                return new List<long>();
            }
            List<long> dynamic_of_hold = new List<long>(password.Length);
            for (int t = 0; t < password.Length; t++)
            {
                dynamic_of_hold.Add(0);
            }
            for (int i = 0; i < data_samples.Count; i++)
            {
                for (int t = 0; t < password.Length; t++)
                {
                    long press_interval = data_samples[i].press_data[t].Interval;
                    dynamic_of_hold[t] += press_interval;
                }
            }
            for (int t = 0; t < password.Length; t++)
            {
                dynamic_of_hold[t] = (long)(dynamic_of_hold[t] / data_samples.Count);
            }
            return dynamic_of_hold;
        }

        public Dictionary<string, float> PrepareDayExpChart()
        {
            if (data_samples.Count < 5)
            {
                return new Dictionary<string, float>();
            }
            List<float> day_chart = new List<float>();
            List<int> entries_count = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                day_chart.Add(0);
                entries_count.Add(0);
            }

            for(int i = 0; i<data_samples.Count; i++)
            {
                int num = -1;
                int hours = data_samples[i].Entry_time.TimeOfDay.Hours;
                if ((hours >= 4)  &&  (hours < 10)){ num = 0; }
                if ((hours >= 10) && (hours < 16)) { num = 1; }
                if ((hours >= 16) && (hours < 22)) { num = 2; }
                if ((hours >= 22) || (hours < 4))  { num = 3; }
                day_chart[num]+=data_samples[i].TypeSpeed;
                entries_count[num]++;
            }
            for (int i = 0; i < 4; i++)
            {
                day_chart[i] = day_chart[i] / entries_count[i];
            }
            day_excpected_speed = day_chart;

            Dictionary<string, float> day_chart_bars = new Dictionary<string, float>();
            day_chart_bars.Add("04:00-10:00", day_chart[0]);
            day_chart_bars.Add("10:00-16:00", day_chart[1]);
            day_chart_bars.Add("16:00-22:00", day_chart[2]);
            day_chart_bars.Add("22:00-4:00", day_chart[3]);
            return day_chart_bars;
        }

        public Dictionary<string, float> PrepareDayDisChart()
        {
            if (data_samples.Count < 5)
            {
                return new Dictionary<string, float>();
            }
            List<float> day_exp_chart = new List<float>();
            List<int> entries_count = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                day_exp_chart.Add(0);
                entries_count.Add(0);
            }
            for (int i = 0; i < data_samples.Count; i++)
            {
                int num = -1;
                int hours = data_samples[i].Entry_time.TimeOfDay.Hours;
                if ((hours >= 4) && (hours < 10)) { num = 0; }
                if ((hours >= 10) && (hours < 16)) { num = 1; }
                if ((hours >= 16) && (hours < 22)) { num = 2; }
                if ((hours >= 22) || (hours < 4)) { num = 3; }
                float dif = data_samples[i].TypeSpeed - day_excpected_speed[num];
                day_exp_chart[num]+= (dif*dif);
                entries_count[num]++;
            }
            for (int i = 0; i < 4; i++)
            {
                day_exp_chart[i] = day_exp_chart[i] / entries_count[i];
            }
            Dictionary<string, float> day_exp_chart_bars = new Dictionary<string, float>();
            day_exp_chart_bars.Add("04:00-10:00", day_exp_chart[0]);
            day_exp_chart_bars.Add("10:00-16:00", day_exp_chart[1]);
            day_exp_chart_bars.Add("16:00-22:00", day_exp_chart[2]);
            day_exp_chart_bars.Add("22:00-4:00", day_exp_chart[3]);
            return day_exp_chart_bars;
        }

    }
    
    
}
