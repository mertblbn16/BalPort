using Avalonia.Markup.Xaml.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BalPort
{
    public partial class Sevkiyatcs : Form
    {
        public Sevkiyatcs()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection("Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\SevkiyatBilgileri.db");

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == " " || textBox5.Text == " ")
                {

                    MessageBox.Show("Tüm alanları doldurunuz");

                }


                else
                {

                    baglan.Open();
                    // int KimlikNoK = Convert.ToInt32(textBox4.Text);
                    string sql = "insert into SevkiyatBilgileri(Sevkiyatturu,Yukturu,Guzergah, GirisTarihi, CikisTarihi ) " +
                        "Values ('" + textBox6.Text + "', '" + textBox5.Text + "' , '"
                        + textBox4.Text + "' , '" + dateTimePicker4.Value + "', '" + dateTimePicker3.Value + "' )";
                    SQLiteCommand komutislet = new SQLiteCommand(sql, baglan);
                    komutislet.ExecuteNonQuery();

                    baglan.Close(); // bağlantıyı kapat
                    MessageBox.Show("Yeni Sevkiyat kaydı başarılı!");
                    textBox6.Text = string.Empty; //textBoxı temizle
                    textBox5.Text = string.Empty; //textBoxı temizle
                    textBox4.Text = string.Empty; //textBoxı temizle




                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı İşlem Yaptınız !");

            }
            finally
            {
                baglan.Close();
            }


        }

        private void chart1_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    using (SQLiteConnection baglan = new SQLiteConnection(@"Data Source=C:\Users\mertb\source\repos\BalPort\BalPort\bin\Debug\SevkiyatBilgileri.db;Version=3;"))
                    {
                        baglan.Open();

                        // Önce tablo ismini kontrol et
                        SQLiteCommand checkTable = new SQLiteCommand(
                            "SELECT name FROM sqlite_master WHERE type='table' AND name='SevkiyatBilgileri'", baglan);
                        var tableName = checkTable.ExecuteScalar();

                        if (tableName == null)
                        {
                            MessageBox.Show("SevkiyatBilgileri tablosu bulunamadı!");
                            return;
                        }

                        SQLiteCommand user = new SQLiteCommand(
                            "SELECT Sevkiyatturu, COUNT(*) as Adet FROM SevkiyatBilgileri GROUP BY Sevkiyatturu", baglan);

                        SQLiteDataAdapter gainAdapter = new SQLiteDataAdapter(user);
                        DataTable gainTable = new DataTable();
                        gainAdapter.Fill(gainTable);

                        chart1.Series[0].Points.Clear();
                        chart1.Titles.Clear();
                        chart1.Titles.Add("Sevkiyat Türleri");

                        foreach (DataRow row in gainTable.Rows)
                        {
                            string sevkiyatTuru = row["Sevkiyatturu"].ToString();
                            int adet = Convert.ToInt32(row["Adet"]);

                            var point = new DataPoint();
                            point.YValues = new double[] { adet };
                            chart1.Series[0].Points.Add(point);
                            point.AxisLabel = sevkiyatTuru;
                            point.ToolTip = $"{sevkiyatTuru}: {adet} sevkiyat";
                            point.Label = $"{adet}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }


            /* SQLiteCommand sale = new SQLiteCommand(
                 "SELECT Product, (InitialStock - Stock) AS SoldUnits FROM ProductInfo WHERE InitialStock > Stock",
                 conn);
             SQLiteDataAdapter saleAdapter = new SQLiteDataAdapter(sale);
             DataTable saleTable = new DataTable();
             saleAdapter.Fill(saleTable);

             chart2.Series[0].Points.Clear();
             chart2.Titles.Clear();
             chart2.Titles.Add("Units Sold per Product");

             foreach (DataRow row in saleTable.Rows)
             {
                 string product = row["Product"].ToString();
                 int soldUnits = Convert.ToInt32(row["SoldUnits"]);
                 var point = new DataPoint();
                 point.YValues = new double[] { (double)soldUnits };
                 chart2.Series[0].Points.Add(point);
                 point.AxisLabel = product;
                 point.ToolTip = $"{product}: {soldUnits} sold";
                 point.Label = $"{product}\n{soldUnits} units";
             }
         }
     }*/
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                textBox6.Text = row.Cells["Sevkiyatturu"].Value?.ToString() ?? "";
                textBox5.Text = row.Cells["Yukturu"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Guzergah"].Value?.ToString() ?? "";

                // GirisTarihi için - NUMERIC'den DateTime'a dönüşüm
                if (row.Cells["GirisTarihi"].Value != null && row.Cells["GirisTarihi"].Value != DBNull.Value)
                {
                    try
                    {
                        // Eğer Unix timestamp ise
                        long timestamp = Convert.ToInt64(row.Cells["GirisTarihi"].Value);
                        dateTimePicker4.Value = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                    }
                    catch
                    {
                        try
                        {
                            // Eğer tarih string'i ise
                            dateTimePicker4.Value = Convert.ToDateTime(row.Cells["GirisTarihi"].Value.ToString());
                        }
                        catch
                        {
                            dateTimePicker4.Value = DateTime.Now;
                        }
                    }
                }

                // CikisTarihi için - NUMERIC'den DateTime'a dönüşüm
                if (row.Cells["CikisTarihi"].Value != null && row.Cells["CikisTarihi"].Value != DBNull.Value)
                {
                    try
                    {
                        // Eğer Unix timestamp ise
                        long timestamp = Convert.ToInt64(row.Cells["CikisTarihi"].Value);
                        dateTimePicker3.Value = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                    }
                    catch
                    {
                        try
                        {
                            // Eğer tarih string'i ise
                            dateTimePicker3.Value = Convert.ToDateTime(row.Cells["CikisTarihi"].Value.ToString());
                        }
                        catch
                        {
                            dateTimePicker3.Value = DateTime.Now;
                        }
                    }
                }

                // Orijinal Sevkiyatturu değerini sakla (güncelleme için)
                textBox6.Tag = row.Cells["Sevkiyatturu"].Value?.ToString();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\SevkiyatBilgileri.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT * FROM SevkiyatBilgileri";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        public void VerileriYenile()
        {
            string connectionString = "Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\SevkiyatBilgileri.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT * FROM SevkiyatBilgileri";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // DataGridView'da seçili satır var mı kontrol et
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin!");
                return;
            }

            // Seçili satırdan Sevkiyatturu değerini al
            string sevkiyatturu = dataGridView2.SelectedRows[0].Cells["Sevkiyatturu"].Value.ToString();

            string connectionString = "Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\SevkiyatBilgileri.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "DELETE FROM SevkiyatBilgileri WHERE Sevkiyatturu = @Sevkiyatturu";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@Sevkiyatturu", sevkiyatturu);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi!");
                        VerileriYenile(); // DataGridView'ı yenile
                    }
                    else
                    {
                        MessageBox.Show("Silinecek kayıt bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string connectionString = "Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\SevkiyatBilgileri.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE SevkiyatBilgileri SET Yukturu = @Yukturu, GirisTarihi = @GirisTarihi, CikisTarihi = @CikisTarihi, Guzergah = @Guzergah WHERE Sevkiyatturu = @Sevkiyatturu";
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@Sevkiyatturu", textBox6.Text);
                command.Parameters.AddWithValue("@Yukturu", textBox5.Text);
                command.Parameters.AddWithValue("@GirisTarihi", dateTimePicker4.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@CikisTarihi", dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                command.Parameters.AddWithValue("@Guzergah", textBox4.Text);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla güncellendi!");
                        VerileriYenile(); // DataGridView'ı güncelle
                    }
                    else
                    {
                        MessageBox.Show("Güncellenecek kayıt bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {
            
            try
            {
                string dbPath = @"C:\Users\mertb\source\repos\BalPort\BalPort\bin\Debug\Veritabani1.db";
                string connectionString = $"Data Source={dbPath};Version=3;";

             

                using (SQLiteConnection baglan = new SQLiteConnection(connectionString))
                {
                    baglan.Open();

                    // Şirket isimlerine göre grupla ve say
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT Sirketİsmi, COUNT(*) as Adet FROM Veritabani1 GROUP BY Sirketİsmi", baglan);

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Chart'ı temizle ve ayarla
                    chart1.Series[0].Points.Clear();
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Şirket Dağılımı");
                    chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

                    // Pasta grafik olarak ayarla
                    chart1.Series[0].ChartType = SeriesChartType.Pie;

                    // Renkler
                    Color[] renkler = {
                Color.FromArgb(54, 162, 235),   // Mavi (2)
                Color.FromArgb(255, 99, 132),   // Kırmızı (4)
                Color.FromArgb(255, 206, 86),   // Sarı (3)
                Color.FromArgb(75, 192, 192),   // Yeşil-mavi (5)
                Color.FromArgb(201, 203, 207),  // Gri (6)
                Color.FromArgb(54, 54, 127),    // Koyu mavi (7)
                Color.FromArgb(255, 159, 64)    // Turuncu
            };

                    int renkIndex = 0;

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("Veritabanında veri bulunamadı!");
                        return;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        string sirketIsmi = row["Sirketİsmi"].ToString();
                        int adet = Convert.ToInt32(row["Adet"]);

                        var point = new DataPoint();
                        point.YValues = new double[] { adet };
                        point.Color = renkler[renkIndex % renkler.Length];
                        point.LegendText = sirketIsmi; // Legend'da gösterilecek isim
                        point.Label = $"{adet}"; // Pasta üzerindeki sayı
                        point.ToolTip = $"{sirketIsmi}: {adet} kayıt";

                        chart1.Series[0].Points.Add(point);

                        renkIndex++;
                    }

                    // Legend (açıklama) ayarları
                    chart1.Legends[0].Enabled = true;
                    chart1.Legends[0].Docking = Docking.Right;
                    chart1.Legends[0].Font = new Font("Arial", 10);

                    // Label'ları pasta dışında göster
                    chart1.Series[0]["PieLabelStyle"] = "Outside";
                    chart1.Series[0].Font = new Font("Arial", 10, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grafik Yükleme Hatası: " + ex.Message);
            }
        }
    }
    }

    

