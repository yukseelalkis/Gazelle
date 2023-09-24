using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

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
        //istek eKleme+
        public void ekleme(TextBox vergiNo ,String istek , MaskedTextBox plaka,String usta)
        {
            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "INSERT INTO tbl_musteriIstek values (@musteriID,@istek,@plaka,@usta)";
                sqlCmd.Connection = sqlConnection;
                sqlCmd.Parameters.AddWithValue("@musteriID", vergiNo.Text);
                sqlCmd.Parameters.AddWithValue("@istek", istek);
                sqlCmd.Parameters.AddWithValue("@plaka", plaka.Text);
                sqlCmd.Parameters.AddWithValue("@usta", usta);
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }
        }
       //  degisen parca ekleme
        public void ekleme(String aracId,string parcaNo,string parcaAd,string adet,string tutar)
        {
            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "INSERT INTO tbl_degisenParcalar (aracId, parcaNo, parcaAd, parcaAdet, parcaTutar) VALUES (@aracId, @parcaNo, @parcaAd, @adet, @tutar)";
                sqlCmd.Connection = sqlConnection;
                sqlCmd.Parameters.AddWithValue("@aracId", Convert.ToInt64(aracId));
                sqlCmd.Parameters.AddWithValue("@parcaNo", parcaNo);
                sqlCmd.Parameters.AddWithValue("@parcaAd", parcaAd);
                sqlCmd.Parameters.AddWithValue("@adet", Convert.ToInt32(adet));
                sqlCmd.Parameters.AddWithValue("@tutar", Convert.ToInt32(tutar));
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }

        }
        // arac Servis ekleme 
        public void ekleme(Label label, String giristrh, String girisSaat, String cikisTarih, String cikisSaat,ComboBox servisId)
        {

            // eklemeden once var mi diye kontrol edilsin ondaan sonra ekleme yapilcak
            // 
            using (SqlCommand sqlCmd = new SqlCommand())
            {

                sqlCmd.CommandText = "insert into tbl_aracServis values (@aracId,@girisTrh,@girisSaat,@cikisTrh,@cikisSaat,@servisId)";
                sqlCmd.Connection = sqlConnection;
                sqlCmd.Parameters.AddWithValue("@aracId", Convert.ToInt32(label.Text));
                sqlCmd.Parameters.AddWithValue("@girisTrh", giristrh.ToString());
                sqlCmd.Parameters.AddWithValue("@girisSaat", girisSaat.ToString());
                sqlCmd.Parameters.AddWithValue("@cikisTrh", cikisTarih.ToString());
                sqlCmd.Parameters.AddWithValue("@cikisSaat", cikisSaat.ToString());
                sqlCmd.Parameters.AddWithValue("@servisId", (servisId.SelectedIndex + 1));
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }

        }
        // Musteri Ekleme 
        public void ekleme(String sorgu,TextBox vergiNo,TextBox ad,TextBox soyad,int ilId,String ilceAdi,RichTextBox rich,MaskedTextBox tel)
        {

          
           
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    sqlCmd.CommandText = sorgu;
                    sqlCmd.Connection = sqlConnection;
                    sqlCmd.Parameters.AddWithValue("@vergiNo", vergiNo.Text);
                    sqlCmd.Parameters.AddWithValue("@ad", ad.Text);
                    sqlCmd.Parameters.AddWithValue("@soyad", soyad.Text);
                    sqlCmd.Parameters.AddWithValue("@ilID", ilId);
                    sqlCmd.Parameters.AddWithValue("@ilceId", ilceAdi);
                    sqlCmd.Parameters.AddWithValue("@acikAdres", rich.Text);
                    sqlCmd.Parameters.AddWithValue("@tel", tel.Text);
                    sqlCmd.Connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Connection.Close();
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        // arac kayit
        public void ekleme(String sorgu, TextBox vergiNo,RadioButton radio, TextBox iskarti,TextBox plaka, TextBox sasi,TextBox motor, TextBox km)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand())
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
        // foto ekleme
        public void ekleme(String vergiNo, String plaka , Byte [] foto)
        {
            using (SqlCommand sqlCmd = new SqlCommand())
            {
                sqlCmd.CommandText = "INSERT INTO  tbl_resimler values (@vergiNo,@plaka,@image)";
                sqlCmd.Connection = sqlConnection;
                sqlCmd.Parameters.AddWithValue("@vergiNo",vergiNo);
                sqlCmd.Parameters.AddWithValue("@plaka", plaka);
                sqlCmd.Parameters.AddWithValue("@image", foto);
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
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
        public void veriCekme(String Sorgu, Label label, String istek)
        {

            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandText = Sorgu;

            sqlConnection.Open();


            using (sqlDataReader = sqlCmd.ExecuteReader())
            {

                while (sqlDataReader.Read())
                {

                    label.Text=sqlDataReader[istek].ToString();
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

        public void dataGridDoldur(string sorgu,DataGridView dataGridView)
        {
           
            sqlConnection.Open();
            dataAdapter = new SqlDataAdapter(sorgu, sqlConnection);
            DataTable tablo = new DataTable();
            dataAdapter.Fill(tablo);
            dataGridView.DataSource = tablo;
            sqlConnection.Close();


        }


        public void ResmiVeritabanindanCek(string vergi, PictureBox pictureBox1, int genislik, int yukseklik)
        {
            try
            {
                string selectQuery = "SELECT image FROM tbl_resimler WHERE vergiNo = @vergiNo";

                using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-9C0C8UB\SQLEXPRESS; Initial Catalog=TurkiyeAdres; Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@vergiNo", vergi);

                        sqlConnection.Open();
                        object imageObject = cmd.ExecuteScalar();

                        if (imageObject != null)
                        {
                            byte[] imageBytes = (byte[])imageObject;
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                System.Drawing.Image resim = System.Drawing.Image.FromStream(ms);

                                // Resmi istediğiniz boyuta yeniden boyutlandırın
                                System.Drawing.Image yenidenBoyutlandirilmisResim = new Bitmap(resim, genislik, yukseklik);

                                pictureBox1.Image = yenidenBoyutlandirilmisResim;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Resim bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


    }
}
