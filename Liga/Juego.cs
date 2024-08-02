using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;


namespace Liga
{
    public class Juego
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        
        public Juego()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public string CampeonId(Juego juego)
        {
            string a = juego.CampeonDeTemporada();
            string sql = "Select id_equipo from equipo where juegos_ganados='" + a + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public string CampeonDirector(Juego juego)
        {
            string a = juego.CampeonDeTemporada();
            string sql = "Select nombre_director from equipo where juegos_ganados='" + a + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public string CampeonDeTemporada()
        {
            string sql = "Select max(juegos_ganados) from equipo;";
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
            string sql = "Select id_equipo_local as 'Local',id_equipo_visitante as 'Visitante',goles_local as 'Goles local',goles_visitante as 'Goles visitante',fecha as 'Fecha' from juego;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }
        public DataSet VerEquipos()
        {
            string sql = "Select id_equipo as 'Id',nombre_equipo as 'Equipos' from equipo order by id_equipo;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }
        public DataSet VerTabla()
        {
            string sql = "Select id_temporada as 'Temp.',nombre_equipo as 'Equipos',juego_ganado as 'Juegos ganados' from equipo,juegos_ganados where equipo.id_equipo=juegos_ganados.id_equipo order by id_temporada;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public string [] SelecAllLocal()
        {
            string sql = "Select nombre_equipo as 'Local' from juego,equipo where id_equipo_local=id_equipo;";
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
        
        public string[] FindAllIdJuego()
        {
            string sql = "Select id_juego from juego order by id_juego;";
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
        public string[] FindAllTeamWin()
        {
            string sql = "Select distinct equipo_ganador from juego;";
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

        public void Insertar(string fecha, string local, string visitor, string golesLoc, string golesVis)
        {
            string sql = "";
            if ( SeekWinner(golesLoc,golesVis) == "1" )
            {
                sql = "Insert into juego (id_equipo_local,id_equipo_visitante,goles_local,goles_visitante,equipo_ganador,fecha) values ('" + local + "','" + visitor + "','" + golesLoc + "','" + golesVis + "','" + local + "','" + fecha + "');";
            }
            else if ( SeekWinner(golesLoc,golesVis) == "2" )
            {
                sql = "Insert into juego (id_equipo_local,id_equipo_visitante,goles_local,goles_visitante,equipo_ganador,fecha) values ('" + local + "','" + visitor + "','" + golesLoc + "','" + golesVis + "','" + visitor + "','" + fecha + "');";
            }
            else
            {
                sql = "Insert into juego (id_equipo_local,id_equipo_visitante,goles_local,goles_visitante,empate,fecha,equipo_ganador) values ('" + local + "','" + visitor + "','" + golesLoc + "','" + golesVis + "','1','" + fecha + "','0');";
            }
            
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        
        public void Eliminar(string idjuego)
        {

            //Eliminar de la tabla que se desea eliminar el dato en juego

            string sql = "Delete from juego Where id_juego='" + idjuego + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public bool BuscarSiExiste(string fecha, string local, string visitor)
        {
            string hay = "";
            string sql = "Select id_juego from juego where (id_equipo_local='" + local + "' and id_equipo_visitante='" + visitor + "' and fecha='" + fecha + "') or (id_equipo_local='" + visitor + "' and id_equipo_visitante='" + local + "' and fecha='" + fecha + "');";
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
        public string SeekWinner(string golesLoc, string golesVis)
        {
            string sql = "Select ganador('" + golesLoc + "','" + golesVis + "') from juego;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
        public bool Buscar(string idTemp, string idEquipo)
        {
            string hay = "";
            string sql = "Select id_equipo from juegos_ganados where id_equipo='" + idEquipo + "' and id_temporada='" + idTemp + "';";
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

        //Juegos_ganados
        public void Insert(string idTemp, string idEquipo)
        {
            string sql = "Insert into juegos_ganados (id_temporada,id_equipo,juego_ganado) values ('" + idTemp + "','" + idEquipo + "','" + JuegosGanados(idEquipo) + "');";
            
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public void Actualizar(string idTemp, string idEquipo)
        {
            string sql = "Update juegos_ganados set juego_ganado='" + JuegosGanados(idEquipo) + "' WHERE id_temporada='" + idTemp + "' and id_equipo='" + idEquipo + "';";
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public string JuegosGanados(string idEquipo)
        {
            string sql = "Select count(equipo_ganador) from juego where equipo_ganador='" + idEquipo + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();

            return (datos.Tables[0].Rows.Count > 0) ? datos.Tables[0].Rows[0][0].ToString() : "0";
        }
    }
}
