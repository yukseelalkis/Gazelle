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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //plakaya bakacaz 
            if (textBox6.TextLength < 10 || textBox1.TextLength < 11 || textBox2.TextLength < 5 || textBox3.TextLength < 11 || textBox4.TextLength < 11 || textBox5.TextLength == 0 )
            {
                MessageBox.Show("Hatalı giriş yaptınız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
               

                Database database = new Database();
                string sorgu = "insert into tbl_Arac values (@vergiNo,@tip,@isKartiNo,@plaka,@sasiNo,@motor,@km)";

                if (radioButton1.Checked)
                    database.ekleme(sorgu, textBox6, radioButton1, textBox1, textBox2, textBox3, textBox4, textBox5);
                else
                {
                    database.ekleme(sorgu, textBox6, radioButton2, textBox1, textBox2, textBox3, textBox4, textBox5);
                }


                MessageBox.Show("ekleme yapildi");
            }
            
           
        }





        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11;
            textBox2.MaxLength = 9;
            textBox3.MaxLength = 11;
            textBox4.MaxLength = 11;
            textBox5.MaxLength = 5;
            textBox6.MaxLength = 10;
        }
    }
}
