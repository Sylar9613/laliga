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
    public partial class FrmConsulta : Form
    {
        public FrmConsulta()
        {
            InitializeComponent();
        }

        ConsultaSQL temp;
        ValidacionControl validar;
        Juego juego;
        Equipo equipo;
        Temporada temporada;
        public void LlenarComboBox()
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
        public void LlenarComboBox1(string idTemp)
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
        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            MostrarHora();
            temporada = new Temporada();
            equipo = new Equipo();
            juego = new Juego();
            temp = new ConsultaSQL();
            validar = new ValidacionControl();
            LlenarComboBox();
            dataGridView1.Hide();
            label1.Visible = true;
            label2.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            button1.Visible = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox2.Text) || String.IsNullOrWhiteSpace(comboBox2.Text) || String.IsNullOrEmpty(comboBox3.Text) || String.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    MessageBox.Show("Debe definir un nombre de equipo y un id de temporada para borrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    equipo.Eliminar(comboBox3.Text, comboBox2.Text);
                    LlenarComboBox();
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    MessageBox.Show("Se han eliminado los datos con éxito");
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

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            validar.NoPermitirEscritura(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarHora();
        }

        public void MostrarHora()
        {
            this.Text = "Consultas - " + DateTime.Now.ToShortDateString() +
                " - " + DateTime.Now.ToLongTimeString();
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
                LlenarComboBox1(comboBox3.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView1.DataSource = temp.consulta1(comboBox1.Text).Tables[0];
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(comboBox1.Text))
            {
                if (radioButton1.Checked)
                {
                    dataGridView1.DataSource = temp.consulta1(comboBox1.Text).Tables[0];
                }
                else
                {
                    dataGridView1.ClearSelection();

                }
            }
            else
            {
                MessageBox.Show("Debe escribir un id de temporada");
            }  
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                dataGridView1.DataSource = temp.consulta2().Tables[0];
            }
            else
            {
                dataGridView1.ClearSelection();

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                dataGridView1.DataSource = temp.consulta3().Tables[0];
            }
            else
            {
                dataGridView1.ClearSelection();

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                dataGridView1.DataSource = temp.consulta4().Tables[0];
            }
            else
            {
                dataGridView1.ClearSelection();

            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                dataGridView1.DataSource = temp.consulta5().Tables[0];
            }
            else
            {
                dataGridView1.ClearSelection();

            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                dataGridView1.Hide();
                label1.Visible = true;
                label2.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                button1.Visible = true;
            }
            else
            {
                dataGridView1.Show();
                label1.Visible = false;
                label2.Visible = false;
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox3.Text = "";
                comboBox2.Text = "";
                button1.Visible = false;
            }
        }

    }
}
