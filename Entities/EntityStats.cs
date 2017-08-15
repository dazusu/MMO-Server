using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Entities
{
    public class EntityStats
    {
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Agi { get; set; }
        public int Vit { get; set; }
        public int Mnd { get; set; }
        public int Int { get; set; }
        public int Chr { get; set; }

        public EntityStats()
        {
            
        }

        public EntityStats(int str, int dex, int agi, int vit, int mnd, int intelligence, int chr)
        {
            Str = str;
            Dex = dex;
            Agi = agi;
            Vit = vit;
            Mnd = mnd;
            Int = intelligence;
            Chr = chr;
        }
    }
}
