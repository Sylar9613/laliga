using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Liga
{
    public partial class FrmEquipo : Liga.FrmHome
    {
        public FrmEquipo()
        {
            InitializeComponent();
        }
        Equipo temp;
        ValidacionControl validar;
        Temporada temporada;
        Equipo_Jugador eq_jug;

        public void LlenarComboBox(string idTemp)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
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
        public void LlenarComboBox2()
        {
            try
            {
                string[] milista = temporada.FindAllIdTemp();
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Clear();
                    comboBox3.Items.Clear();
                }
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox1.Items.Add(milista[i]);
                    comboBox3.Items.Add(milista[i]);
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
        private void FrmEquipo_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 305);
            temp = new Equipo();
            validar = new ValidacionControl();
            temporada = new Temporada();
            eq_jug = new Equipo_Jugador();
            LlenarComboBox2();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
            {
                comboBox2.Items.Clear();
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                LlenarComboBox(comboBox3.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Debe definir una temporada, un equipo con su director y su color", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (temp.BuscarSiExisteDirector(textBox2.Text))
                    {
                        if (temp.BuscarSiExiste(textBox1.Text))
                        {
                            temp.Actualizar(textBox3.Text, textBox1.Text);
                            temp.InsertarDirector_equipo(textBox1.Text, textBox2.Text, comboBox1.Text);
                            eq_jug.Insertar(comboBox1.Text, temp.FindIdForName(textBox1.Text));
                            ActualizarDataGrid();
                            MessageBox.Show("Como el equipo y el DT ya existen se actualizaron sus datos");
                        }
                        else
                        {
                            temp.Insertar(textBox1.Text, textBox3.Text);
                            temp.InsertarDirector_equipo(textBox1.Text, textBox2.Text, comboBox1.Text);
                            eq_jug.Insertar(comboBox1.Text, temp.FindIdForName(textBox1.Text));
                            ActualizarDataGrid();
                            MessageBox.Show("Se han introducido los datos con éxito");
                        }
                    }
                    else
                    {
                        if (temp.BuscarSiExiste(textBox1.Text))
                        {
                            temp.Actualizar(textBox3.Text, textBox1.Text);
                            temp.InsertarDT(textBox2.Text);
                            temp.InsertarDirector_equipo(textBox1.Text, textBox2.Text, comboBox1.Text);
                            eq_jug.Insertar(comboBox1.Text, temp.FindIdForName(textBox1.Text));
                            ActualizarDataGrid();
                            MessageBox.Show("Como el equipo ya existe se actualizaron sus datos");
                        }
                        else
                        {
                            temp.Insertar(textBox1.Text, textBox3.Text);
                            temp.InsertarDT(textBox2.Text);
                            temp.InsertarDirector_equipo(textBox1.Text, textBox2.Text, comboBox1.Text);
                            eq_jug.Insertar(comboBox1.Text, temp.FindIdForName(textBox1.Text));
                            ActualizarDataGrid();
                            MessageBox.Show("Se han introducido los datos con éxito");
                        }
                    }                  
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    LlenarComboBox2();                    
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text) || String.IsNullOrEmpty(comboBox2.Text) || String.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    MessageBox.Show("Debe definir una temporada y un equipo para borrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    temp.Eliminar(comboBox3.Text, comboBox2.Text);

                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    LlenarComboBox2();
                    ActualizarDataGrid();
                    MessageBox.Show("Se han eliminado los datos con éxito");
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActualizarDataGrid();
            if (this.Size == new Size(this.Size.Width, 305))
            {
                this.button3.Text = "Ocultar detalles <<";
                this.Size = new Size(this.Size.Width, 458);
            }
            else
            {
                this.button3.Text = "Mostrar detalles >>";
                this.Size = new Size(this.Size.Width, 305);
            }
            this.CenterToScreen();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyTextAndSpace(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyTextAndSpace(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyTextAndSpace(e);
        }

    }
}
