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
    public class Posicion
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        public Posicion()
        {
            con = new Conexion();
            datos = new DataSet();
        }
        public string FindIDForName(string name)
        {
            string sql = "Select id_posiciones from posiciones where nombre_pos='" + name + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public string[] FindAllNames()
        {
            string sql = "Select nombre_pos from posiciones order by id_posiciones;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            string[] codigos = new string[datos.Tables[0].Rows.Count];
            for (int i = 0; i < codigos.Length; i++)
            {
                codigos[i] = datos.Tables[0].Rows[i][0].ToString();
            }
            return codigos;
        }
        public DataSet SeleccionarTodos()
        {
            string sql = "Select id_posiciones as 'ID',nombre_pos as 'Posiciones',caracter as 'Caracteres' from posiciones;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }
    }
}
