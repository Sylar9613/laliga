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
    public class Jugador
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;        
        public Jugador()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public string FindIdForName(string nombreJug)
        {
            string sql = "Select id_jugador from jugador where nombre_jug='" + nombreJug + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }

        public DataSet SeleccionarTodos()
        {
            string sql = "Select * from view_player";
            //string sql = "Select distinct id_temporada as 'Temp.',nombre_equipo as 'Equipo',nombre_jug as 'Jugadores',alias as 'Alias',caracter as 'Pos.',edad as 'Edad',peso as 'Peso(Kg)',talla as 'Talla(m)',rendimiento(peso,talla) as 'AVE' from equipo,equipo_jugador,jugador,posiciones,posiciones_jugador where equipoid_equipo=id_equipo and jugadorid_jugador=id_jugador and id_jugador=jugadorid_posicion and id_posiciones=posicionesid_posiciones order by id_temporada,id_equipo,id_posiciones;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public string[] FindAllNames()
        {
            string sql = "Select nombre_jug from jugador;";
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
        public string[] FindAllID()
        {
            string sql = "Select id_jugador from jugador order by id_jugador;";
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

        public void Insertar(string nombreJug, string alias, string edad, string peso, string talla)
        {
            string sql = "";
            if (alias == null)
            {
                sql = "Insert into jugador (nombre_jug,edad,peso,talla) values ('" + nombreJug + "','" + edad + "','" + peso + "','" + talla + "');";
            }
            else
            {
                sql = "Insert into jugador (nombre_jug,alias,edad,peso,talla) values ('" + nombreJug + "','" + alias + "','" + edad + "','" + peso + "','" + talla + "');";
            }
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string nombreJug, string alias, string edad, string peso, string talla)
        {
            string sql = "";
            if (alias == null)
            {
                sql = "Update jugador set edad='" + edad + "',peso='" + peso + "',talla='" + talla + "' WHERE nombre_jug='" + nombreJug + "';";
            }
            else
            {
                sql = "Update jugador set alias='" + alias + "',edad='" + edad + "',peso='" + peso + "',talla='" + talla + "' WHERE nombre_jug='" + nombreJug + "';";
            }
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Eliminar(string idTemp, string nombreJug)
        {
            //Eliminar llaves foraneas en equipo_jugador
            string idequipo_jugador = FindIdForName(nombreJug);
            string sql2 = "Delete from equipo_jugador Where jugadorid_jugador='" + idequipo_jugador + "' and id_temporada='" + idTemp + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql2, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();        
        }
        public bool BuscarSiExiste(string nombre)
        {
            string hay = "";
            string sql = "Select id_jugador from jugador where nombre_jug='" + nombre + "';";
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
    }
}
