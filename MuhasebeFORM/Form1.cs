using MetroFramework.Forms;
using MetroFramework.Components;
using MetroFramework.Interfaces;
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
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();
            db.girisYap(txtAd.Text, txtSifre.Text);
            if (db.mesaj == true)
            {
                MessageBox.Show("Doğru");
                Index index = new Index();
                index.Show();

            }
            else
                MessageBox.Show("Yanlış");
        }
    }
}
