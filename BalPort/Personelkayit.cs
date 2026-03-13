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
using System.Linq.Expressions;

namespace BalPort
{
    public partial class KullaniciKayit : Form
    {
        public KullaniciKayit()
        {
            InitializeComponent();
        }

        public SQLiteConnection con = new SQLiteConnection("Data Source=C:\\Users\\mertb\\source\\repos\\BalPort\\BalPort\\bin\\Debug\\Veritabani1.db");


        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e) //Yeni kayıt
        {
            try
            {
                if (textBox4.Text == " " || textBox5.Text == " ")
                {

                    MessageBox.Show("Tüm alanları doldurunuz");

                }
                else if (textBox5.Text != textBox6.Text)
                {
                    MessageBox.Show("Şifre tekrarı eşleşmiyor !");
                }

                else
                {

                    con.Open();
                   // int KimlikNoK = Convert.ToInt32(textBox4.Text);
                    string sql = "insert into Veritabani1(Sirketİsmi, Ad, Soyad, KimlikNo, Sifre,SifreTekrar) Values ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + textBox3.Text + "' , '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";
                    SQLiteCommand komutislet = new SQLiteCommand(sql, con);
                    komutislet.ExecuteNonQuery();

                    con.Close(); // bağlantıyı kapat
                    MessageBox.Show("Yeni personel kaydı başarılı!");
                    textBox1.Text = string.Empty; //textBoxı temizle
                    textBox2.Text = string.Empty; //textBoxı temizle
                    textBox3.Text = string.Empty; //textBoxı temizle
                    textBox4.Text = string.Empty; //textBoxı temizle
                    textBox5.Text = string.Empty; //textBoxı temizle
                    textBox6.Text = string.Empty;
                    this.Hide();
                    Form f1 = new Form1();
                    f1.Show();




                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı İşlem Yaptınız !");

            }
            finally
            {
                con.Close();
            }






        }












    }
}

