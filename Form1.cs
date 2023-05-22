using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GeriDon  //proje ismini geridön koymuştum.
{
    public partial class MainForm : Form
    {
        private SqlConnection connection;
        private string connectionString = "Your_Connection_String"; // Veritabanı bağlantı dizesini buraya girin

        public MainForm()
        {
            InitializeComponent();

            connection = new SqlConnection(connectionString);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            VerileriGuncelle();
        }

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string adres = txtAdres.Text;
            int kazanilanPuan = Convert.ToInt32(txtKazanilanPuan.Text);
            decimal odenenPara = Convert.ToDecimal(txtOdenenPara.Text);

            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO Musteriler (Ad, Soyad, Adres, KazanilanPuan, OdenenPara) VALUES (@Ad, @Soyad, @Adres, @KazanilanPuan, @OdenenPara)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Adres", adres);
                    command.Parameters.AddWithValue("@KazanilanPuan", kazanilanPuan);
                    command.Parameters.AddWithValue("@OdenenPara", odenenPara);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Müşteri başarıyla eklendi.");

                VerileriGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void VerileriGuncelle()
        {
            try
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Musteriler";

                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvMusteriler.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
