using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent(); 
            progressBar1.PerformStep();
        }
        Random r = new Random();
        int n;
        private FrmHomeDefault frm;
        private void timerFondos_Tick(object sender, EventArgs e)
        {
            MoverFondos(timerFondos);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarHora();
            if (progressBar1.Value < timer1.Interval)
            {
                progressBar1.PerformStep();
            }
            if (progressBar1.Value == progressBar1.Maximum)
            {
                if (frm == null)
                {
                    frm = new FrmHomeDefault();
                    this.Visible = false;
                    frm.ShowDialog();
                    frm.Dispose();
                    this.Close();
                    Application.ExitThread();
                }
            }
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {
            for (int i = progressBar1.Value; i < timer1.Interval; i++)
            {
                n = r.Next(50, 150);
                progressBar1.Step = n;
            }
            MostrarHora();
            MoverFondos(timerFondos);
        }
        Random ran = new Random();
        int fondo;
        public void MoverFondos(Timer crono)
        {
            fondo = 0;
            fondo = ran.Next(0, 10);
            if (fondo == 0)
            {
                this.BackgroundImage = global::Liga.Properties.Resources._2010_fifa_world_cup_football_stadium_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 1)
            {
                this.BackgroundImage = global::Liga.Properties.Resources._2010_fifa_world_cup_south_africa_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 2)
            {
                this.BackgroundImage = global::Liga.Properties.Resources._2010_fifa_world_cup_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 3)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.cr7_3_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 4)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.lionel_messi_by_joaodesign_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 5)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.maribor_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 6)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.goal_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 7)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.football_stadium_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 8)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.football_2_wallpaper_1920x1080;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            if (fondo == 9)
            {
                this.BackgroundImage = global::Liga.Properties.Resources.football_10_wallpaper_1280x1024;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
        }
        public void MostrarHora()
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void FrmStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                progressBar1.Step = timer1.Interval;
            }
        }
    }
}
