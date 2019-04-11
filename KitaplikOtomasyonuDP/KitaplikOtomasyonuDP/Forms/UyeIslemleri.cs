using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP
{
    public partial class UyeIslemleri : Form
    {
        public UyeIslemleri()
        {
            InitializeComponent();
        }
        void TextleriBosalt()
        {
            txtAdi.Text = "";
            txtSoyad.Text = "";
            txtTelefon.Text = "";
            txtEpostaa.Text = "";
            txtAdres.Text = "";
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);

                   List<Uyeler.Uyeler> uyeler = repos.Ara(txtAd.Text,txtEposta.Text);
                    
                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;
                    txtAd.Text = "";
                    txtEposta.Text = "";
                }
                catch (Exception ex)
                {


                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);

                   List<Uyeler.Uyeler> uyeler = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;
                    
                }
                catch (Exception ex)
                {


                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAdi.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtEpostaa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtAdres.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);

                    List<Uyeler.Uyeler> uyeler = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;

                }
                catch (Exception ex)
                {


                }

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    repos.Sil(ID);
                    List<Uyeler.Uyeler> uyeler = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;
                    TextleriBosalt();
                    
                }
                catch (Exception ex)
                {


                }

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);
                    string[] degerler = {txtAdi.Text,txtSoyad.Text,txtTelefon.Text,txtEpostaa.Text,txtAdres.Text };
                    
                    repos.Ekle(degerler);
                    List<Uyeler.Uyeler> uyeler = repos.GetAll();
                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;
                    TextleriBosalt();

                }
                catch (Exception ex)
                {


                }

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Uyeler.UyelerRepository(uow);
                    string[] degerler =new string[5];
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    degerler[0] = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    degerler[1] = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    degerler[2] = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    degerler[3] = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    degerler[4] = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                    repos.Guncelle(degerler,ID);
                    List<Uyeler.Uyeler> uyeler = repos.GetAll();
                    uow.SaveChanges();
                    dataGridView1.DataSource = uyeler;
                    TextleriBosalt();

                }
                catch (Exception ex)
                {


                }

            }
            //string secili = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //string sec = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                this.Close();

            }
        }
    }
}
