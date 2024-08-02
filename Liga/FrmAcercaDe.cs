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
    public partial class FrmAcercaDe : Form
    {
        public FrmAcercaDe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 254);
        }

        private void FrmAcercaDe_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 152);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 152);
        }
    }
}
