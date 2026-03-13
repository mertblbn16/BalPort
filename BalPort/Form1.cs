using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace BalPort
{
    public partial class Form1 : Form
    {
        public SQLiteConnection con = new SQLiteConnection("Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\" +
            "Veritabani1.db");
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KullaniciKayit kk = new KullaniciKayit();
            this.Hide();
            kk.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            string password = textBox3.Text;
            string connectionString = @"Data Source=C:\Users\mertb\source\repos\BalPort\BalPort\bin\Debug\Veritabani1.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM Veritabani1 WHERE KimlikNo = @username AND Sifre = @password";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            Sevkiyatcs sekviyat = new Sevkiyatcs();
                            sekviyat.Show();

                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:" + ex.Message);
            }
        }
    }
}

