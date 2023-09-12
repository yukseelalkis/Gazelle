using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazelle
{
    public class Database
    {
        public static SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-9C0C8UB\SQLEXPRESS; Initial Catalog=TurkiyeAdres; Integrated Security=True");
        public static SqlCommand sqlCmd = new SqlCommand();
        public static SqlDataReader sqlDataReader = null;
        public static SqlDataAdapter dataAdapter = null;

        // BAGLANTI
        public void Baglanti ()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baglanti Hatasi" + ex.ToString());
            }
            finally {
                if (sqlConnection != null)  sqlConnection.Close(); 
            }
        }



        public void ekleme(String sorgu,TextBox vergiNo,TextBox ad,TextBox soyad,int ilId,String ilceAdi,RichTextBox rich)
        {

            // eklemeden once var mi diye kontrol edilsin ondaan sonra ekleme yapilcak
            // 


            sqlCmd.CommandText = sorgu;
            sqlCmd.Connection = sqlConnection;
            
            sqlCmd.Parameters.AddWithValue("@vergiNo", vergiNo.Text);
            sqlCmd.Parameters.AddWithValue("@ad", ad.Text);
            sqlCmd.Parameters.AddWithValue("@soyad", soyad.Text);
            sqlCmd.Parameters.AddWithValue("@ilID", ilId);
            sqlCmd.Parameters.AddWithValue("@ilceId", ilceAdi);
            sqlCmd.Parameters.AddWithValue("@acikAdres", rich.Text);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Connection.Close();

        }

        public void ekleme(String sorgu, TextBox vergiNo,RadioButton radio, TextBox iskarti,TextBox plaka, TextBox sasi,TextBox motor, TextBox km)
        {
            try
            {
                sqlCmd.CommandText = sorgu;
                sqlCmd.Connection = sqlConnection;
                sqlCmd.Parameters.AddWithValue("@vergiNo", vergiNo.Text);
                sqlCmd.Parameters.AddWithValue("@tip", radio.Text);
                sqlCmd.Parameters.AddWithValue("@isKartiNo", iskarti.Text);
                sqlCmd.Parameters.AddWithValue("@plaka", plaka.Text);
                sqlCmd.Parameters.AddWithValue("@sasiNo", sasi.Text);
                sqlCmd.Parameters.AddWithValue("@motor", motor.Text);
                sqlCmd.Parameters.AddWithValue("@km", km.Text);
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Arac eklendi");

            }
            catch (Exception e)
            {
                 MessageBox.Show(e.Message.ToString());
                //MessageBox.Show("Vergi Numaranizi kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                sqlCmd.Connection.Close();
            }

        }




        // Comboya Veri cekme 
        public void veriCekme(String Sorgu, ComboBox combo,String istek)
        {

            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandText = Sorgu;

            sqlConnection.Open();
            

            using (sqlDataReader=sqlCmd.ExecuteReader())
            {
                
                while(sqlDataReader.Read())
                {
                    
                    combo.Items.Add(sqlDataReader[istek].ToString());
                }
            }
            sqlConnection.Close();           
        }
        public void veriCekme(String Sorgu, TextBox textBox, String istek)
        {


            try
            {
                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = Sorgu;

                sqlConnection.Open();


                using (sqlDataReader = sqlCmd.ExecuteReader())
                {

                    while (sqlDataReader.Read())
                    {

                        textBox.Text = sqlDataReader[istek].ToString();
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.ToString());
            }
            finally { if (sqlConnection != null) sqlConnection.Close(); }

        }
        public void veriCekme(String Sorgu, RichTextBox textBox, String istek)
        {


            try
            {
                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = Sorgu;

                sqlConnection.Open();


                using (sqlDataReader = sqlCmd.ExecuteReader())
                {

                    while (sqlDataReader.Read())
                    {

                        textBox.Text = sqlDataReader[istek].ToString();
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.ToString());
            }
            finally { if (sqlConnection != null) sqlConnection.Close(); }

        }
        public void veriCekme(String Sorgu, MaskedTextBox textBox, String istek)
        {


            try
            {
                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = Sorgu;

                sqlConnection.Open();


                using (sqlDataReader = sqlCmd.ExecuteReader())
                {

                    while (sqlDataReader.Read())
                    {

                        textBox.Text = sqlDataReader[istek].ToString();
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.ToString());
            }
            finally { if (sqlConnection != null) sqlConnection.Close(); }

        }
        public void veriCekme(String Sorgu, RadioButton kamyonet, RadioButton panelvan, String istek)
        {


            try
            {
                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = Sorgu;

                sqlConnection.Open();


                using (sqlDataReader = sqlCmd.ExecuteReader())
                {

                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader[istek].ToString() == "Kamyonet")
                        {
                            kamyonet.Checked = true;
                        }
                        else
                        {
                            panelvan.Checked = true;
                        }
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.ToString());
            }
            finally { if (sqlConnection != null) sqlConnection.Close(); }

        }
        public void veriEsitlme(String Sorgu, ComboBox combo, String istek)
        {


            try
            {
                sqlCmd.Connection = sqlConnection;
                sqlCmd.CommandText = Sorgu;

                sqlConnection.Open();


                using (sqlDataReader = sqlCmd.ExecuteReader())
                {

                    while (sqlDataReader.Read())
                    {

                        combo.SelectedItem = sqlDataReader[istek].ToString();
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.ToString());
            }
            finally { if (sqlConnection != null) sqlConnection.Close(); }

        }
       





        public void dataGridDoldur(DataGridView dataGridView)
        {
            String sorgu = "SELECT tbl_Musteri.vergiNo,tbl_Musteri.ad,tbl_Musteri.soyAd,tbl_Musteri.ilId,tbl_Musteri.adres,tbl_Arac.Tip,tbl_Arac.isKartiNo,tbl_Arac.plaka,tbl_Arac.sasiNo,tbl_Arac.motorNo,tbl_Arac.km FROM tbl_Musteri INNER JOIN tbl_Arac ON tbl_Musteri.vergiNo=tbl_Arac.vergiNo";
            sqlConnection.Open();
            dataAdapter = new SqlDataAdapter(sorgu, sqlConnection);
            DataTable tablo = new DataTable();
            dataAdapter.Fill(tablo);
            dataGridView.DataSource = tablo;
            sqlConnection.Close();


        }
    }
}
