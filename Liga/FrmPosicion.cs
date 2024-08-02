using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmPosicion : Liga.FrmHome
    {
        public FrmPosicion()
        {
            InitializeComponent();
        }
        Posicion temp;

        private void FrmPosicion_Load(object sender, EventArgs e)
        {
            try
            {
                temp = new Posicion();
                dataGridView1.DataSource = temp.SeleccionarTodos().Tables[0];
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
    }
}
