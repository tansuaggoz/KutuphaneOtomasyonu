using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP.Emanetler
{
    public class Emanet
    {
        public int EID { get; set; }
        public int UyeID { get; set; }
        public int KitapID { get; set; }
        public DateTime VermeTarihi { get; set; }
        public DateTime? GeriAlmaTarihi { get; set; }

        public string TeslimNot { get; set; }
        public string TeslimEdildiMi { get; set; }
    }
}
