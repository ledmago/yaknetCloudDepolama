using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Yaknet
{
    public partial class intro : Form
    {
        public intro()
        {
            InitializeComponent();
        }
        public string txtyaz(string adres, string yazilacak)
        {
            string yol = yolbelirle();
             StreamWriter SW = new StreamWriter(yol + @":\\SystemDirectory\" + adres);
             SW.WriteLine(yazilacak);
             SW.Close();
             return null;
   
        }




        public void guncellemeyap()
        { 
            string yol = yolbelirle();

            // Veri Çekme İşlemi

            string kimlik = txtoku(yol + @":\\SystemDirectory\MrLed.dll");
            kimlik = StringCipher.Decrypt(kimlik, "firat934"); // Kimliği Normal yazıya Çevir Encryptr yap yani çöz
            string detay = nettencek(Yaknet.config.Confingadres + "vericek.php?kimlik=" + kimlik + "&islem=detay");
            string isim = nettencek(Yaknet.config.Confingadres + "vericek.php?kimlik=" + kimlik + "&islem=isim");
            string sifre = nettencek(Yaknet.config.Confingadres + "vericek.php?kimlik=" + kimlik + "&islem=sifre");
            
            // string encryptedstring = StringCipher.Encrypt(kadi, sifre);
            //string decryptedstring = StringCipher.Decrypt(kadi, sifre);

            detay = StringCipher.Encrypt(detay, "firat934");
            sifre = StringCipher.Encrypt(sifre, "firat934");

            //Güncelleme İşlemi

            txtyaz("opens.dll", detay);
            txtyaz("chrotable.dll", isim);
            txtyaz("meotak.dll", sifre);

            //sifre meotak
            //chrotable isim
            //MrLed Kimkik
            //Opens Detay

            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }


        public string nettencek(string adres)
        {
            string baslik = "";
            try
            {
                WebRequest istek = HttpWebRequest.Create(adres); //2
                WebResponse cevap; //3
                cevap = istek.GetResponse(); //4
                StreamReader donenBilgiler = new StreamReader(cevap.GetResponseStream()); //5
                string gelen = donenBilgiler.ReadToEnd(); //6
                int titleIndexBaslangici = gelen.IndexOf("<body>") + 7; //7
                int titleIndexBitisi = gelen.Substring(titleIndexBaslangici).IndexOf("</body>"); //8
                baslik = gelen.Substring(titleIndexBaslangici, titleIndexBitisi);


            }
            catch
            {
                baslik = "net";
            }
            return baslik;
        }
        public string txtoku(string yolal)
        {
            string veriaktar = "";
            try
            {
                StreamReader oku;


                oku = File.OpenText(yolal);

                string yazi;


                while ((yazi = oku.ReadLine()) != null)
                {

                    veriaktar = yazi.ToString();
                }


                oku.Close();
                return veriaktar;
            }
            catch
            {
                MessageBox.Show("YakNet USB Sürücüsü Takılı Değil !", "Dikkat");

                Application.Exit();
            }
            return veriaktar;
        }
        public string yolbelirle()
        {
            char harfler = 'A';
            string mama = "";
            for (int x = 0; x < 27; x++)
            {




                if (File.Exists(harfler.ToString() + ":\\SystemDirectory\\chrotable.dll"))
                {

                    mama = harfler.ToString();
                    break;


                }
                else
                {
                    harfler++;
                }

            }

            string harfler2 = mama;
            return harfler2;

        }
        private void intro_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Opacity = 0.9;



           


            

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public string adres { get; set; }
        int saniye = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye += 1;
            if (saniye > 2)
            {


                timer1.Stop();

                string yol = yolbelirle();
                string kimlik = txtoku(yol + @":\\SystemDirectory\MrLed.dll");
                string sonuc = StringCipher.Decrypt(kimlik, "firat934");

                string guncelleme = nettencek("http://zeonnn.com/agar/internet_kontrol.php");


                if (guncelleme == "net")
                {
                    Form1 frm1 = new Form1();
                    this.Opacity = 0;
                    frm1.Show();
                    this.Hide();
                   

                }
                else
                {
                    guncellemeyap();

                }



            }
        }
    }
}
