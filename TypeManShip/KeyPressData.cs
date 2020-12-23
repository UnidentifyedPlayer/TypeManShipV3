using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeManShip
{
    public class KeyPressData
    {
        public int phrase_key_pos;
        public long key_down_time;
        public long key_up_time;
        public Keys key;
        public long Interval
        {
            get
            {
                return key_up_time - key_down_time;
            }
        }
    }
}
