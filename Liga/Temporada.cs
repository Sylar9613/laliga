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
    public class Temporada
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        public Temporada()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public string FindId(string codigo)
        {
            string sql = "Select id_temporada from temporada where codigo='" + codigo + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public string[] FindAllIdTemp()
        {
            string sql = "Select id_temporada from temporada order by id_temporada;";
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
            string sql = "Select id_temporada as 'ID',fecha_comienzo as 'Fecha inicio',fecha_final as 'Fecha fin',codigo as 'Codigo',duracion(fecha_comienzo,fecha_final) as 'Duracion' from temporada;";
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
            string sql = "Select codigo from temporada;";
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
        public void Insertar(string fechaInicio, string fechaTermina, string codigo)
        {
            string sql = "Insert into temporada (fecha_comienzo,fecha_final,codigo) values ('" + fechaInicio + "','" + fechaTermina + "','" + codigo + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string valorUpdate, string condicion)
        {
            string sql = "Update temporada set codigo='" + valorUpdate + "' WHERE id_temporada='" + condicion + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Eliminar(string codigo)
        {

            //Eliminar de la tabla que se desea eliminar el dato

            string sql = "Delete from temporada Where codigo='" + codigo + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public bool BuscarSiTieneElementos(string id)
        {
            string hay = "";
            string sql = "Select id_jugador_equipo from equipo_jugador where id_temporada='" + id + "';";
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
        public string[] FindAllEquiposForTemp(string idTemp)
        {
            string sql = "Select distinct nombre_equipo from equipo,equipo_jugador where equipo_jugador.id_temporada='" + idTemp + "' and equipoid_equipo=id_equipo;";
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
        public string[] FindAllJugForTemp(string idTemp)
        {
            string sql = "Select distinct nombre_jug from equipo_jugador,equipo,jugador where equipoid_equipo=id_equipo and jugadorid_jugador=id_jugador and id_temporada='" + idTemp + "';";
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
    }
}
