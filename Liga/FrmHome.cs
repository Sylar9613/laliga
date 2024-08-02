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
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void probarConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conexion m = new Conexion();
            if (m.ProbarConexion() != null)
            {
                MessageBox.Show("Hay conexión");
                m.Desconectar();
            }
            else
            {
                MessageBox.Show("No hay conexión");
            }
        }

        #region Funciones Help y AcercaDe
        private void MostrarAcercaDe()
        {
            FrmAcercaDe frm = new FrmAcercaDe();
            frm.ShowDialog();
            frm.Dispose();
        }
        private void MostrarHelp()
        {
            FrmHelp frm = new FrmHelp();
            frm.ShowDialog();
            frm.Dispose();
        }
        #endregion
        
        private void FrmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmSalida frm = new FrmSalida();
            DialogResult respueta = frm.ShowDialog();
            if (respueta == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        #region Forms Funciones
        FrmHome frmHD;
        private void MostrarHomeDefault()
        {
            if (frmHD == null)
            {
                frmHD = new FrmHomeDefault();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmHomeDefault();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarTemporadas()
        {
            if (frmHD == null)
            {
                frmHD = new FrmTemporada();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmTemporada();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarEquipos()
        {
            if (frmHD == null)
            {
                frmHD = new FrmEquipo();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmEquipo();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarEncuentros()
        {
            if (frmHD == null)
            {
                frmHD = new FrmEncuentro();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmEncuentro();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarJuegos()
        {
            if (frmHD == null)
            {
                frmHD = new FrmJuego();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmJuego();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarJugadores()
        {
            if (frmHD == null)
            {
                frmHD = new FrmJugador();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmJugador();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarPosiciones()
        {
            if (frmHD == null)
            {
                frmHD = new FrmPosicion();
                this.Visible = false;
                frmHD.ShowDialog();
                frmHD.Dispose();
                Application.ExitThread();
            }
            else
            {
                if (frmHD.IsDisposed)
                {
                    frmHD = new FrmPosicion();
                    frmHD.Show();
                    this.Close();
                }
                else
                {
                    frmHD.Activate();
                }
            }
        }
        private void MostrarConsultas()
        {
            FrmConsulta frm = new FrmConsulta();
            frm.ShowDialog();
            frm.Dispose();
        }
        #endregion

        #region Botones MenuStrip
        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarConsultas();
        }
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarHomeDefault();
        }
        private void temporadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarTemporadas();
        }
        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarEquipos();
        }
        private void jugadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarJugadores();
        }
        private void encuentrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarEncuentros();
        }
        private void juegosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarJuegos();
        }
        private void verPosionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarPosiciones();
        }
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarAcercaDe();
        }
        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarHelp();
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmHome_Load(object sender, EventArgs e)
        {
            MostrarHora();
        }
        public void MostrarHora()
        {
            this.Text = "La Liga BBVA - " + DateTime.Now.ToShortDateString() +
                " - " + DateTime.Now.ToLongTimeString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarHora();
        }
    }
}
