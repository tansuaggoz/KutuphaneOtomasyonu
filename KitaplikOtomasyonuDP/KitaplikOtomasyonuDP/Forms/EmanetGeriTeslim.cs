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
    public partial class EmanetGeriTeslim : Form
    {
        public EmanetGeriTeslim()
        {
            InitializeComponent();
        }
        int ID;
        private void EmanetGeriTeslim_Load(object sender, EventArgs e)
        {
            txtTeslimDurum.ReadOnly = true;
            DataGridViewRow satir2 = KitaplikOtomasyonuDP.Emanetler.EmanetIslemleri.Satir;
            ID = Convert.ToInt32(satir2.Cells[0].Value);
            txtUyeID.Text = satir2.Cells[1].Value.ToString();
            txtKitapID.Text = satir2.Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DialogResult sonuc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (sonuc == DialogResult.Yes)
            //{
                this.Close();

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var uow = (AdoNetUnitOfWork)UnitOfWorkFactory.Create(UnitOfWorkFactory.connectiontype.SQL))
            {
                try
                {
                    var repos = new EmanetlerRepository(uow);


                    repos.EmanetGeriAl(dateTimePicker1.Text,cmbKitapDurum.SelectedItem.ToString(),txtTeslimDurum.Text,ID);
                   
                    uow.SaveChanges();

                    this.Close();

                }
                catch (Exception ex)
                {

                    throw new Exception("Put your error message here.", ex);
                }

            }
        }
    }
}
