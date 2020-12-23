using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace TypeManShip
{
    class Log
    {
        private static object sync = new object();
        private string filename;

        public Log(string newlogpath)
        {
            int i = 1;
            bool exists = true;
            string newfilename = "";
            while (exists)
            {
                newfilename = Path.Combine(newlogpath, string.Format("{0}_{1:dd.MM.yyy}_{1:HH_mm}_{2}.log",
                "Log", DateTime.Now, i));
                if (!File.Exists(newfilename))
                {
                    exists = false;
                }
                else
                {
                    i++;
                }
            }
            filename = newfilename;
            
        }
        public void Write(Exception ex)
        {
            try
            {
                // Путь .\\Log
                //string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                //string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyyy}_{1:t}.log",
                //AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}.{2}()] {3}\r\n",
                DateTime.Now, ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
        }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
        public void Write(string str)
        {
            try
            {
                string fullText = string.Format("[{0:dd.MM.yyy HH_mm_ss_fff}] {1}\r\n",
                DateTime.Now, str);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
        public void Space()
        {
            try
            {
                string fullText = "\n";
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
    }
}
