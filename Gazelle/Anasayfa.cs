using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazelle
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        Database database = new Database();
        private void Anasayfa_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(37, 43, 51);
           
            database.dataGridDoldur(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusteriKayit kayit = new MusteriKayit();
            kayit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.dataGridDoldur(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 aracKayit = new Form2();
            aracKayit.ShowDialog();
        }
    }
}
