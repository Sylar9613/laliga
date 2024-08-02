using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmHomeDefault : Liga.FrmHome
    {
        public FrmHomeDefault()
        {
            InitializeComponent();
        }

        private void FrmHomeDefault_Load(object sender, EventArgs e)
        {
            CambiarFondos();
        }
        public void CambiarFondos()
        {
            Random a = new Random();
            int b = a.Next(1, 8);
            if (b == 1)
            {
                this.BackColor = Color.Black;
            }
            if (b == 2)
            {
                this.BackColor = Color.DodgerBlue;
            }
            if (b == 3)
            {
                this.BackColor = Color.DarkViolet;
            }
            if (b == 4)
            {
                this.BackColor = Color.Red;
            }
            if (b == 5)
            {
                this.BackColor = Color.Crimson;
            }
            if (b == 6)
            {
                this.BackColor = Color.Lime;
            }
            if (b == 7)
            {
                this.BackColor = Color.Yellow;
            }
        }
    }
}
