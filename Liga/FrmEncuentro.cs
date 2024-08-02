using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmEncuentro : Liga.FrmHome
    {
        public FrmEncuentro()
        {
            InitializeComponent();
        }
        Juego temp;
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Size == new Size(748, this.Size.Height))
            {
                this.button1.Text = "Ver cronograma";
                dataGridView1.DataSource = temp.VerTabla().Tables[0];
                dataGridView1.Size = new Size(360, 190);
                dataGridView2.Location = new Point(430, 55);
                this.Size = new Size(670, this.Size.Height);
                button1.Location = new Point(265, 275);
            }
            else
            {
                this.button1.Text = "Ver tabla de posiciones";
                dataGridView1.DataSource = temp.SeleccionarTodos().Tables[0];
                dataGridView2.Location = new Point(510, 55);
                dataGridView1.Size = new Size(442, 190);
                this.Size = new Size(748, this.Size.Height);
                button1.Location = new Point(347, 275);
            }
            this.CenterToScreen();
        }

        private void FrmEncuentro_Load(object sender, EventArgs e)
        {
            temp = new Juego();
            dataGridView1.Size = new Size(442, 190);
            dataGridView2.Location = new Point(510, 55);
            button1.Text = "Ver tabla de posiciones";
            button1.Location = new Point(347, 275);
            this.Size = new System.Drawing.Size(748, 366);
            //dataGridView1.DataSource = temp.SeleccionarTodos().Tables[0];
            dataGridView1.DataSource = temp.SeleccionarTodos().Tables[0];
            dataGridView2.DataSource = temp.VerEquipos().Tables[0];
        }
    }
}
