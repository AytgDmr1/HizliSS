using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HızlıSS
{
    public partial class Ayarlar : Form
    {
        static string iniKonumu = AppDomain.CurrentDomain.BaseDirectory + @"\Ayarlar.ini";
        static INIFile ini = new INIFile(iniKonumu);


        public Ayarlar()
        {
            InitializeComponent();
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            textBox1.Text = ini.Read("Ayarlar", "KaydetmeYolu");
            if (ini.Read("Ayarlar", "Bildirim") == "evet")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ini.Write("Ayarlar", "KaydetmeYolu", textBox1.Text);
            if(checkBox1.Checked == true)
            {
                ini.Write("Ayarlar", "Bildirim", "evet");
            }
            else
            {
                ini.Write("Ayarlar", "Bildirim", "hayır");
            }
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog kaydetmeYolu = new FolderBrowserDialog();
            kaydetmeYolu.ShowDialog();
            string KlasorYolu = kaydetmeYolu.SelectedPath;
            textBox1.Text = KlasorYolu;
        }
    }
}
