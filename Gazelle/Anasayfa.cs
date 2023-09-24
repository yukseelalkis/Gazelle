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
       string sorgu = "SELECT tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Arac.vergiNo, tbl_Arac.plaka,tbl_aracServis.girisTarih ," +
                 "tbl_aracServis.tahminiTeslimTarihi,tbl_musteriIstek.musteriIstek,tbl_degisenParcalar.parcaAd,tbl_degisenParcalar.parcaAdet,tbl_degisenParcalar.parcaTutar" +
                 " FROM tbl_Arac INNER JOIN tbl_aracServis\r\nON tbl_Arac.id = tbl_aracServis.aracId\r\nINNER JOIN tbl_degisenParcalar " +
                 "ON tbl_Arac.id= tbl_degisenParcalar.aracId\r\nINNER JOIN tbl_Musteri\r\nON tbl_Arac.vergiNo=tbl_Musteri.vergiNo\r\nINNER JOIN tbl_musteriIstek\r\nON " +
                 "tbl_Musteri.vergiNo = tbl_musteriIstek.musteriID";

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(37, 43, 51);
           
            database.dataGridDoldur(sorgu,dataGridView1);
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
            database.dataGridDoldur(sorgu,dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 aracKayit = new Form2();
            aracKayit.ShowDialog();
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

       

       

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string degisken = textBox2.Text;
            if (degisken.Length == 0)
            {
                sorgu = "SELECT tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Arac.vergiNo, tbl_Arac.plaka,tbl_aracServis.girisTarih ," +
                 "tbl_aracServis.tahminiTeslimTarihi,tbl_musteriIstek.musteriIstek,tbl_degisenParcalar.parcaAd,tbl_degisenParcalar.parcaAdet,tbl_degisenParcalar.parcaTutar" +
                 " FROM tbl_Arac INNER JOIN tbl_aracServis\r\nON tbl_Arac.id = tbl_aracServis.aracId\r\nINNER JOIN tbl_degisenParcalar " +
                 "ON tbl_Arac.id= tbl_degisenParcalar.aracId\r\nINNER JOIN tbl_Musteri\r\nON tbl_Arac.vergiNo=tbl_Musteri.vergiNo\r\nINNER JOIN tbl_musteriIstek\r\nON " +
                 "tbl_Musteri.vergiNo = tbl_musteriIstek.musteriID";
            }
            else
            {
                sorgu = "SELECT tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Arac.vergiNo, tbl_Arac.plaka,tbl_aracServis.girisTarih ," +
                  "tbl_aracServis.tahminiTeslimTarihi,tbl_musteriIstek.musteriIstek,tbl_degisenParcalar.parcaAd,tbl_degisenParcalar.parcaAdet,tbl_degisenParcalar.parcaTutar" +
                  " FROM tbl_Arac INNER JOIN tbl_aracServis\r\nON tbl_Arac.id = tbl_aracServis.aracId\r\nINNER JOIN tbl_degisenParcalar " +
                  "ON tbl_Arac.id= tbl_degisenParcalar.aracId\r\nINNER JOIN tbl_Musteri\r\nON tbl_Arac.vergiNo=tbl_Musteri.vergiNo\r\nINNER JOIN tbl_musteriIstek\r\nON " +
                  "tbl_Musteri.vergiNo = tbl_musteriIstek.musteriID WHERE  tbl_Arac.plaka like '" + textBox2.Text + "%'";
            }
            database.dataGridDoldur(sorgu, dataGridView1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string degisken = textBox1.Text;
            if (degisken.Length == 0)
            {
                sorgu = "SELECT tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Arac.vergiNo, tbl_Arac.plaka,tbl_aracServis.girisTarih ," +
                 "tbl_aracServis.tahminiTeslimTarihi,tbl_musteriIstek.musteriIstek,tbl_degisenParcalar.parcaAd,tbl_degisenParcalar.parcaAdet,tbl_degisenParcalar.parcaTutar" +
                 " FROM tbl_Arac INNER JOIN tbl_aracServis\r\nON tbl_Arac.id = tbl_aracServis.aracId\r\nINNER JOIN tbl_degisenParcalar " +
                 "ON tbl_Arac.id= tbl_degisenParcalar.aracId\r\nINNER JOIN tbl_Musteri\r\nON tbl_Arac.vergiNo=tbl_Musteri.vergiNo\r\nINNER JOIN tbl_musteriIstek\r\nON " +
                 "tbl_Musteri.vergiNo = tbl_musteriIstek.musteriID";
            }
            else
            {
                sorgu = "SELECT tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Arac.vergiNo, tbl_Arac.plaka,tbl_aracServis.girisTarih ," +
                  "tbl_aracServis.tahminiTeslimTarihi,tbl_musteriIstek.musteriIstek,tbl_degisenParcalar.parcaAd,tbl_degisenParcalar.parcaAdet,tbl_degisenParcalar.parcaTutar" +
                  " FROM tbl_Arac INNER JOIN tbl_aracServis\r\nON tbl_Arac.id = tbl_aracServis.aracId\r\nINNER JOIN tbl_degisenParcalar " +
                  "ON tbl_Arac.id= tbl_degisenParcalar.aracId\r\nINNER JOIN tbl_Musteri\r\nON tbl_Arac.vergiNo=tbl_Musteri.vergiNo\r\nINNER JOIN tbl_musteriIstek\r\nON " +
                  "tbl_Musteri.vergiNo = tbl_musteriIstek.musteriID WHERE  tbl_Arac.vergiNo like '" + textBox1.Text + "%'";
            }
            database.dataGridDoldur(sorgu, dataGridView1);
        }

   


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Hücreye tıklama yapıldı mı kontrolü
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex]; // Tıklanan satırı al

                // Satır içerisindeki hücrelere erişim örneği:
                string cell1Value = selectedRow.Cells["vergiNo"].Value.ToString();

                database.ResmiVeritabanindanCek(cell1Value, pictureBox2,512,512);

            }
            
        }
    }
}
