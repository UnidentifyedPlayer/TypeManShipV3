using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManShip
{
    public class PhraseInputStat
    {
        public string login;
        public string password;
        private float typing_speed;
        private long press_time;
        public List<KeyPressData> press_data;
        private DateTime entry_time;
        public int type1_impos;
        public int type2_impos;
        public int type3_impos;
        public float[] bio_vecktor;
        public float TypeSpeed
        {
            get
            {
                return typing_speed;
            }
            set
            {
                typing_speed = value;
            }
        }
        public long PressTime
        {
            get
            {
                return press_time;
            }
            set
            {
                press_time = value;
            }
        }

        public DateTime Entry_time
        {
            get
            {
                return entry_time;
            }

            set
            {
                entry_time = value;
            }
        }

        public PhraseInputStat()
        {
            press_data = new List<KeyPressData>();
            type1_impos = 0;
            type2_impos = 0;
            type3_impos = 0;
        }
        
    }
}
