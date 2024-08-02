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
    public class Equipo_Jugador
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        Equipo equipo;
        Jugador jugador;
        public Equipo_Jugador()
        {
            con = new Conexion();
            datos = new DataSet();
            equipo = new Equipo();
            jugador = new Jugador();
        }
        public string[] FindAllID()
        {
            string sql = "Select id_jugador_equipo from equipo_jugador order by id_jugador_equipo;";
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
        public string CogerLastId()
        {
            return FindAllID()[FindAllID().Length - 1];
        }
        public DataSet SeleccionarTodos()
        {
            string sql = "Select * from equipo_jugador;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public void Insertar(string idTemp, string idEquipo)
        {
            string sql = "Insert into equipo_jugador (equipoid_equipo,id_temporada) values ('" + idEquipo + "','" + idTemp + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string temporada,string equi,string jug)
        {
            string idJug = jugador.FindIdForName(jug);
            string idEq = equipo.FindIdForName(equi);
            string sql = "Update equipo_jugador set jugadorid_jugador='" + idJug + "' WHERE equipoid_equipo='" + idEq + "' and id_temporada='" + temporada + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void Eliminar(string idJug_Equipo)
        {

            //Eliminar de la tabla que se desea eliminar el dato en jugador

            string sql = "Delete from equipo_jugador Where id_jugador_equipo='" + idJug_Equipo + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        
        public bool Buscar(string temporada, string equi)
        {
            string hay = "";
            string idEq = equipo.FindIdForName(equi);
            string sql = "Select jugadorid_jugador from equipo_jugador where equipoid_equipo='"+idEq+"' and id_temporada='"+temporada+"';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            if (datos.Tables[0].Rows.Count > 0)
            {
                hay = datos.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                hay = "0";
            }
            if (hay != "0")
            {
                return true;
            }
            return false;
        }
        public void Insert(string idTemp, string nombreEquipo, string nombreJug)
        {
            string idJug = jugador.FindIdForName(nombreJug);
            string idEquipo = equipo.FindIdForName(nombreEquipo);
            string sql = "Insert into equipo_jugador (equipoid_equipo,id_temporada,jugadorid_jugador) values ('" + idEquipo + "','" + idTemp + "','" + idJug + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
    }
}
