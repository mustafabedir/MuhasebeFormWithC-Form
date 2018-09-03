using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuhasebeFORM
{
    public partial class Index : MetroForm
    {
        public int id;
        public int sirketid;
        public int cariid;
        public Index()
        {
            InitializeComponent();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            showMusteri();
            showCari();
            showSirket();
            showTahsilat();
            showGelirGider();
            /*db.musteriDoldur();
            cmbMusteri.BackColor = Color.White;
            
            foreach(object musteriler in db.musteriList)
            {
                cmbMusteri.Items.Add(musteriler);
            }*/
            cmbMusteri.DataSource = db.musteriIdAl().DefaultView;
            cmbMusteri.DisplayMember = "ad";
            cmbMusteri.ValueMember = "musteriid";



        }

        public void showGelirGider()
        {
            MySqlDB db = new MySqlDB();
            db.allGelirlerGiderler();
            gelirgiderdatagrid.DataSource = db.dt;
        }

        public void showCari()
        {
            MySqlDB db = new MySqlDB();
            db.allCari();
            cariDataGrid.DataSource = db.dt;
        }

        public void showTahsilat()
        {
            MySqlDB db = new MySqlDB();
            db.allTahsilat();
            tahsilatDataGrid.DataSource = db.dt;
        }


        public void showMusteri()
        {
            MySqlDB db = new MySqlDB();
            db.allMusteri();
            dataGridView1.DataSource = db.dt;
        }

        public void showSirket()
        {
            MySqlDB db = new MySqlDB();
            db.allSirket();
            sirketDataGrid.DataSource = db.dt;
        }

        

        private void btnKaydetMusteri_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected)
            {
                MySqlDB db = new MySqlDB();

                MessageBox.Show(id.ToString());
                db.updateMusteri(id, txtTCKN.Text, txtAdSoyad.Text, txtTelefon.Text, txtEmail.Text);
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Müşteri Düzenlendi");
                }
                else
                    MessageBox.Show("Hata Oluştu Düzenlede");
            }
            else
            {
                MySqlDB db = new MySqlDB();
                db.addMusteri(txtTCKN.Text, txtAdSoyad.Text, txtTelefon.Text, txtEmail.Text);
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Müşteri Eklendi");
                }
                else
                    MessageBox.Show("Hata Oluştu");
            }

            showMusteri();
        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow.Selected)
            {
                txtTCKN.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            db.deleteMusteri(id);

            showMusteri();


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnCariEkle_Click(object sender, EventArgs e)
        {
            int musteriid = Convert.ToInt32(cmbMusteri.SelectedValue);
            MessageBox.Show(musteriid.ToString());

            if (cariDataGrid.CurrentRow.Selected)
            {
                MySqlDB db = new MySqlDB();
                
                MessageBox.Show(id.ToString());
                db.updateCari(id, txtHizmetAdi.Text, txtCariAciklama.Text, txtTarih.Text, Convert.ToInt32(txtTutar.Text), musteriid);
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Müşteri Düzenlendi");
                }
                else
                {
                    MessageBox.Show(db.hataMesaji);
                    
                }
            }

            else
            {
                MySqlDB db = new MySqlDB();
                db.addCari(txtHizmetAdi.Text, txtCariAciklama.Text, txtTarih.Text, Convert.ToInt16(txtTutar.Text), musteriid);
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Cari Eklendi");

                }
                else
                    MessageBox.Show(db.hataMesaji);
            }

            showCari();

        }

        private void cmbMusteri_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cariDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (cariDataGrid.CurrentRow.Selected)
            {
                txtHizmetAdi.Text = cariDataGrid.CurrentRow.Cells[1].Value.ToString();
                txtCariAciklama.Text = cariDataGrid.CurrentRow.Cells[2].Value.ToString();
                txtTarih.Text = cariDataGrid.CurrentRow.Cells[3].Value.ToString();
                txtTutar.Text = cariDataGrid.CurrentRow.Cells[4].Value.ToString();
                id = Convert.ToInt32(cariDataGrid.CurrentRow.Cells[0].Value.ToString());
            }
        }
       

        private void btnCariSil_Click(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            db.deleteCari(id);

            showCari();
        }

        private void btnSirketKaydet_Click(object sender, EventArgs e)
        {
            if (sirketDataGrid.CurrentRow.Selected)
            {
                MySqlDB db = new MySqlDB();

                MessageBox.Show(sirketid.ToString());
                db.updateSirket(sirketid, txtIslemAd.Text, txtSirketAciklama.Text, txtSirketTutar.Text);
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Sirket Düzenlendi");
                }
                else
                    MessageBox.Show(db.hataMesaji);
            }

            else
            {

                MySqlDB db = new MySqlDB();
                db.addSirket(txtIslemAd.Text, txtSirketAciklama.Text, Convert.ToInt16(txtSirketTutar.Text));
                if (db.mesaj == true)
                {
                    MessageBox.Show("Başarıyla Sirket Eklendi");
                }
                else
                    MessageBox.Show("Hata Oluştu");

            }
            showSirket();
        }

        private void sirketDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (sirketDataGrid.CurrentRow.Selected)
            {
                txtIslemAd.Text = sirketDataGrid.CurrentRow.Cells[1].Value.ToString();
                txtSirketAciklama.Text = sirketDataGrid.CurrentRow.Cells[2].Value.ToString();
                txtSirketTutar.Text = sirketDataGrid.CurrentRow.Cells[3].Value.ToString();
                sirketid = Convert.ToInt32(sirketDataGrid.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void btnSirketSil_Click(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            db.deleteSirket(sirketid);

            showSirket();
        }

        private void btnOde_Click(object sender, EventArgs e)
        {
           
        }

        private void btnOde_Click_1(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            db.cariid = id;
            db.addTahsilat(Convert.ToInt32(txtTutar.Text), txtCariAciklama.Text, id);
            MessageBox.Show(txtTutar.Text);
            MessageBox.Show(txtCariAciklama.Text);
            MessageBox.Show(id.ToString());
            showCari();
            showTahsilat();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
    }
}
