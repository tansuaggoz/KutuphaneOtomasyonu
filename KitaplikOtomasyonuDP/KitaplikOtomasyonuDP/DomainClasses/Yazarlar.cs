using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP.Kitaplar
{
    public class Yazarlar
    {
        public int ID { get; set; }
        public string YazarAdi { get; set; }
        public string YazarSoyadi { get; set; }

        public override string ToString()
        {
            return YazarAdi + " " + YazarSoyadi;
        }
    }
}
