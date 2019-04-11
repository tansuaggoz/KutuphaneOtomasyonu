using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitaplikOtomasyonuDP
{
    public partial class KitapIslemleri : Form
    {
        public KitapIslemleri()
        {
            InitializeComponent();
        }
        void TextleriBosalt()
        {
            txtAd.Text = "";
            txtBaskiYili.Text = "";
            txtSayfaSayisi.Text = "";
            txtAciklama.Text = "";
            cmbKategori.SelectedItem = null;
            cmbKategori.Text = "Seçiniz";
            cmbYayınevi.SelectedItem = null;
            cmbYayınevi.Text = "Seçiniz";
            cmbYazar.SelectedItem = null;
            cmbYazar.Text = "Seçiniz";
            cmbDili.Text = "Seçiniz";

        }
        private void KitapIslemleri_Load(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Kitaplar.KitaplarRepository(uow);

                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();
                    List<Kitaplar.Kategory> kategories = repos.GetAllKategory();
                    List<Kitaplar.Yayinevi> yayinevleri = repos.TumYayinevi();
                    List<Kitaplar.Yazarlar> yazarlar = repos.TumYazarlar();
                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;
                    //////kategori
                    BindKategory(kategories,cmbKategori);
                    BindKategory(kategories, cmbKategoriAra);
                    //////yayınevi

                    cmbYayınevi.DisplayMember = "YayıneviAdi";
                    cmbYayınevi.ValueMember = "ID";

                    cmbYayınevi.DataSource = yayinevleri;
                    cmbYayınevi.SelectedItem = null;
                    cmbYayınevi.Text = "Seçiniz";
                    ///////yazarlar
                    BindYazar(yazarlar,cmbYazar);
                    BindYazar(yazarlar, cmbYazarAra);
                }
                catch (Exception ex)
                {


                }

            }
        }
        void BindYazar(List<Kitaplar.Yazarlar> yazarlar, ComboBox cmbYazar)
        {
            cmbYazar.DisplayMember = "ToString()";
            cmbYazar.ValueMember = "ID";
            //cmbYazarAra.SelectedIndex = -1;
            cmbYazar.DataSource = yazarlar;
            cmbYazar.SelectedItem = null;
            cmbYazar.Text = "Seçiniz";
        }
        void BindKategory(List<Kitaplar.Kategory> kategories, ComboBox cmbKategori)
        {
            cmbKategori.DisplayMember = "KategoriAdi";
            cmbKategori.ValueMember = "ID";

            cmbKategori.DataSource = kategories;
            cmbKategori.SelectedItem = null;
            cmbKategori.Text = "Seçiniz";
        }
        public byte[] ImageByteArray(PictureBox pb)
        {
            MemoryStream st = new MemoryStream();
            
            pb.Image.Save(st, System.Drawing.Imaging.ImageFormat.Jpeg);
            return st.ToArray();
            
        }
        

        private void btnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtResim.Text = openFileDialog1.FileName;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Kitaplar.KitaplarRepository(uow);
                    //ArrayList arr = new ArrayList();
                    string[] stringArr = { txtAd.Text, cmbDili.SelectedItem.ToString(), txtAciklama.Text };
                    int[] intArr = { Convert.ToInt32(cmbKategori.SelectedValue), Convert.ToInt32(cmbYazar.SelectedValue), Convert.ToInt32(txtBaskiYili.Text), Convert.ToInt32(txtSayfaSayisi.Text), Convert.ToInt32(cmbYayınevi.SelectedValue) };
                    
                    //byte[] resim= ImageByteArray(pictureBox1);

                    repos.KitapEkle(stringArr,intArr, ImageByteArray(pictureBox1));
                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();
                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;
                    

                }
                catch (Exception ex)
                {

                    throw new Exception("Put your error message here.", ex);
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[secilen].Cells[2].FormattedValue.ToString();
            cmbYazar.Text = dataGridView1.Rows[secilen].Cells[3].FormattedValue.ToString();
            txtBaskiYili.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            cmbDili.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            cmbYayınevi.Text = dataGridView1.Rows[secilen].Cells[8].FormattedValue.ToString();
            txtAciklama.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();


            if (dataGridView1.Rows[secilen].Cells[7].Value.ToString() != "")
            {
                byte[] _byte = (byte[])dataGridView1.Rows[secilen].Cells[7].Value;
                MemoryStream ms = new MemoryStream(_byte);
                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                txtResim.Text = "";
            }
            else
                pictureBox1.Image = null;
        }
        
        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Kitaplar.KitaplarRepository(uow);

                    List<Kitaplar.Kitaplar> kitaplar = repos.KitapAra(txtKitapAdiAra.Text, Convert.ToInt32(cmbYazarAra.SelectedValue), Convert.ToInt32(cmbKategoriAra.SelectedValue));

                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;
                    txtKitapAdiAra.Text = "";
                    cmbKategoriAra.Text = "Seçiniz";
                    cmbYazarAra.Text = "Seçiniz";
                }
                catch (Exception ex)
                {


                }

            }
        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new Kitaplar.KitaplarRepository(uow);

                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;

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
                    var repos = new Kitaplar.KitaplarRepository(uow);
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    repos.Sil(ID);
                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();

                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;
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
                    var repos = new Kitaplar.KitaplarRepository(uow);
                    //ArrayList arr = new ArrayList();
                    string[] stringArr = { dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[6].Value.ToString(), dataGridView1.CurrentRow.Cells[9].Value.ToString() };
                    int[] intArr = { Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString()), Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString()), Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString()), Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value.ToString()), Convert.ToInt32(dataGridView1.CurrentRow.Cells[8].Value.ToString()), Convert.ToInt32(dataGridView1.CurrentRow.Cells[10].Value.ToString()) };

                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    repos.Guncelle(stringArr, intArr,ID);
                    List<Kitaplar.Kitaplar> kitaplar = repos.GetAll();
                    uow.SaveChanges();
                    dataGridView1.DataSource = kitaplar;


                }
                catch (Exception ex)
                {

                    //throw new Exception("Put your error message here.", ex);
                }

            }
        }
    }
}
