using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP.Forms
{
    public partial class EmanettekiKitaplar : Form
    {
        public EmanettekiKitaplar()
        {
            InitializeComponent();
        }

        private void EmanettekiKitaplar_Load(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Kitaplar.KitaplarRepository(uow);
                    var repos2 = new Emanetler.EmanetlerRepository(uow);
                    List<Emanetler.Emanet> emanetler = repos2.GetAll();

                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;
                    foreach (var item in emanetler)
                    {
                        if(item.TeslimEdildiMi=="Hayır")
                        {
                            TimeSpan kalangun = DateTime.Today - item.VermeTarihi;//Sonucu zaman olarak döndürür
                            double toplamGun = kalangun.TotalDays;// kalanGun den TotalDays ile sadece toplam gun değerini çekiyoruz. 
                            if (toplamGun>15)
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == item.KitapID)
                                    {
                                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    }
                                }
                            }
                            else if(toplamGun>11 && toplamGun<15)
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == item.KitapID)
                                    {
                                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) == item.KitapID)
                                    {
                                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                    }
                                }
                            }
                        }
                    }
                   
                    
                }
                catch (Exception ex)
                {


                }

            }
        }
    }
}
