using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeManShip
{
    class LogController
    {
        Log Presses_log;
        Log Error_log;

        public LogController(string logs_path)
        {
            InitLogs(logs_path);
        }

        public void InitLogs(string logs_path)
        {
            if (!Directory.Exists(logs_path))
            {
                Directory.CreateDirectory(logs_path);
            }
            string press_log_path = Path.Combine(logs_path, "Press_logs");
            if (!Directory.Exists(press_log_path))
            {
                Directory.CreateDirectory(press_log_path);
            }
            string error_log_path = Path.Combine(logs_path, "Error_logs");
            if (!Directory.Exists(error_log_path))
            {
                Directory.CreateDirectory(logs_path);
            }
            Presses_log = new Log(press_log_path);
            Error_log = new Log(error_log_path);
        }

        public void NewSession(string str)
        {
            Presses_log.Space();
            Presses_log.Write(string.Format("New User session: {0} .", str));
        }
        public void Press(Keys key, long time)
        {
            string str = string.Format("Key {0} was pressed, {1} press time", key.ToString(), time);
            Presses_log.Write(str);
        }
        public void Release(Keys key, long time)
        {
            string str = string.Format("Key {0} was released, {1} release time", key.ToString(), time);
            Presses_log.Write(str);
        }
        public void Enter(bool success)
        {
            string status = success ? "success" : "failure";
            string str = string.Format("{0} : attempt to login", status);
            Presses_log.Write(str);
        }
        public void Erase()
        {
            string str = "the password field was cleared";
            Presses_log.Write(str);
        }
    }
}
