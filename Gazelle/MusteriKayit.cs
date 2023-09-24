using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazelle
{
    public partial class MusteriKayit : Form
    {
        public MusteriKayit()
        {
            InitializeComponent();
        }

        Database database = new Database();


        private void MusteriKayit_Load(object sender, EventArgs e)
        {
          

            panel1.BackColor = Color.FromArgb(37, 43, 51);
            pictureBox1.BackColor = Color.FromArgb(37, 43, 51);

           

            label2.BackColor = Color.FromArgb(37, 43, 51);
            label2.ForeColor = Color.White;

            label3.BackColor = Color.FromArgb(37, 43, 51);
            label3.ForeColor = Color.White;

            label4.BackColor = Color.FromArgb(37, 43, 51);
            label4.ForeColor = Color.White;

            label5.BackColor = Color.FromArgb(37, 43, 51);
            label5.ForeColor = Color.White;

            label6.BackColor = Color.FromArgb(37, 43, 51);
            label6.ForeColor = Color.White;

            label7.BackColor = Color.FromArgb(37, 43, 51);
            label7.ForeColor = Color.White;

            label8.BackColor = Color.FromArgb(37, 43, 51);
            label8.ForeColor = Color.White;

            database.veriCekme("select * from tbl_Iller", comboBox1,"sehirAdi");

            textBox1.MaxLength = 25;
            textBox1.MaxLength = 25;
            textBox4.MaxLength = 10;
            richTextBox1.MaxLength = 125;

            database.Baglanti();

        }

        private void button1_Click(object sender, EventArgs e){
     
            if (textBox4.TextLength<10 || textBox2.TextLength <= 2 || textBox1.TextLength<=2 || (comboBox1.SelectedIndex+1)<0 || (comboBox2.SelectedIndex)<0)
            {
                MessageBox.Show("Hatalı giriş yaptınız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sorgu = "insert into tbl_Musteri values (@vergiNo,@ad,@soyad,@ilID,@ilceId,@acikAdres,@tel)";
                database.ekleme(sorgu,textBox4, textBox1, textBox2, (comboBox1.SelectedIndex + 1), (comboBox2.SelectedItem.ToString()), richTextBox1,maskedTextBox1);

                MessageBox.Show("Müşteri Eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            label9.Text = comboBox2.SelectedItem.ToString();
}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            database.veriCekme("select * from tbl_Ilceler where sehirId='"+(comboBox1.SelectedIndex+1)+"'",comboBox2,"ilceAdi");
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tuş girişini engelle
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Tuş girişini engelle
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Tuş girişini engelle
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Tuş girişini engelle
            }
        }
    }
}
