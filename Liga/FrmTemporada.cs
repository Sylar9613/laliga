using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmTemporada : Liga.FrmHome
    {
        public FrmTemporada()
        {
            InitializeComponent();
        }
        Temporada temp;
        ValidacionControl validar;
        public void LlenarComboBox()
        {
            try
            {
                string[] milista = temp.FindAllNames();
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Clear();
                }
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Add(milista[i]);
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        public void ActualizarDataGrid()
        {
            try
            {
                dataGridView1.DataSource = temp.SeleccionarTodos().Tables[0];
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void FrmTemporada_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 264);
            temp = new Temporada();
            validar = new ValidacionControl();
            LlenarComboBox();
        }

        #region DateTimePicker
        string fechaInicio = null;
        DateTime secondInicio;
        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            fechaInicio = dateTimePicker1.Value.ToShortDateString();
            secondInicio = DateTime.Parse(fechaInicio);
            //MessageBox.Show("el tiempo es: " + second.ToShortDateString());
        }

        string fechaTermina = null;
        DateTime second2Termina;
        TimeSpan duration;
        int duracionDias;
        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            if (fechaInicio == null)
            {
                MessageBox.Show("Debe introducir primero una fecha de inicio", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                fechaTermina = dateTimePicker2.Value.ToShortDateString();
                second2Termina = DateTime.Parse(fechaTermina);
                //MessageBox.Show("el tiempo es: " + second2.ToShortDateString());
                duration = second2Termina - secondInicio;
                duracionDias = duration.Days;
                if (duration.Days < 2)
                {
                    MessageBox.Show("La duración de una temporada debe ser de 2 días como mínimo", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fechaTermina = null;
                }
                else
                {
                    MessageBox.Show("La duracion de la temporada es de : " + duration.Days + " día(s)");
                }
            }
        }        
        #endregion

        #region Controles
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                btnBorrar.Visible = true;
                comboBox1.Visible = true;
                label4.Visible = true;
            }
            else
            {
                btnBorrar.Visible = false;
                comboBox1.Visible = false;
                label4.Visible = false;
                comboBox1.Text = "";
            }
        }
        #endregion

        #region Botones
        private void btnGenerarCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaTermina))
                {
                    MessageBox.Show("Debe definir una fecha de inicio y una fecha de terminación de la temporada", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string slash = "/";
                    string fcod1 = fechaInicio.Substring(0, 2);
                    string fcod2 = fechaInicio.Substring(3, 2);
                    string fcod3 = fechaInicio.Substring(6, fechaInicio.Length - 6);

                    string scod1 = fechaTermina.Substring(0, 2);
                    string scod2 = fechaTermina.Substring(3, 2);
                    string scod3 = fechaTermina.Substring(6, fechaTermina.Length - 6);

                    string fcod = fcod3 + slash + fcod2 + slash + fcod1;
                    string scod = scod3 + slash + scod2 + slash + scod1;

                    fechaInicio = fcod;
                    fechaTermina = scod;
                    MessageBox.Show("La fecha de inicio es: " + fechaInicio + ".\nLa fecha de terminación es: " + fechaTermina + ".");

                    string fIni = fechaInicio.Replace("/", "");
                    string fTerm = fechaTermina.Replace("/", "");
                    textBox1.Text = fIni + fTerm;
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    MessageBox.Show("Debe definir un código para borrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    temp = new Temporada();
                    temp.Eliminar(comboBox1.Text);
                    comboBox1.Text = "";
                    LlenarComboBox();
                    ActualizarDataGrid();
                    MessageBox.Show("Se han eliminado los datos con éxito");
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaTermina) || String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Debe definir una fecha de inicio y una fecha de terminación de la temporada y generar un código", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    temp = new Temporada();
                    temp.Insertar(fechaInicio, fechaTermina, textBox1.Text);
                    textBox1.Clear();
                    fechaInicio = null;
                    fechaTermina = null;
                    LlenarComboBox();
                    ActualizarDataGrid();
                    MessageBox.Show("Se han introducido los datos con éxito");
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ActualizarDataGrid();
            if (this.Size == new Size(this.Size.Width, 264))
            {
                this.button1.Text = "Ocultar detalles <<";
                this.Size = new Size(this.Size.Width, 392);
            }
            else
            {
                this.button1.Text = "Mostrar detalles >>";
                this.Size = new Size(this.Size.Width, 264);
            }
            this.CenterToScreen();
        }
    }
}
