using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;

namespace Liga
{
    public class Conexion
    {
        public MySqlConnection MiConexion { get; set; }
        private string conexion = "Server=localhost;User ID=root;Password='';Database=liga";

        public Conexion()
        {
            MiConexion = new MySqlConnection(conexion);
        }

        public void Conectar()
        {
            try
            {
                MiConexion.Open();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        public void Desconectar()
        {
            MiConexion.Close();
        }

        public string ProbarConexion()
        {
            try
            {
                MySqlConnection m = new MySqlConnection(conexion);
                m.Open();
                return conexion;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
