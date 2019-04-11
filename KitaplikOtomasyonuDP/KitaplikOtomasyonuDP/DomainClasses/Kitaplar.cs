using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP.Kitaplar
{
    public class Kitaplar
    {
        public int KID { get; set; }
        public string KitapAdi { get; set; }
        public int KategoriID { get; set; }
        public int YazarID { get; set; }
        public int BaskiYili { get; set; }
        public int SayfaSayisi { get; set; }
        public string KitapDili { get; set; }
        public byte[] Fotograf { get; set; }
        public int YayıneviID { get; set; }
        public string Aciklama { get; set; }
        public int DurumID { get; set; }
    }
}
