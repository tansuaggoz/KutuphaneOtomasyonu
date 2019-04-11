using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP.Kitaplar
{
    public class KitaplarRepository
    {
        private AdoNetUnitOfWork _unitOfWork;

        public KitaplarRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _unitOfWork = uow as AdoNetUnitOfWork;
            if (_unitOfWork == null)
                throw new NotSupportedException("Ohh my, change that UnitOfWorkFactory, will you?");
        }
        public List<Kitaplar> GetAll()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Kitaplar";


                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Kitaplar> kitaplar = new List<Kitaplar>();
                Kitaplar kitap = null;
                while (rd.Read())
                {
                    kitap = new Kitaplar
                    {
                        KID = rd.GetInt32(0),
                        KitapAdi = rd.GetString(1),
                        KategoriID = rd.GetInt32(2),
                        YazarID = rd.GetInt32(3),
                        BaskiYili = rd.GetInt32(4),
                        SayfaSayisi = rd.GetInt32(5),
                        KitapDili=rd.GetString(6),
                        Fotograf=(byte[])rd["Fotograf"],
                        YayıneviID=rd.GetInt32(8),
                        Aciklama=rd.GetString(9),
                        DurumID=rd.GetInt32(10)

                    };
                    kitaplar.Add(kitap);
                }
                rd.Close();

                return kitaplar;
            }
        }
        
        public List<Kategory> GetAllKategory()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM KitapKategorileri";


                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Kategory> kategories = new List<Kategory>();
                Kategory kategory = null;
                while (rd.Read())
                {
                    kategory = new Kategory
                    {
                        ID = rd.GetInt32(0),
                        KategoriAdi = rd.GetString(1)
                        
                    };
                    kategories.Add(kategory);
                }

                rd.Close();

                return kategories;
            }
        }
        public List<Yayinevi> TumYayinevi()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Yayınevleri";


                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Yayinevi> yayinevleri = new List<Yayinevi>();
                Yayinevi yayinevi = null;
                while (rd.Read())
                {
                    yayinevi = new Yayinevi
                    {
                        ID = rd.GetInt32(0),
                        YayıneviAdi = rd.GetString(1)

                    };
                    yayinevleri.Add(yayinevi);
                }

                rd.Close();

                return yayinevleri;
            }
        }
        public List<Yazarlar> TumYazarlar()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Yazarlar";


                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Yazarlar> yazarlar = new List<Yazarlar>();
                Yazarlar yazar = null;
                while (rd.Read())
                {
                    yazar = new Yazarlar
                    {
                        ID = rd.GetInt32(0),
                        YazarAdi = rd.GetString(1),
                        YazarSoyadi=rd.GetString(2)
                       
                    };
                    yazarlar.Add(yazar);
                }

                rd.Close();

                return yazarlar;
            }
        }
        public List<Kitaplar> KitapAra(string kitapAdi,int yazarID,int kategoryID)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = "SP_KitapAra" + " '" + kitapAdi + "'," + yazarID + ","+kategoryID;

                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                Kitaplar kitap = null;
                List<Kitaplar> kitaplar = new List<Kitaplar>();
                while (rd.Read())
                {
                    kitap = new Kitaplar
                    {
                        KID = rd.GetInt32(0),
                        KitapAdi = rd.GetString(1),
                        KategoriID = rd.GetInt32(2),
                        YazarID = rd.GetInt32(3),
                        BaskiYili = rd.GetInt32(4),
                        SayfaSayisi = rd.GetInt32(5),
                        KitapDili = rd.GetString(6),
                        Fotograf = (byte[])rd["Fotograf"],
                        YayıneviID = rd.GetInt32(8),
                        Aciklama = rd.GetString(9),
                        DurumID = rd.GetInt32(10)

                    };
                    kitaplar.Add(kitap);
                }
                rd.Close();

                return kitaplar;
            }
        }
        public void Sil(int ID)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Kitaplar WHERE KID=" + ID;
                cmd.ExecuteNonQuery();

            }
        }
        public void Guncelle(string[] arr1, int[] arr2,int ID)
        {
            if (MessageBox.Show("Seçili kayıtı güncellemek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = " UPDATE Kitaplar SET " + "KitapAdi='" + arr1[0] + "',KategoriID=" + arr2[0] + ",YazarID=" + arr2[1] + ",BaskiYili=" + arr2[2] + ",SayfaSayisi=" + arr2[3] + ",KitapDili='"+arr1[1]+ "',YayineviID="+arr2[4]+ ",Acıklama='"+arr1[2] +"',DurumID="+arr2[5]+ " Where KID=" + ID;
                    cmd.ExecuteNonQuery();
                }

            }
        }
        public void KitapEkle(string[] arr1, int[] arr2, byte[] sd )
        {
            //var foto = sd;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = " INSERT INTO Kitaplar (KitapAdi,KategoriID,YazarID,BaskiYili,SayfaSayisi,KitapDili,YayineviID,Acıklama,Fotograf) VALUES( " + "'" + arr1[0] + "'," + arr2[0] + "," + arr2[1] + "," + arr2[2] + "," + arr2[3] + ",'" + arr1[1] + "'," + arr2[4] + ",'" + arr1[2] + "', @p9)";

                
                var foto = new SqlParameter("@p9", SqlDbType.Image)
                {
                    Direction = ParameterDirection.Input,
                    Size = sd.Length,
                    Value = sd
                };
                
                cmd.Parameters.Add(foto);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
