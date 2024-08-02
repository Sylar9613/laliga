using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmJugador : Liga.FrmHome
    {
        public FrmJugador()
        {
            InitializeComponent();
        }
        Jugador temp;
        Temporada temporada;
        Equipo equipo;
        Equipo_Jugador eq_jug;
        Posicion pos;
        Posiciones_Jugador pos_jug;
        ValidacionControl validar;
        string alias = null;
        public void LlenarComboBoxTemp()
        {
            try
            {
                string[] milista = temporada.FindAllIdTemp();
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Clear();
                    comboBox6.Items.Clear();
                }
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Add(milista[i]);
                    comboBox6.Items.Add(milista[i]);
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        public void LlenarComboBoxPos()
        {
            try
            {
                string[] milista = pos.FindAllNames();
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox4.Items.Clear();
                }
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox4.Items.Add(milista[i]);
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        public void LlenarComboBoxEquipo(string idTemp)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    comboBox2.Items.Clear();
                }
                else
                {
                    if (temporada.BuscarSiTieneElementos(idTemp))
                    {
                        string[] milista = temporada.FindAllEquiposForTemp(idTemp);
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox2.Items.Clear();
                        }
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox2.Items.Add(milista[i]);
                        }
                    }
                    else
                    {
                        comboBox2.Items.Clear();
                    }
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        public void LlenarComboBoxJugador(string idTemp)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox6.Text) || String.IsNullOrWhiteSpace(comboBox6.Text))
                {
                    comboBox3.Items.Clear();
                }
                else
                {
                    if (temporada.BuscarSiTieneElementos(idTemp))
                    {
                        string[] milista = temporada.FindAllJugForTemp(idTemp);
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox3.Items.Clear();
                        }
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox3.Items.Add(milista[i]);
                        }
                    }
                    else
                    {
                        comboBox3.Items.Clear();
                    }
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
        private void FrmJugador_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 318);
            temp = new Jugador();
            temporada = new Temporada();
            equipo = new Equipo();
            eq_jug = new Equipo_Jugador();
            pos = new Posicion();
            pos_jug = new Posiciones_Jugador();
            validar = new ValidacionControl();
            LlenarComboBoxTemp();
            LlenarComboBoxPos();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.Visible = true;
                alias = textBox2.Text;
            }
            else
            {
                textBox2.Visible = false;
                textBox2.Clear();
                alias = null;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox2.Items.Clear();
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                LlenarComboBoxEquipo(comboBox1.Text);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ActualizarDataGrid();
            if (this.Size == new Size(this.Size.Width, 318))
            {
                this.button3.Text = "Ocultar detalles <<";
                this.Size = new Size(this.Size.Width, 506);
            }
            else
            {
                this.button3.Text = "Mostrar detalles >>";
                this.Size = new Size(this.Size.Width, 318);
            }
            this.CenterToScreen();
        }
        private void comboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox6.Text) || String.IsNullOrWhiteSpace(comboBox6.Text))
            {
                comboBox3.Items.Clear();
            }
            else
            {
                comboBox3.Items.Clear();
                comboBox3.Text = "";
                LlenarComboBoxJugador(comboBox6.Text);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox6.Text) || String.IsNullOrWhiteSpace(comboBox6.Text) || String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    MessageBox.Show("Debe definir una temporada y un jugador para borrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    temp.Eliminar(comboBox6.Text,comboBox3.Text);
                    comboBox6.Text = "";
                    comboBox3.Text = "";
                    ActualizarDataGrid();
                    MessageBox.Show("Se han eliminado los datos con éxito");
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(comboBox2.Text) || String.IsNullOrEmpty(comboBox4.Text) || String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Debe llenar todos los campos", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (String.IsNullOrEmpty(textBox2.Text)||String.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        alias = null;
                    }
                    else
                    {
                        alias = textBox2.Text;
                    }
                    if (temp.BuscarSiExiste(textBox1.Text))
                    {
                        if (eq_jug.Buscar(comboBox1.Text, comboBox2.Text))
                        {
                            temp.Actualizar(textBox1.Text, alias, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), numericUpDown3.Value.ToString());
                            eq_jug.Insert(comboBox1.Text, comboBox2.Text, textBox1.Text);
                            pos_jug.Actualizar(comboBox4.Text, textBox1.Text);
                            textBox1.Clear();
                            textBox2.Clear();
                            comboBox1.Text = "";
                            comboBox2.Text = "";
                            comboBox4.Text = "";
                            numericUpDown1.Value = numericUpDown1.Minimum;
                            numericUpDown2.Value = numericUpDown2.Minimum;
                            numericUpDown3.Value = numericUpDown3.Minimum;
                            ActualizarDataGrid();
                            MessageBox.Show("Como el jugador ya existe se han actualizado sus datos");
                        }
                        else
                        {
                            temp.Actualizar(textBox1.Text, alias, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), numericUpDown3.Value.ToString());
                            eq_jug.Actualizar(comboBox1.Text, comboBox2.Text, textBox1.Text);
                            pos_jug.Actualizar(comboBox4.Text, textBox1.Text);
                            textBox1.Clear();
                            textBox2.Clear();
                            comboBox1.Text = "";
                            comboBox2.Text = "";
                            comboBox4.Text = "";
                            numericUpDown1.Value = numericUpDown1.Minimum;
                            numericUpDown2.Value = numericUpDown2.Minimum;
                            numericUpDown3.Value = numericUpDown3.Minimum;
                            ActualizarDataGrid();
                            MessageBox.Show("Como el jugador ya existe se han actualizado sus datos");
                        }
                    }
                    else
                    {
                        if (eq_jug.Buscar(comboBox1.Text, comboBox2.Text))
                        {
                            temp.Insertar(textBox1.Text, alias, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), numericUpDown3.Value.ToString());
                            eq_jug.Insert(comboBox1.Text, comboBox2.Text, textBox1.Text);
                            pos_jug.Insertar(comboBox4.Text, textBox1.Text);
                            textBox1.Clear();
                            textBox2.Clear();
                            comboBox1.Text = "";
                            comboBox2.Text = "";
                            comboBox4.Text = "";
                            numericUpDown1.Value = numericUpDown1.Minimum;
                            numericUpDown2.Value = numericUpDown2.Minimum;
                            numericUpDown3.Value = numericUpDown3.Minimum;
                            ActualizarDataGrid();
                            MessageBox.Show("Se han introducido los datos con éxito");
                        }
                        else
                        {
                            temp.Insertar(textBox1.Text, alias, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), numericUpDown3.Value.ToString());
                            eq_jug.Actualizar(comboBox1.Text, comboBox2.Text, textBox1.Text);
                            pos_jug.Insertar(comboBox4.Text, textBox1.Text);
                            textBox1.Clear();
                            textBox2.Clear();
                            comboBox1.Text = "";
                            comboBox2.Text = "";
                            comboBox4.Text = "";
                            numericUpDown1.Value = numericUpDown1.Minimum;
                            numericUpDown2.Value = numericUpDown2.Minimum;
                            numericUpDown3.Value = numericUpDown3.Minimum;
                            ActualizarDataGrid();
                            MessageBox.Show("Se han introducido los datos con éxito");
                        }
                    }
                }

            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox6_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyTextAndSpace(e);
        }
    }
}
