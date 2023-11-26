using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using MySql.Data.MySqlClient;
using Vuelos.Base_de_datos;
using Vuelos.Entidades;

namespace Vuelos.Entidades
{
    //Solo para hacer el crud boleto, se debe borrar una vez este listo el crud Vuelo;
    public class Boleto
    {
        public int Id { get; set; }
        public DateTime fecha_creacion { get; set; }
        public Vuelo Vuelo { get; set; }
        public Pasajero Pasajero { get; set; }
        public int Asiento { get; set; }

        public static List<Boleto> GetAll()
        {
            List<Boleto> boletos = new List<Boleto>();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT boleto.id AS id_boleto, avion.id AS id_avion, pasajero.id AS id_pasajero,fecha_creacion, boleto.asiento AS asiento,vuelo.origen AS origen_vuelo, vuelo.destino AS destino_vuelo," +
                        "\r\n vuelo.capacidad AS capacidad_avion, vuelo.fechaPartida as fechaPartida, vuelo.fechaLlegada as fechaLlegada ,avion.placa as avion_placa," +
                        "\r\n avion.nombre AS avion_nombre, CONCAT(pasajero.nombre, \" \", pasajero.apellidoPaterno, \" \", " +
                        "\r\n pasajero.apellidoMaterno) AS nombre_completo_pasajero, pasajero.fechaNacimiento AS fecha_nacimiento_pasajero" +
                        "\r\n FROM boleto INNER JOIN vuelo ON boleto.id_vuelo=vuelo.id " +
                        "\r\n INNER JOIN avion ON vuelo.idAvion=avion.id" +
                        "\r\n INNER JOIN pasajero ON boleto.id_pasajero=pasajero.id;";
                    MySqlCommand command = new MySqlCommand(query, conexion.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Boleto boleto = new Boleto();
                        boleto.Id = int.Parse(dataReader["id_boleto"].ToString());
                        boleto.fecha_creacion = DateTime.Parse(dataReader["fecha_creacion"].ToString());
                        boleto.Asiento = int.Parse(dataReader["asiento"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["id_avion"].ToString());
                        avion.Placa = dataReader["avion_placa"].ToString();
                        avion.Nombre = dataReader["avion_nombre"].ToString();

                        Vuelo vuelo = new Vuelo();
                        vuelo.Avion = avion;
                        vuelo.Capacidad = int.Parse(avion.Placa = dataReader["capacidad_avion"].ToString());
                        vuelo.Origen = dataReader["origen_vuelo"].ToString();
                        vuelo.Destino = dataReader["destino_vuelo"].ToString();
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaPartida"].ToString());
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaLlegada"].ToString());

                        Pasajero pasajero = new Pasajero();
                        pasajero.Id = int.Parse(dataReader["id_pasajero"].ToString());
                        pasajero.Nombre = dataReader["nombre_completo_pasajero"].ToString();
                        pasajero.FechaNacimiento = dataReader["fecha_nacimiento_pasajero"].ToString();

                        boleto.Vuelo = vuelo;
                        boleto.Pasajero = pasajero;

                        boletos.Add(boleto);
                    }

                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return boletos;
        }

        public static List<Boleto> GetByVuelo(int id)
        {
            List<Boleto> boletos = new List<Boleto>();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    MySqlCommand cmd = conexion.connection.CreateCommand();

                    cmd.CommandText = "SELECT boleto.id AS id_boleto, avion.id AS id_avion, pasajero.id AS id_pasajero,fecha_creacion, boleto.asiento AS asiento,vuelo.origen AS origen_vuelo, vuelo.destino AS destino_vuelo," +
                        "\r\n vuelo.capacidad AS capacidad_avion, vuelo.fechaPartida as fechaPartida, vuelo.fechaLlegada as fechaLlegada ,avion.placa as avion_placa," +
                        "\r\n avion.nombre AS avion_nombre, CONCAT(pasajero.nombre, \" \", pasajero.apellidoPaterno, \" \", " +
                        "\r\n pasajero.apellidoMaterno) AS nombre_completo_pasajero, pasajero.fechaNacimiento AS fecha_nacimiento_pasajero" +
                        "\r\n FROM boleto INNER JOIN vuelo ON boleto.id_vuelo=vuelo.id " +
                        "\r\n INNER JOIN avion ON vuelo.idAvion=avion.id" +
                        "\r\n INNER JOIN pasajero ON boleto.id_pasajero=pasajero.id WHERE boleto.id_vuelo=@id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Boleto boleto = new Boleto();
                        boleto.Id = int.Parse(dataReader["id_boleto"].ToString());
                        boleto.fecha_creacion = DateTime.Parse(dataReader["fecha_creacion"].ToString());
                        boleto.Asiento = int.Parse(dataReader["asiento"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["id_avion"].ToString());
                        avion.Placa = dataReader["avion_placa"].ToString();
                        avion.Nombre = dataReader["avion_nombre"].ToString();

                        Vuelo vuelo = new Vuelo();
                        vuelo.Avion = avion;
                        vuelo.Capacidad = int.Parse(avion.Placa = dataReader["capacidad_avion"].ToString());
                        vuelo.Origen = dataReader["origen_vuelo"].ToString();
                        vuelo.Destino = dataReader["destino_vuelo"].ToString();
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaPartida"].ToString());
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaLlegada"].ToString());

                        Pasajero pasajero = new Pasajero();
                        pasajero.Id = int.Parse(dataReader["id_pasajero"].ToString());
                        pasajero.Nombre = dataReader["nombre_completo_pasajero"].ToString();
                        pasajero.FechaNacimiento = dataReader["fecha_nacimiento_pasajero"].ToString();

                        boleto.Vuelo = vuelo;
                        boleto.Pasajero = pasajero;

                        boletos.Add(boleto);
                    }

                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return boletos;
        }
        public static Boleto GetById(int id)
        {
            Boleto boleto = new Boleto();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    MySqlCommand cmd = conexion.connection.CreateCommand();

                    cmd.CommandText = "SELECT boleto.id AS id_boleto, avion.id AS id_avion, pasajero.id AS id_pasajero,fecha_creacion, boleto.asiento AS asiento, vuelo.origen AS origen_vuelo, vuelo.destino AS destino_vuelo," +
                        "\r\n vuelo.capacidad AS capacidad_avion, vuelo.fechaPartida as fechaPartida, vuelo.fechaLlegada as fechaLlegada, avion.placa as avion_placa," +
                        "\r\n avion.nombre AS avion_nombre, CONCAT(pasajero.nombre, \" \", pasajero.apellidoPaterno, \" \", " +
                        "\r\n pasajero.apellidoMaterno) AS nombre_completo_pasajero, pasajero.fechaNacimiento AS fecha_nacimiento_pasajero" +
                        "\r\n FROM boleto INNER JOIN vuelo ON boleto.id_vuelo=vuelo.id " +
                        "\r\n INNER JOIN avion ON vuelo.idAvion=avion.id" +
                        "\r\n INNER JOIN pasajero ON boleto.id_pasajero=pasajero.id WHERE boleto.id=@id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        boleto.Id = int.Parse(dataReader["id_boleto"].ToString());
                        boleto.fecha_creacion = DateTime.Parse(dataReader["fecha_creacion"].ToString());
                        boleto.Asiento = int.Parse(dataReader["asiento"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["id_avion"].ToString());
                        avion.Placa = dataReader["avion_placa"].ToString();
                        avion.Nombre = dataReader["avion_nombre"].ToString();

                        Vuelo vuelo = new Vuelo();
                        vuelo.Avion = avion;
                        vuelo.Capacidad = int.Parse(avion.Placa = dataReader["capacidad_avion"].ToString());
                        vuelo.Origen = dataReader["origen_vuelo"].ToString();
                        vuelo.Destino = dataReader["destino_vuelo"].ToString();
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaPartida"].ToString());
                        vuelo.FechaPartida = DateTime.Parse(dataReader["fechaLlegada"].ToString());

                        Pasajero pasajero = new Pasajero();
                        pasajero.Id = int.Parse(dataReader["id_pasajero"].ToString());
                        pasajero.Nombre = dataReader["nombre_completo_pasajero"].ToString();
                        pasajero.FechaNacimiento = dataReader["fecha_nacimiento_pasajero"].ToString();

                        boleto.Vuelo = vuelo;
                        boleto.Pasajero = pasajero;

                    }

                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return boleto;
        }

        public static int Guardar(Boleto boleto)
        {
            int result = -1;
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    MySqlCommand cmd = conexion.connection.CreateCommand();

                    cmd.CommandText = "INSERT INTO boleto (fecha_creacion, id_vuelo, id_pasajero, asiento) VALUES (@fecha_creacion, @id_vuelo, @id_pasajero, @asiento);";

                    cmd.Parameters.AddWithValue("@fecha_creacion", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@id_vuelo", boleto.Vuelo.Id);
                    cmd.Parameters.AddWithValue("@id_pasajero", boleto.Pasajero.Id);
                    cmd.Parameters.AddWithValue("@asiento", boleto.Asiento);

                    //result = cmd.ExecuteNonQuery() == 1;
                    cmd.ExecuteNonQuery();
                    result = (int) cmd.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool Eliminar(int id)
        {
            bool result = false;
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    MySqlCommand cmd = conexion.connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM boleto WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    result = cmd.ExecuteNonQuery() == 1;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static bool PasajeroTieneBoletoEnVuelo(int pasajeroId, int vueloId)
        {
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT COUNT(*) FROM boleto WHERE id_pasajero = @pasajeroId AND id_vuelo = @vueloId;";
                    MySqlCommand cmd = new MySqlCommand(query, conexion.connection);
                    cmd.Parameters.AddWithValue("@pasajeroId", pasajeroId);
                    cmd.Parameters.AddWithValue("@vueloId", vueloId);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    conexion.CloseConnection();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }

    
}