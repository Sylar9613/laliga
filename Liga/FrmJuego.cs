using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Liga
{
    public partial class FrmJuego : Liga.FrmHome
    {
        public FrmJuego()
        {
            InitializeComponent();
        }

        ValidacionControl validar;
        Juego temp;
        Equipo equipo;
        Temporada temporada;
        public void VaciarControles()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Clear();
            textBox2.Clear();
        }
        public string DateFormatChange()
        {
            try
            {
                DateTime date = dateTimePicker1.Value;
                var a = String.Format("{0:u}", date);
                string b = a.Substring(0, 10);
                return b;
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
                throw;
            }            
        }
        public void LlenarComboBox(string idTemp)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();
                }
                else
                {
                    if (temporada.BuscarSiTieneElementos(idTemp))
                    {
                        string[] milista = temporada.FindAllEquiposForTemp(idTemp);
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox1.Items.Clear();
                            comboBox2.Items.Clear();
                        }
                        for (int i = 0; i < milista.Length; i++)
                        {
                            comboBox1.Items.Add(milista[i]);
                            comboBox2.Items.Add(milista[i]);
                        }
                    }
                    else
                    {
                        comboBox1.Items.Clear();
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
                    comboBox3.Items.Clear();
                }
                for (int i = 0; i < milista.Length; i++)
                {
                    comboBox3.Items.Add(milista[i]);
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        private void FrmJuego_Load(object sender, EventArgs e)
        {
            validar = new ValidacionControl();
            temp = new Juego();
            equipo = new Equipo();
            temporada = new Temporada();
            LlenarComboBox2();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
            {
                comboBox2.Items.Clear();
                comboBox1.Items.Clear();
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                LlenarComboBox(comboBox3.Text);
            }
        }
        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyNum(sender, e);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.ValidateOnlyNum(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(comboBox2.Text) || String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(comboBox2.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    MessageBox.Show("Debe llenar todos los campos", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (comboBox2.Text == comboBox1.Text)
                {
                    MessageBox.Show("No puede elegir el mismo equipo como visitante y como local", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox2.Text = "";
                }
                else
                {
                    if (temp.BuscarSiExiste(DateFormatChange(), equipo.FindIdForName(comboBox1.Text), equipo.FindIdForName(comboBox2.Text)))
                    {
                        MessageBox.Show("Dos equipos no pueden jugar entre sí más de una vez en un mismo día", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (temp.Buscar(comboBox3.Text,equipo.FindIdForName(comboBox1.Text)))
                        {
                            temp.Insertar(DateFormatChange(), equipo.FindIdForName(comboBox1.Text), equipo.FindIdForName(comboBox2.Text), textBox1.Text, textBox2.Text);
                            temp.Actualizar(comboBox3.Text, equipo.FindIdForName(comboBox1.Text));
                            if (temp.Buscar(comboBox3.Text, equipo.FindIdForName(comboBox2.Text)))
                            {
                                temp.Actualizar(comboBox3.Text, equipo.FindIdForName(comboBox2.Text));
                            }
                            else
                            {
                                temp.Insert(comboBox3.Text, equipo.FindIdForName(comboBox2.Text));
                            }
                            VaciarControles();
                        }
                        else if (temp.Buscar(comboBox3.Text, equipo.FindIdForName(comboBox2.Text)))
                        {
                            temp.Insertar(DateFormatChange(), equipo.FindIdForName(comboBox1.Text), equipo.FindIdForName(comboBox2.Text), textBox1.Text, textBox2.Text);
                            temp.Actualizar(comboBox3.Text, equipo.FindIdForName(comboBox2.Text));
                            if (temp.Buscar(comboBox3.Text, equipo.FindIdForName(comboBox1.Text)))
                            {
                                temp.Actualizar(comboBox3.Text, equipo.FindIdForName(comboBox1.Text));
                            }
                            else
                            {
                                temp.Insert(comboBox3.Text, equipo.FindIdForName(comboBox1.Text));
                            }
                            VaciarControles();
                        }
                        else
                        {
                            temp.Insertar(DateFormatChange(), equipo.FindIdForName(comboBox1.Text), equipo.FindIdForName(comboBox2.Text), textBox1.Text, textBox2.Text);
                            temp.Insert(comboBox3.Text, equipo.FindIdForName(comboBox1.Text));
                            temp.Insert(comboBox3.Text, equipo.FindIdForName(comboBox2.Text));
                            VaciarControles();
                        }
                        MessageBox.Show("Se han introducido los datos con éxito");
                    }                   
                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "HOME";
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "LOCAL";
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = "VISITOR";
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Text = "VISITANTE";
        }
    }
}
