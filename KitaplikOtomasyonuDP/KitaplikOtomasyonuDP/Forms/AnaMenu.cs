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
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void btnKitapIsl_Click(object sender, EventArgs e)
        {
            KitapIslemleri form = new KitapIslemleri();
            form.ShowDialog();
        }

        private void btnUyeIsl_Click(object sender, EventArgs e)
        {
            UyeIslemleri uyeIslemleri = new UyeIslemleri();
            uyeIslemleri.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void btnEmanetIslemleri_Click(object sender, EventArgs e)
        {
            Emanetler.EmanetIslemleri emanetIslemleri = new Emanetler.EmanetIslemleri();
            emanetIslemleri.ShowDialog();
        }

        private void btnEmanetKitaplar_Click(object sender, EventArgs e)
        {
            Forms.EmanettekiKitaplar emanettekiKitaplar = new Forms.EmanettekiKitaplar();
            emanettekiKitaplar.ShowDialog();
        }
    }
}
