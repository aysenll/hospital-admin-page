using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace sql_homework2
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        private string connectionString = "Host=localhost;Port=5434;Username=postgres;Password=1234;Database=sql_homework1";

        public Form1()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connectionString);
        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
               try
               {
                   conn.Open();
                   string query = "SELECT * FROM HASTA";
                  NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                   DataSet dataSet = new DataSet();
                   adapter.Fill(dataSet, "HASTA");
                    dataGridView1.DataSource = dataSet.Tables["HASTA"];
               }


               catch(Exception ex)
               {
                   MessageBox.Show("hasta listeleme hatası:" + ex.ToString());
               }
               finally
               {
                   conn.Close();
               }

           



        }


        private void button_guncelle_Click(object sender, EventArgs e)
        {

            // Hasta bilgilerini güncellemek için
            try
            {
                conn.Open();
                string query = "UPDATE HASTA SET adsoyad=@adsoyad, telefon=@telefon, dogumtarihi=@dogumtarihi, " +
                               "dogumyeri=@dogumyeri, adres=@adres, hastalik=@hastalik, ilac=@ilac WHERE tc=@tc";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@adsoyad", textBox_adsoyad.Text);
                cmd.Parameters.AddWithValue("@tc", textBox_tc.Text);
                cmd.Parameters.AddWithValue("@telefon", textBox_telefon.Text);
                cmd.Parameters.AddWithValue("@dogumtarihi", dtDogumTarihi.Text);
                cmd.Parameters.AddWithValue("@dogumyeri", textBox_dogumyeri.Text);
                cmd.Parameters.AddWithValue("@adres", textBox_Adres.Text);
                cmd.Parameters.AddWithValue("@hastalik", txtHastalik.Text);
                cmd.Parameters.AddWithValue("@ilac", txtIlac.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Hasta güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta güncellerken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
                Listele(); // Verileri güncelle
            }
        }

        private void button_sil_Click(object sender, EventArgs e)
        {
            // Hasta silmek için
            try
            {
                conn.Open();
                string query = "DELETE FROM HASTA WHERE tc=@tc";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tc", textBox_tc.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Hasta silindi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta silerken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
                Listele(); // Verileri güncelle
            }
        }

        private void button_ara_Click(object sender, EventArgs e)
        {
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            // Hastalık veya ad bilgisine göre arama yapmak için
            try
            {
                conn.Open();
                string query = "SELECT * FROM HASTA WHERE hastalik ILIKE @arama OR adsoyad ILIKE @arama";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@arama", "%" + textBox_arama.Text + "%");
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "HASTA");
                dataGridView1.DataSource = dataSet.Tables["HASTA"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama hatası: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        //butonlar
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void ileri_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button_ekleme_Click_1(object sender, EventArgs e)
        {
            // Yeni hasta eklemek için
            try
            {
                conn.Open();
                string query = "INSERT INTO HASTA (adsoyad, tc, telefon, dogumtarihi, dogumyeri, adres, hastalik, ilac) " +
                               "VALUES (@adsoyad, @tc, @telefon, @dogumtarihi, @dogumyeri, @adres, @hastalik, @ilac)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@adsoyad", textBox_adsoyad.Text);
                cmd.Parameters.AddWithValue("@tc", textBox_tc.Text);
                cmd.Parameters.AddWithValue("@telefon", textBox_telefon.Text);
                cmd.Parameters.AddWithValue("@dogumtarihi", dtDogumTarihi.Text);
                cmd.Parameters.AddWithValue("@dogumyeri", textBox_dogumyeri.Text);
                cmd.Parameters.AddWithValue("@adres", textBox_Adres.Text);
                cmd.Parameters.AddWithValue("@hastalik", txtHastalik.Text);
                cmd.Parameters.AddWithValue("@ilac", txtIlac.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Hasta eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta eklerken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
                Listele();                   // Verileri güncelle
            }
        }
    }
}
