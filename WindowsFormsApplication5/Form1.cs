/*
 * Başla butonu sayıları üretir, Yerleştir butonu sayıları ilgili algoritmaya göre yerleştirir. 
 *Packing factor girilen sayı büyüdükçe %90'a yaklaşıyor. 
 * 3 algoritma için 4'er fonksiyon var.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int sayi;
        public int tabloboyut;
        public int[] dizi;
        int[] dizilisch;
        int[] linklisch;
        int[] dizieisch;
        int[] linkeisch;
        int[] dizilich;
        int[] linklich;
        int p;

        private void button1_Click(object sender, EventArgs e)//sayıları atayıp ekrana yaz
        {
            Random x;
            sayi = Convert.ToInt32(boyutbox.Text);
            if (sayi >= 0 && sayi < 900)
            {
                dizi = new int[sayi];
                x = new Random();
                for (int i = 0; i < sayi; i++)//sayıların atanması
                {
                    dizi[i] = x.Next(10, 1000);
                }
                /*
                dizi[0] = 27;
                dizi[1] = 18;
                dizi[2] = 29;
                dizi[3] = 28;
                dizi[4] = 39;
                dizi[5] = 13;
                dizi[6] = 16;
                dizi[7] = 42;
                dizi[8] = 17;*/
                for (int i = 0; i < sayi; i++)//sayıları texboxa yazma
                {
                    sayilarbox.Text += dizi[i] + "\n";
                }
                boyutayarla();
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lütfen 1 ile 900 arasında bir sayı giriniz");
                Application.Restart();
            }
        }

        public void boyutayarla()//girilen sayıdan bi sonraki asal sayıyı bulacak
        {
            tabloboyut = (sayi*10)/9;//packing factor 0.9 olsun diye
            while (true)
            {
                if (asalmi(tabloboyut))
                {
                    break;
                }
                tabloboyut++;
            }
            dizilisch = new int[tabloboyut];
            linklisch = new int[tabloboyut];
            for (int i = 0; i < linklisch.Length; i++)//Linkin boş olduğunu kontrol etmek için. -1 ise link yok
                linklisch[i] = -1;
            dizieisch = new int[tabloboyut];
            linkeisch = new int[tabloboyut];
            for (int i = 0; i < linkeisch.Length; i++)//Linkin boş olduğunu kontrol etmek için. -1 ise link yok
                linkeisch[i] = -1;
            dizilich = new int[tabloboyut];
            linklich = new int[tabloboyut];
            for (int i = 0; i < linklich.Length; i++)//Linkin boş olduğunu kontrol etmek için. -1 ise link yok
                linklich[i] = -1;
            p = (sayi * 86)/100;
            label4.Text += "%"+((float)sayi/(float)tabloboyut)*100;
        }
        public bool asalmi(int a)
        {
            for (int i = 2; i <= a; i++)
            {
                int kalan = a % i;
                if (kalan == 0)
                {
                    return false;
                }
                if (i == sayi - 1)
                {
                    return true;
                }
            }
            return false;

        }

        //LISCH    
        public void lisch(int gelen)
        {
            int son = tabloboyut - 1;
            int sira = gelen % tabloboyut;
            int yerlesmeyeri;
            int sonlink = -1;
            if (dizilisch[sira] == 0)// asıl yer boşsa
            {
                yerlesmeyeri = sira;
            }
            else
            {
                while (dizilisch[son] != 0)
                {
                    son--;
                }
                yerlesmeyeri = son;
                int gecici = sira;
                while (linklisch[gecici] != -1)
                {
                    gecici = linklisch[gecici];
                }
                sonlink = gecici;
            }
            dizilisch[yerlesmeyeri] = gelen;
            if (sonlink != -1)
                linklisch[sonlink] = yerlesmeyeri;
        }
        public void lischyaz()
        {
            for (int i = 0; i < dizi.Length; i++)
                lisch(dizi[i]);
            for (int i = 0; i < tabloboyut; i++)
            {
                richTextBox1.Text += i + "\n";
                lischbox.Text += dizilisch[i] + "\n";
                lischbox2.Text += linklisch[i] + "\n";
            }
            ortaprobe();
        }
        public int lischarama(int aranan)
        {
            int mod = aranan % tabloboyut;
            int probe = 0;
            if (dizilisch[mod] == aranan)
            {
                probe++;
                son1label.Text = "Sonuç bulundu";
                lischprobe.Text ="Probe Sayısı:"+ probe.ToString();
                return probe;
            }
            else
            {
                probe++;
                bool varyok=true;
                while (dizilisch[mod] != aranan)
                {
                    if (linklisch[mod] != -1)//link boş değilse
                    {
                        mod = linklisch[mod];
                        probe++;
                    }
                    else//link boş
                    {
                        varyok = false;
                        break;
                    }
                }
                if (varyok)
                    son1label.Text = "Sonuç Bulundu.";
                else
                    son1label.Text = "Sonuç Bulunamadı.";
                lischprobe.Text = "Probe Sayısı:"+ probe.ToString();
                return probe;
            }
        }
        public void ortaprobe()
        {
            lischprobe.Visible = false;
            son1label.Visible = false;

            int topprobe=0;
            for (int i = 0; i < dizi.Length; i++)
            {
                topprobe += lischarama(dizi[i]);
            }
            label9.Text = ((double)topprobe / (double)dizi.Length).ToString();
        }


        //EISCH
        public void eisch(int gelen)
        {
            int son = tabloboyut - 1;
            int sira = gelen % tabloboyut;
            int yerlesmeyeri;
            if (dizieisch[sira] == 0)// asıl yer boşsa
            {
                yerlesmeyeri = sira;
            }
            else
            {
                while (dizieisch[son] != 0)
                {
                    son--;
                }
                yerlesmeyeri = son;
                if (linkeisch[sira] == -1)
                {
                    linkeisch[sira] = son;
                }
                else
                {
                    linkeisch[yerlesmeyeri] = linkeisch[sira];
                    linkeisch[sira] = yerlesmeyeri;
                }
            }
            dizieisch[yerlesmeyeri] = gelen;
        }
        public void eischyaz()
        {
            for (int i = 0; i < dizi.Length; i++)
                eisch(dizi[i]);
            for (int i = 0; i < tabloboyut; i++)
            {
                richTextBox4.Text += i + "\n";
                richTextBox2.Text += dizieisch[i] + "\n";
                richTextBox3.Text += linkeisch[i] + "\n";
            }
            ortaprobe2();
        }
        public int eischarama(int aranan)
        {
            int mod = aranan % tabloboyut;
            int probe = 0;
            if (dizieisch[mod] == aranan)
            {
                probe++;
                son2label.Text = "Sonuç bulundu";
                eischprobe.Text = "Probe Sayısı:"+probe.ToString();
                return probe;
            }
            else
            {
                probe++;
                bool varyok = true;
                while (dizieisch[mod] != aranan)
                {
                    if (linkeisch[mod] != -1)//link boş değilse
                    {
                        mod = linkeisch[mod];
                        probe++;
                    }
                    else//link boş
                    {
                        varyok = false;
                        break;
                    }
                }
                if (varyok)
                    son2label.Text = "Sonuç Bulundu.";
                else
                    son2label.Text = "Sonuç Bulunamadı.";

                eischprobe.Text = "Probe Sayısı:" + probe.ToString();
                return probe;
            }
        }
        public void ortaprobe2()
        {
                eischprobe.Visible = false;
                son2label.Visible = false;
                
                int topprobe = 0;
                for (int i = 0; i < dizi.Length; i++)
                {
                    topprobe += eischarama(dizi[i]);
                }
                eichort.Text = ((double)topprobe / (double)dizi.Length).ToString();
        }

        //LICH
        public void lich(int gelen)
        {
            int sira = gelen % p;
            int son = tabloboyut - 1;
            int yerlesmeyeri=0;
            bool dolu=false ;
            label10.Text = "["+p+"-"+son+"]"+" Overflow area";
            //dizilich[p] ve dizilich[son] overflow
            if (!dolu)
            {
                if (dizilich[sira] == 0)
                {
                    yerlesmeyeri = sira;
                }
                else
                {
                    for (int i = son; i >= p; i--)
                    {
                        if (dizilich[i] == 0)
                        {
                            yerlesmeyeri = i;
                            linklich[sira] = yerlesmeyeri;
                            if (i == p)
                            {
                                dolu = true;
                            }
                            break;
                        }
                    }
                }
                dizilich[yerlesmeyeri] = gelen;
            }
            else {
                dolubox.Text = "Overflow area doldu";
            }

        }
        public void lichyaz()
        {
            for (int i = 0; i < dizi.Length; i++)
                lich(dizi[i]);
            for (int i = 0; i < tabloboyut; i++)
            {
                richTextBox7.Text += i + "\n";
                richTextBox5.Text += dizilich[i] + "\n";
                richTextBox6.Text += linklich[i] + "\n";
            }
            ortaprobe3(); 
        }
        public int licharama(int aranan)
        {
            int mod = aranan % p;
            int probe = 0;
            if (dizilich[mod] == aranan)
            {
                probe++;
                lichprobe.Text ="Probe Sayısı:"+ probe.ToString();
                son3label.Text = "Sonuç bulundu.";
                return probe;
            }
            else
            {
                probe++;

                if (linklich[mod] != -1)//link boş değilse
                {
                    probe++;
                    if (dizilich[linklich[mod]] == aranan)
                    {
                        lichprobe.Text = "Probe Sayısı:" + probe.ToString();
                    }
                    else
                    {
                        lichprobe.Text = "Probe Sayısı:" + probe.ToString();
                        son3label.Text = "Sayı bulunamadı.";
                    }
                }
                else
                {
                    lichprobe.Text = "Probe Sayısı:" + probe.ToString();
                    son3label.Text = "Sayı bulunamadı.";
                }
                return probe;
            }
        }
        public void ortaprobe3()
        {
            lichprobe.Visible = false;
            son3label.Visible = false;
            int topprobe = 0;
            for (int i = 0; i < dizi.Length; i++)
            {
                topprobe += licharama(dizi[i]);
            }
            ortlich.Text = ((double)topprobe / (double)dizi.Length).ToString();
        }


        private void button2_Click(object sender, EventArgs e)//ekrana yazdıracak buton
        {
            lischyaz();
            eischyaz();
            lichyaz();
            button2.Enabled = false;
        }
        private void arabuton_Click(object sender, EventArgs e)
        {
        
            lischarama(Convert.ToInt32(textBox1.Text));
            eischarama(Convert.ToInt32(textBox1.Text));
            licharama(Convert.ToInt32(textBox1.Text));
            son1label.Visible = true;
            son2label.Visible = true;
            son3label.Visible = true;
            lischprobe.Visible = true;
            eischprobe.Visible = true;
            lichprobe.Visible = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            son1label.Text = "";
            son2label.Text = "";
            son3label.Text = "";
            lischprobe.Text = "";
            eischprobe.Text = "";
            lichprobe.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
