using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using KlavyeDinle;

namespace HızlıSS
{
    public partial class Form1 : Form
    {
        static string iniKonumu = AppDomain.CurrentDomain.BaseDirectory + @"\Ayarlar.ini";
        static INIFile ini = new INIFile(iniKonumu);

        globalKeyboardHook klavyeDinleyicisi = new globalKeyboardHook();


        public Form1()
        {
            InitializeComponent();
            DinlenecekTuslariAyarla();

        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            

            base.OnLoad(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Form'u sistem tepsisine küçült
            notifyIcon1.Visible = true;

            if (!File.Exists(iniKonumu))
            {

                ini.Write("Ayarlar", "KaydetmeYolu", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                ini.Write("Ayarlar", "Bildirim", "evet");
            }
        }

        public void DinlenecekTuslariAyarla()
        {
            klavyeDinleyicisi.HookedKeys.Add(Keys.PrintScreen);

            klavyeDinleyicisi.KeyUp += new KeyEventHandler(islem1);
        }

        void islem1(object sender, KeyEventArgs e)
        {
            e.Handled = false;

            string yol = ini.Read("Ayarlar", "KaydetmeYolu");
            Image img = Clipboard.GetImage();
            img.Save(yol + @"\Ekran Görüntüsü " + DateTime.Now.ToString("HH.mm.ss dd/mm/yyyy") + ".jpg");

            if(ini.Read("Ayarlar", "Bildirim") == "evet")
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "Ekran Görüntüsü Kaydedildi";
                notifyIcon1.BalloonTipText = "Ekran görüntüsü " + yol + " dizinine kaydedildi.";
                notifyIcon1.ShowBalloonTip(700);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "HızlıSS";
                notifyIcon1.BalloonTipText = "Program sistem tepsisine küçültüldü.";
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar ayarlar = new Ayarlar();
            ayarlar.ShowDialog();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
