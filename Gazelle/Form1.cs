using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace Gazelle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        Database database = new Database();
        PictureBox[] pictureBoxArray = new PictureBox[4];
        DataTable istekTablo = new DataTable();
        DataTable parcaTablo = new DataTable();
                
        // KaydetClick
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker2.Value;
            DateTime selectedDateTime2 = dateTimePicker4.Value;

            String gTarih = dateTimePicker1.Value.Date.ToString("dd-MM-yyy");
            String gSaat = selectedDateTime.Hour.ToString("00") + ":" + selectedDateTime.ToString("00");
            String cTarih = dateTimePicker3.Value.Date.ToString("dd-MM-yyy");
            String cSaat = selectedDateTime2.Hour.ToString("00") + ":" + selectedDateTime2.Minute.ToString("00");

            //aracinn kismi ekleme 
            database.ekleme(label48,gTarih,gSaat,cTarih,cSaat,comboBox1);
            // istek Ekleme 
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        string istek = row.Cells[0].Value.ToString();
                        string usta = row.Cells[1].Value.ToString();
                        database.ekleme(textBox5, istek, maskedTextBox4, usta);
                    }
                }// parca ekleme
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow)
                    {

                        string parcaNo = row.Cells[0].Value.ToString();
                        string parcaAdi = row.Cells[1].Value.ToString();
                        string adet = row.Cells[2].Value.ToString();
                        string tutar = row.Cells[3].Value.ToString();

                        database.ekleme(label48.Text, parcaNo, parcaAdi, adet, tutar);

                    }
                }// foto
                foreach (System.Drawing.Image resim in imageList1.Images)
                {
                    // Resmi veritabanına kaydetme işlemi
                    byte[] imageBytes;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        resim.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageBytes = ms.ToArray();
                    }
                    database.ekleme(textBox5.Text, maskedTextBox4.Text, imageBytes);
                }
                MessageBox.Show("Kayit olusturuldu");
            }
            catch {
                MessageBox.Show("Hata");
            }
          
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            textBox17.MaxLength = 11;
            textBox3.MaxLength = 11;
            textBox9.MaxLength = 11;
            textBox13.MaxLength = 11;

            istekTablo.Columns.Add("İstek", typeof(string));
            istekTablo.Columns.Add("Usta", typeof(string));
            dataGridView2.DataSource = istekTablo;
            dataGridView2.Columns["Usta"].Width = 120;
            dataGridView2.Columns["İstek"].Width = 250;
            
            parcaTablo.Columns.Add("Parca No", typeof(string));
            parcaTablo.Columns.Add("Parca Adi", typeof(string));
            parcaTablo.Columns.Add("Adet", typeof(string));
            parcaTablo.Columns.Add("Tutar", typeof(string));
            dataGridView1.DataSource = parcaTablo;

            dataGridView1.Columns["Parca No"].Width = 75;
            dataGridView1.Columns["Parca Adi"].Width = 200;
            dataGridView1.Columns["Adet"].Width = 50;
            dataGridView1.Columns["Tutar"].Width = 50;
            
            this.WindowState = FormWindowState.Maximized;
            // this.FormBorderStyle = FormBorderStyle.None;
            database.Baglanti();
            textBox14.Enabled = false;
            textBox15.Enabled = false;
            textBox16.Enabled = false;
            richTextBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Enabled = false;
            maskedTextBox4.Enabled = false;
            maskedTextBox5.Enabled = false;
            maskedTextBox6.Enabled = false;
            textBox5.Enabled = false;
            textBox1.Enabled = false;
            database.veriCekme("select IsyeriAdi from tbl_IsYeri", comboBox1, "IsyeriAdi");
            database.veriCekme("Select * from tbl_Iller", comboBox5, "sehirAdi");

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            //belki istekleri 
            String[] istekListesi =
            {
                "istek 1",
                "istek 1",
                "istek 1",
                "istek 1",
                "istek 1"
             };

            database.veriCekme("select * from tbl_parcalar", comboBox11, "parcaAdi");



        }

        // Bunu belki is bolumu diye yapabnilirim beklesin asko
        public void comboDoldur(String[] dizi, ComboBox combo)
        {
            combo.Items.AddRange(dizi);
        }
        // ilceleri getirme
        public void ilceleriGetir(ComboBox combo, ComboBox changeCombo)
        {
            combo.Items.Clear();
            String Sorgu = "select ilceAdi from tbl_Ilceler where sehirId = '" + (changeCombo.SelectedIndex + 1) + "'";
            database.veriCekme(Sorgu, combo, "ilceAdi");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox14.Clear();
            textBox15.Clear();
            richTextBox1.Clear();
            textBox16.Clear();
            database.veriCekme("select  tbl_Iller.sehirAdi  from Tbl_IsYeri inner join tbl_Iller on Tbl_IsYeri.ilId =tbl_Iller.id where Tbl_IsYeri.ilId = '"
                + (comboBox1.SelectedIndex + 1) + "'", textBox14, "sehirAdi");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '" + comboBox1.Text + "'", textBox15, "ilceAdi");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '" + comboBox1.Text + "'", richTextBox1, "acikAdres");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '" + comboBox1.Text + "'", textBox16, "telefon");


        }
        // Kaydet
        private void button3_Click(object sender, EventArgs e)
        {
            //arama
            Arama("isKartiNo", textBox17);
            Arama("plaka", textBox3);
            Arama("sasiNo", textBox9);
            Arama("motorNo", textBox13);

        }
        public void Arama(String aranan, TextBox text)
        {

            String sorgu = "SELECT tbl_Arac.id,tbl_Musteri.vergiNo,tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Iller.sehirAdi,tbl_Musteri.ilceAdi,tbl_Musteri.adres," +
                "tbl_Arac.Tip,tbl_Arac.isKartiNo,tbl_Arac.plaka,tbl_Arac.sasiNo,tbl_Arac.motorNo,tbl_Arac.km FROM tbl_Musteri" +
                " INNER JOIN tbl_Arac ON tbl_Musteri.vergiNo=tbl_Arac.vergiNo " +
                "INNER JOIN tbl_Iller ON tbl_Iller.id=tbl_Musteri.ilId where ";
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", radioButton1, radioButton1, "Tip");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", maskedTextBox4, "plaka");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", maskedTextBox5, "sasiNo");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", maskedTextBox6, "motorNo");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", maskedTextBox7, "km");


            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", textBox2, "ad");
            database.veriEsitlme(sorgu + aranan + "='" + text.Text + "'", comboBox5, "sehirAdi");
            comboBox6.Items.Clear();
            database.veriCekme("select * from tbl_Ilceler where sehirId='" + (comboBox5.SelectedIndex + 1) + "'", comboBox6, "ilceAdi");
            database.veriEsitlme(sorgu + aranan + "='" + text.Text + "'", comboBox6, "ilceAdi");
            // database.veriCekme(sorgu + aranan + "='" + text.Text + "'", comboBox6, "ilceAdi");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", richTextBox2, "adres");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", textBox5, "vergiNo");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", textBox1, "isKartiNo");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", label48, "id");

        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-9C0C8UB\SQLEXPRESS; Initial Catalog=TurkiyeAdres; Integrated Security=True");
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDataReader = null;
            SqlDataAdapter dataAdapter = null;

            /*
            comboBox6.Items.Clear();
            database.veriCekme("select * from tbl_Ilceler where sehirId='" + (comboBox5.SelectedIndex + 1) + "'", comboBox6, "ilceAdi");
            */
            comboBox6.Items.Clear();
            int selectedCityId = comboBox5.SelectedIndex + 1; // Seçilen ilin ID'si

            // İlçeleri ilin ID'sine göre veritabanından çekin ve ComboBox'a ekleyin
            string ilceSorgu = "SELECT ilceAdi FROM tbl_Ilceler WHERE sehirId = " + selectedCityId;

            try
            {

                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = ilceSorgu;
                sqlConnection.Open();

                using (sqlDataReader = sqlCmd.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        comboBox6.Items.Add(sqlDataReader["ilceAdi"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.ToString());
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }


        }
        public void fotoEkle(PictureBox pictureBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string resimYolu = openFileDialog.FileName;
                pictureBox.Image = System.Drawing.Image.FromFile(resimYolu);
                imageList1.Images.Add(pictureBox.Image);

            }

        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string dosyaYolu in openFileDialog.FileNames)
                {
                    // Seçilen resmi PictureBox kontrolüne yükleyin
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = System.Drawing.Image.FromFile(dosyaYolu);

                    // PictureBox'taki resmi ImageList'e ekleyin
                    imageList1.Images.Add(pictureBox.Image);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.TextLength > 0 && usta7.TextLength > 0)
            {
                istekTablo.Rows.Add(textBox4.Text, usta7.Text);
                dataGridView2.DataSource = istekTablo;
                textBox4.Clear();
                usta7.Clear();
            }
            else {
                MessageBox.Show("Bilgileri Kontrol Ediniz");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

             if (parca11.TextLength > 0 && comboBox11.SelectedIndex != -1  && adet11.TextLength > 0 && tutar11.TextLength > 0)
             {
                 parcaTablo.Rows.Add(parca11.Text, comboBox11.SelectedItem.ToString(), adet11.Text, tutar11.Text);
                parca11.Clear();
                comboBox11.SelectedIndex = -1;
                adet11.Clear();
                tutar11.Clear();
                
             }
             else if (parca12.TextLength > 0 && textBox6.TextLength > 0 && adet12.TextLength > 0 && tutar12.TextLength > 0)
            {
                parcaTablo.Rows.Add(parca12.Text, textBox6.Text, adet12.Text, tutar12.Text);
                parca12.Clear();
                textBox6.Clear();
                adet12.Clear();
                tutar12.Clear();
            }
            else {
                // erro hatasi vercez

                MessageBox.Show("Hatali Giris yapildi ");
            }

            dataGridView1.DataSource = parcaTablo;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow)
                {
                    
                    string istek = row.Cells[0].Value.ToString(); 
                    string usta = row.Cells[1].Value.ToString();
                    database.ekleme(textBox5,istek,maskedTextBox4,usta);   
                }
               
            }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                if (!row.IsNewRow)
                {

                    string parcaNo = row.Cells[0].Value.ToString();
                    string parcaAdi = row.Cells[1].Value.ToString();
                    string adet = row.Cells[2].Value.ToString();
                    string tutar = row.Cells[3].Value.ToString();

                    database.ekleme(label48.Text, parcaNo, parcaAdi, adet, tutar);
                    
                }

            }*/
        }

   
        private void button8_Click(object sender, EventArgs e)
        {
            /*
            foreach (System.Drawing.Image resim in imageList1.Images)
            {
                // Resmi veritabanına kaydetme işlemi
                byte[] imageBytes;

                using (MemoryStream ms = new MemoryStream())
                {
                    resim.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageBytes = ms.ToArray();
                }
                database.ekleme(textBox5.Text,maskedTextBox4.Text, imageBytes);
            }
            */
        }
    }
}
