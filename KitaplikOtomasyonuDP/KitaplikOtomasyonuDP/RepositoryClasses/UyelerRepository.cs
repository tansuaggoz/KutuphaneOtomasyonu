using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP.Uyeler
{
    public class UyelerRepository
    {
        private AdoNetUnitOfWork _unitOfWork;

        public UyelerRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _unitOfWork = uow as AdoNetUnitOfWork;
            if (_unitOfWork == null)
                throw new NotSupportedException("Ohh my, change that UnitOfWorkFactory, will you?");
        }
        public List<Uyeler> Ara(string ad, string ePosta)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = "Exec SP_UyeAra" + " '" + ad + "','" + ePosta + "'";

                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                Uyeler uye = null;
                List<Uyeler> uyeler = new List<Uyeler>();
                while (rd.Read())
                {
                    uye = new Uyeler
                    {
                        UID = rd.GetInt32(0),
                        Adi = rd.GetString(1),
                        Soyadi = rd.GetString(2),
                        Telefon = rd.GetString(3),
                        Eposta = rd.GetString(4),
                        Adres = rd.GetString(5)

                    };
                    uyeler.Add(uye);
                }
                rd.Close();

                return uyeler;
            }
        }
        public List<Uyeler> GetAll()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Uyeler";


                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Uyeler> uyeler = new List<Uyeler>();
                Uyeler uye = null;
                while (rd.Read())
                {
                    uye = new Uyeler
                    {
                        UID = rd.GetInt32(0),
                        Adi = rd.GetString(1),
                        Soyadi = rd.GetString(2),
                        Telefon = rd.GetString(3),
                        Eposta = rd.GetString(4),
                        Adres = rd.GetString(5)

                    };
                    uyeler.Add(uye);
                }

                rd.Close();

                return uyeler;
            }
        }
        public void Sil(int ID)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Uyeler WHERE UID=" + ID;
                cmd.ExecuteNonQuery();

            }
        }
        public void Ekle(string[] degerler)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = " exec SP_UyeEkle " + "'" + degerler[0] + "','" + degerler[1] + "','" + degerler[2] + "','" + degerler[3] + "','" + degerler[4] + "'";
                cmd.ExecuteNonQuery();
            }
        }
        public void Guncelle(string[] degerler,int ID)
        {
            if (MessageBox.Show("Seçili kayıtı güncellemek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = " UPDATE Uyeler SET " + "Adi='" + degerler[0] + "',Soyadi='" + degerler[1] + "',Telefon='" + degerler[2] + "',Eposta='" + degerler[3] + "',Adres='" + degerler[4] + "'"+"Where UID="+ID;
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}
