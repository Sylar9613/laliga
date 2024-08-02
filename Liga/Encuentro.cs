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
    public class Encuentro
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        public Encuentro()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public string FindIdForLocalTeam(string idEquipoLocal)
        {
            string sql = "Select id_encuentro from encuentro where id_equipo_local='" + idEquipoLocal + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }

        public string FindIdForVisitantTeam(string idEquipoVis)
        {
            string sql = "Select id_encuentro from encuentro where id_equipo_visitante='" + idEquipoVis + "';";
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
            string sql = "Select * from encuentro;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public string[] FindAllLocalTeams()
        {
            string sql = "Select distinct id_equipo_local from encuentro;";
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
        public string[] FindAllIdEncuentro()
        {
            string sql = "Select distinct id_encuentro from encuentro;";
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


        public void Insertar(string idEncuentro, string idEquipoLocal, string idEquipoVisitante)
        {
            string sql = "Insert into encuentro (id_encuentro,id_equipo_local,id_equipo_visitante) values ('" + idEncuentro + "','" + idEquipoLocal + "','" + idEquipoVisitante + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string valorUpdate, string condicion)
        {
            string sql = "Update encuentro set id_equipo_local='" + valorUpdate + "' WHERE id_encuentro='" + condicion + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Eliminar(string idEquipoLocal)
        {
            //Eliminar llaves foraneas en juego
            string idjuego = FindIdForLocalTeam(idEquipoLocal);
            string sql1 = "Delete from juego Where encuentroid_encuentro='" + idjuego + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql1, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en juego
            string idjuego2 = FindIdForVisitantTeam(idEquipoLocal);
            string sql2 = "Delete from juego Where encuentroid_encuentro='" + idjuego2 + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql2, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();


            //Eliminar de la tabla que se desea eliminar el dato en encuentro

            string sql = "Delete from encuentro Where id_equipo_local='" + idEquipoLocal + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void EliminarIdencuentro(string idencuentro)
        {
            string sql = "Delete from encuentro Where id_encuentro='" + idencuentro + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public string CuantosIdHay()
        {
            string resp = "";
            string sql = "Select count id_encuentro from encuentro;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            resp = datos.ToString();
            return resp;
        }
    }
}
