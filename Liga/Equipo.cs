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
    public class Equipo
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        public Equipo()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public string FindIdForName(string nombre)
        {
            string sql = "Select id_equipo from equipo where nombre_equipo='" + nombre + "';";
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
            string sql = "Select distinct equipo_jugador.id_temporada as 'Temporadas',nombre_equipo as 'Equipos',nombre_director as 'Directores',color as 'Color' from equipo,director_equipo,director,equipo_jugador where director_equipo.id_equipo=equipo.id_equipo and director_equipo.id_director=director.id_director and equipoid_equipo=equipo.id_equipo and equipo_jugador.id_temporada=director_equipo.id_temporada order by equipo_jugador.id_temporada,equipo.id_equipo;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public DataSet SelecId_Nombre_Equipos()
        {
            string sql = "Select id_equipo,nombre_equipo from equipo order by id_equipo;";
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
            string sql = "Select nombre_equipo from equipo;";
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
            string sql = "Select id_equipo from equipo order by id_equipo;";
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


        public void Insertar(string nombreEquipo, string color)
        {
            string sql = "Insert into equipo (nombre_equipo,color) values ('" + nombreEquipo + "','" + color + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string vUpdateColor, string condicion)
        {
            string id = FindIdForName(condicion);
            string sql = "Update equipo set color='" + vUpdateColor + "' WHERE id_equipo='" + id + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Eliminar(string idTemp, string nombre)
        {
            //Eliminar llaves foraneas en temporada_equipo_jugador
            string idEquipo = FindIdForName(nombre);
            string idEquipo_jugador = FindId(idEquipo);
            string sql7 = "Delete from temporada_equipo_jugador Where equipo_jugadorid_equipo_jugador='" + idEquipo_jugador + "' and temporadaid_temporada='" + idTemp + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql7, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en equipo_jugador
            string idequipo_jugador = FindIdForName(nombre);
            string sql2 = "Delete from equipo_jugador Where equipoid_equipo='" + idequipo_jugador + "' and id_temporada='" + idTemp + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql2, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en director_equipo
            string idEquip = FindIdForName(nombre);
            string sql1 = "Delete from director_equipo Where id_equipo='" + idEquip + "' and id_temporada='"+idTemp+"';";
            con.Conectar();
            cmd = new MySqlCommand(sql1, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en encuentro
            string idencuentro = FindIdForName(nombre);
            string sql3 = "Delete from encuentro Where id_equipo_local='" + idencuentro + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql3, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en encuentro
            string idencuentro2 = FindIdForName(nombre);
            string sql4 = "Delete from encuentro Where id_equipo_visitante='" + idencuentro2 + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql4, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en juego
            string idjuego = FindIdForVisitantTeam(FindIdForName(nombre));
            string sql5 = "Delete from juego Where encuentroid_encuentro='" + idjuego + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql5, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

            //Eliminar llaves foraneas en juego
            string idjuego2 = FindIdForLocalTeam(FindIdForName(nombre));
            string sql6 = "Delete from juego Where encuentroid_encuentro='" + idjuego2 + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql6, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();

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
        public bool BuscarSiExiste(string nombre)
        {
            string hay = "";
            string sql = "Select id_equipo from equipo where nombre_equipo='" + nombre + "';";
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
        public string FindId(string idequipo)
        {
            string sql = "Select id_jugador_equipo from equipo_jugador where equipoid_equipo='" + idequipo + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }

        /// <summary>
        /// Director
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>

        public void InsertarDT(string nombreDirector)
        {
            string sql = "Insert into director (nombre_director) values ('" + nombreDirector + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void InsertarDirector_equipo(string nombreEquipo, string nombreDirector, string idTemp)
        {
            string idEq = FindIdForName(nombreEquipo);
            string idDT = FindIdDirectorForName(nombreDirector);
            string sql = "Insert into director_equipo (id_equipo,id_director,id_temporada) values ('" + idEq + "','" + idDT + "','" + idTemp + "');";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public string FindIdDirectorForName(string nombre)
        {
            string sql = "Select id_director from director where nombre_director='" + nombre + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public bool BuscarSiExisteDirector(string nombre)
        {
            string hay = "";
            string sql = "Select id_director from director where nombre_director='" + nombre + "';";
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
