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
    public class ConsultaSQL
    {
        private Conexion con;
        private DataSet datos;
        private MySqlDataAdapter adaptador;
        private MySqlCommand cmd;
        Juego juego = new Juego();
        public ConsultaSQL()
        {
            con = new Conexion();
            datos = new DataSet();
        }

        public DataSet consulta1(string idTemp)
        {
            string sql = "Select distinct nombre_equipo as 'Equipos' from equipo,encuentro,juego,equipo_jugador,temporada where (id_equipo = id_equipo_local or id_equipo = id_equipo_visitante) and id_encuentro = encuentroid_encuentro and equipo_ganador=id_equipo and id_equipo=equipoid_equipo and equipo_jugador.id_temporada='" + idTemp + "';";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public DataSet consulta2()
        {
            string sql = "Select nombre_jug as 'Jugadores',alias as 'Alias' from jugador,equipo,equipo_jugador,temporada where equipo.id_equipo = equipoid_equipo and id_jugador = jugadorid_jugador and temporada.id_temporada = equipo_jugador.id_temporada order by temporada.id_temporada,equipo.id_equipo,id_jugador;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public DataSet consulta3()
        {
            string sql = "Select count(id_jugador) as 'Cant. jugad.',nombre_pos as 'Posiciones',max(rendimiento(peso,talla)) as 'Rendimiento' from jugador,posiciones_jugador,posiciones where id_jugador=jugadorid_posicion and id_posiciones = posicionesid_posiciones group by nombre_pos;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public DataSet consulta4()
        {
            string a = juego.CampeonId(juego);
            //string b = juego.CampeonDirector(juego);
            string sql = "Select distinct nombre_equipo as 'Equipo',nombre_director as 'DT' from equipo,director,director_equipo,equipo_jugador,temporada where equipo.id_equipo = equipoid_equipo and temporada.id_temporada = equipo_jugador.id_temporada and equipo.id_equipo='" + a + "' and equipo.id_equipo=director_equipo.id_equipo and director_equipo.id_director=director.id_director order by temporada.id_temporada;";
            datos = new DataSet();
            adaptador = new MySqlDataAdapter();
            con.Conectar();
            cmd = new MySqlCommand(sql, con.MiConexion);
            adaptador.SelectCommand = cmd;
            adaptador.Fill(datos);
            con.Desconectar();
            return datos;
        }

        public DataSet consulta5()
        {
            string sql = "Select max(duracion(fecha_comienzo,fecha_final)) as 'Duracion max.',min(duracion(fecha_comienzo,fecha_final)) as 'Duracion min.',max(id_juego) as 'Max. juegos' from temporada,juego;";
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
