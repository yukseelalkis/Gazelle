using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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



        // KaydetClick
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDateOnly = dateTimePicker1.Value.Date;
            DateTime selectedDateTime = dateTimePicker2.Value;
            DateTime selectedDateOnly3 = dateTimePicker3.Value.Date;
            DateTime selectedDateTime4 = dateTimePicker4.Value;
            label9.Text = selectedDateOnly3.ToString("dd-MM-yyy");
 
            label10.Text = selectedDateTime4.Hour.ToString("00") + ":"+ selectedDateTime4.Minute.ToString("00");
            
        }



        private void Form1_Load(object sender, EventArgs e)
        {
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
            maskedTextBox3.Enabled = false;
            textBox1.Enabled = false;
            database.veriCekme("select IsyeriAdi from tbl_IsYeri", comboBox1, "IsyeriAdi");
            database.veriCekme("Select * from tbl_Iller", comboBox5, "sehirAdi");



            String[] istekListesi =
            {
                "istek 1",
                "istek 1",
                "istek 1",
                "istek 1",
                "istek 1"
             };




            String[] parcaAdi =
            {
                "Motor yağı değişimi",
                "Motor bakımı",
                "Yağ filtresi değişimi",
                "Hava filtresi değişimi",
                "Polen filtresi değişimi",
                "Yakıt filtresi değişimi",
                "Frenlerin bakımı ve kontrolü",
                "Soğutma suyu kontrolü","Far ayarı",
                "Ön takım kontrol",
                "Elektronik check-up",
                "Lastiklerin kontrol edilmesi",
                "Mevsime göre klima kullanılacak ise gaz basıncı kontrolü",
                "Şase ve kaportanın gözle kontrolü"
            };

            comboDoldur(parcaAdi,comboBox12);
            comboDoldur(parcaAdi, comboBox13); 
            comboDoldur(parcaAdi, comboBox14);
            comboDoldur(parcaAdi, comboBox15);
            comboDoldur(parcaAdi, comboBox16);
            comboDoldur(parcaAdi, comboBox17);
            comboDoldur(parcaAdi, comboBox18);
            comboDoldur(parcaAdi, comboBox19);
            comboDoldur(parcaAdi, comboBox20);
            comboDoldur(parcaAdi, comboBox21);
            comboDoldur(parcaAdi, comboBox2);
            comboDoldur(parcaAdi, comboBox3);
            comboDoldur(parcaAdi, comboBox11);
            comboDoldur(parcaAdi, comboBox22);
            comboDoldur(parcaAdi, comboBox12);
            comboDoldur(parcaAdi, comboBox13);
            comboDoldur(parcaAdi, comboBox14);
            comboDoldur(parcaAdi, comboBox15);
            comboDoldur(parcaAdi, comboBox16);
            comboDoldur(parcaAdi, comboBox4);

            comboDoldur(istekListesi, comboBox7);
            comboDoldur(istekListesi, comboBox8);
            comboDoldur(istekListesi, comboBox9);
            comboDoldur(istekListesi, comboBox10);
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
            database.veriCekme("select  tbl_Iller.sehirAdi  from Tbl_IsYeri inner join tbl_Iller on Tbl_IsYeri.ilId =tbl_Iller.id where Tbl_IsYeri.ilId = '" 
                + (comboBox1.SelectedIndex + 1) + "'", textBox14, "sehirAdi");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '"+comboBox1.Text+"'",textBox15,"ilceAdi");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '" + comboBox1.Text + "'", richTextBox1, "acikAdres");
            database.veriCekme("select * from tbl_IsYeri where IsyeriAdi= '" + comboBox1.Text + "'",textBox16 , "telefon");


        }
        // Kaydet
        private void button3_Click(object sender, EventArgs e)
        {
            //arama
            Arama("isKartiNo",textBox17);
            Arama("plaka", textBox3);
            Arama("sasiNo", textBox9);
            Arama("motorNo", textBox13);
            label48.Text = (comboBox5.SelectedIndex + 1).ToString();
        }

        public void Arama(String aranan, TextBox text)
        {
           
            String sorgu = "SELECT tbl_Musteri.vergiNo,tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Iller.sehirAdi,tbl_Musteri.ilceAdi,tbl_Musteri.adres," +
                "tbl_Arac.Tip,tbl_Arac.isKartiNo,tbl_Arac.plaka,tbl_Arac.sasiNo,tbl_Arac.motorNo,tbl_Arac.km FROM tbl_Musteri" +
                " INNER JOIN tbl_Arac ON tbl_Musteri.vergiNo=tbl_Arac.vergiNo " +
                "INNER JOIN tbl_Iller ON tbl_Iller.id=tbl_Musteri.ilId where ";
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", radioButton1,radioButton1 ,"Tip");
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
           
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", maskedTextBox3, "vergiNo");
            database.veriCekme(sorgu + aranan + "='" + text.Text + "'", textBox1, "isKartiNo");

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
            





        

    }
}
