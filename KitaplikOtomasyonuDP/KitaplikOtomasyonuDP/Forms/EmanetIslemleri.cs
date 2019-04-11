using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP.Emanetler
{
    public partial class EmanetIslemleri : Form
    {
        public EmanetIslemleri()
        {
            InitializeComponent();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void btnEmanetVer_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new EmanetlerRepository(uow);
                    
                    
                    repos.EmanetEkle(Convert.ToInt32(cmbUyeID.SelectedValue),Convert.ToInt32(cmbKitapID.SelectedValue),dtpEmanetVermeTerihi.Text);
                    List<Emanet> emanetler = repos.GetAll();
                    uow.SaveChanges();
                    dataGridView1.DataSource = emanetler;
                    
                }
                catch (Exception ex)
                {

                    //throw new Exception("Put your error message here.", ex);
                }

            }
        }

        private void EmanetIslemleri_Load(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos1 = new EmanetlerRepository(uow);
                    var repos2 = new Kitaplar.KitaplarRepository(uow);
                    var repos3 = new Uyeler.UyelerRepository(uow);

                    List<Uyeler.Uyeler> uyeler = repos3.GetAll();
                    List<Kitaplar.Kitaplar> kitaplar = repos2.GetAll();

                    List<Emanet> emanetler = repos1.GetAll();
                    
                    uow.SaveChanges();
                    dataGridView1.DataSource = emanetler;
                    /////kitaplar
                    cmbKitapID.DisplayMember = "KitapAdi";
                    cmbKitapID.ValueMember = "KID";

                    cmbKitapID.DataSource = kitaplar;
                    cmbKitapID.SelectedItem = null;
                    cmbKitapID.Text = "Seçiniz";
                    ///////uyeler
                    cmbUyeID.DisplayMember = "ToString()";
                    cmbUyeID.ValueMember = "UID";

                    cmbUyeID.DataSource = uyeler;
                    cmbUyeID.SelectedItem = null;
                    cmbUyeID.Text = "Seçiniz";

                }
                catch (Exception ex)
                {

                    throw new Exception("Put your error message here.", ex);
                }

            }
        }
       public static DataGridViewRow Satir;
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)//datagridview'ın herhangi birsatırın indisi olup olmadığını kontrol ediyoruz.

            {

                Satir = dataGridView1.Rows[e.RowIndex];//tıklanan satırıstatic olan bir satir tutucusuna atıyoruz.

                EmanetGeriTeslim emanetGeriTeslim  = new EmanetGeriTeslim();//detay formunu oluşturuyoruz.

                emanetGeriTeslim.ShowDialog();//show metodunu çağırıyoruz.bu sayede formu görünür yapıyoruz.

            }
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos1 = new EmanetlerRepository(uow);
                    
                    List<Emanet> emanetler = repos1.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = emanetler;
                    
                }
                catch (Exception ex)
                {

                    throw new Exception("Put your error message here.", ex);
                }

            }
        }
    }
}
