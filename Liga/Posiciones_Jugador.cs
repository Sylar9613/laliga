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
    public class Posiciones_Jugador
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        Posicion posicion;
        Jugador jugador;
        public Posiciones_Jugador()
        {
            con = new Conexion();
            datos = new DataSet();
            posicion = new Posicion();
            jugador = new Jugador();
        }
        public string[] FindAllID()
        {
            string sql = "Select id_posicion_jugador from posiciones_jugador order by id_posicion_jugador;";
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
            string sql = "Select * from posiciones_jugador;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public void Insertar(string Pos, string Jug)
        {
            string idPos = posicion.FindIDForName(Pos);
            string idJug = jugador.FindIdForName(Jug);
            string sql = "Insert into posiciones_jugador (posicionesid_posiciones,jugadorid_posicion) values ('" + idPos + "','" + idJug + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string pos, string jug)
        {
            string idJug = jugador.FindIdForName(jug);
            string idPos = posicion.FindIDForName(pos);
            string sql = "Update posiciones_jugador set posicionesid_posiciones='" + idPos + "' WHERE jugadorid_posicion='" + idJug + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public string BuscarTalla(string idjugador)
        {
            string sql = "Select talla from jugador where id_jugador='" + idjugador + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }

        public string BuscarPeso(string idjugador)
        {
            string sql = "Select peso from jugador where id_jugador='" + idjugador + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }

        public void Eliminar(string idPos_Jug)
        {

            //Eliminar de la tabla que se desea eliminar el dato en posiciones_jugador

            string sql = "Delete from posiciones_jugador Where id_posicion_jugador='" + idPos_Jug + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
    }
}
