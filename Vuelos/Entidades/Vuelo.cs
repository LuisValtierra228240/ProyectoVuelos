using MySql.Data.MySqlClient;
using Vuelos.Base_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Vuelos.Entidades
{
    public class Vuelo
    {

        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public Avion Avion { get; set; }
        public int Capacidad { get; set; }
        public DateTime Fecha { get; set; }
        public List<Pasajero> pasajeros;


        public static Vuelo GetById(int id)
        {
            Vuelo vuelo = new Vuelo();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT vuelo.id, vuelo.origen, vuelo.destino, " +
                        "vuelo.capacidad, vuelo.fecha, avion.id AS idAvion, avion.nombre " +
                        "AS nombreAvion, avion.placa AS placaAvion FROM vuelo INNER JOIN avion " +
                        "ON vuelo.idAvion = avion.id WHERE vuelo.id = @id;";

                    MySqlCommand cmd = new MySqlCommand(query, conexion.connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        vuelo.Id = int.Parse(dataReader["id"].ToString());
                        vuelo.Origen = dataReader["origen"].ToString();
                        vuelo.Destino = dataReader["destino"].ToString();
                        vuelo.Capacidad = int.Parse(dataReader["capacidad"].ToString());
                        vuelo.Fecha = DateTime.Parse(dataReader["fecha"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["idAvion"].ToString());
                        avion.Nombre = dataReader["nombreAvion"].ToString();
                        avion.Placa = dataReader["placaAvion"].ToString();

                        vuelo.Avion = avion;

                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vuelo;
        }

        public static Vuelo GetPasajeros(int id)
        {
            Vuelo vuelo = new Vuelo();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT vuelo.id, vuelo.origen, vuelo.destino, vuelo.capacidad, vuelo.fecha," +
                        "\r\navion.id AS idAvion, avion.nombre AS nombreAvion, avion.placa AS placaAvion," +
                        "\r\npasajero.id AS id_pasajero, pasajero.nombre AS nombre_pasajero, pasajero.apellidoPaterno as apellido_paterno_pasajero," +
                        "\r\npasajero.apellidoMaterno AS apellido_materno_pasajero, pasajero.fechaNacimiento AS fecha_nacimiento_pasajero" +
                        "\r\nFROM pasajero INNER JOIN boleto ON pasajero.id=boleto.id_pasajero \r\nINNER JOIN vuelo ON boleto.id_vuelo=vuelo.id" +
                        "\r\nINNER JOIN avion ON avion.id=vuelo.idAvion\r\nWHERE vuelo.id=@id;";

                    MySqlCommand cmd = new MySqlCommand(query, conexion.connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    vuelo.pasajeros = new List<Pasajero>();

                    while (dataReader.Read())
                    {
                        vuelo.Id = int.Parse(dataReader["id"].ToString());
                        vuelo.Origen = dataReader["origen"].ToString();
                        vuelo.Destino = dataReader["destino"].ToString();
                        vuelo.Capacidad = int.Parse(dataReader["capacidad"].ToString());
                        vuelo.Fecha = DateTime.Parse(dataReader["fecha"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["idAvion"].ToString());
                        avion.Nombre = dataReader["nombreAvion"].ToString();
                        avion.Placa = dataReader["placaAvion"].ToString();

                        vuelo.Avion = avion;

                        Pasajero pasajero = new Pasajero();
                        pasajero.Id = int.Parse(dataReader["id_pasajero"].ToString());
                        pasajero.Nombre = dataReader["nombre_pasajero"].ToString();
                        pasajero.ApellidoPaterno = dataReader["apellido_paterno_pasajero"].ToString();
                        pasajero.ApellidoMaterno = dataReader["apellido_materno_pasajero"].ToString();
                        pasajero.FechaNacimiento = dataReader["fecha_nacimiento_pasajero"].ToString();

                        vuelo.pasajeros.Add(pasajero);
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vuelo;
        }

        public static List<Vuelo> GetAll()
        {
            List<Vuelo> vuelos = new List<Vuelo>();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT vuelo.id, vuelo.origen, vuelo.destino, " +
                        "vuelo.capacidad, vuelo.fecha, avion.id AS idAvion, avion.nombre " +
                        "AS nombreAvion, avion.placa AS placaAvion FROM vuelo INNER JOIN avion " +
                        "ON vuelo.idAvion = avion.id;";
                    MySqlCommand command = new MySqlCommand(query, conexion.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Vuelo vuelo = new Vuelo();

                        vuelo.Id = int.Parse(dataReader["id"].ToString());
                        vuelo.Origen = dataReader["origen"].ToString();
                        vuelo.Destino = dataReader["destino"].ToString();
                        vuelo.Capacidad = int.Parse(dataReader["capacidad"].ToString());
                        vuelo.Fecha = DateTime.Parse(dataReader["fecha"].ToString());

                        Avion avion = new Avion();
                        avion.Id = int.Parse(dataReader["idAvion"].ToString());
                        avion.Nombre = dataReader["nombreAvion"].ToString();
                        avion.Placa = dataReader["placaAvion"].ToString();
                        vuelo.Avion = avion;

                        vuelos.Add(vuelo);
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vuelos;
        }

        public static bool Guardar(int id, string origen, string destino, int idAvion, string capacidad, DateTime fecha)
        {
            bool result = false;
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    MySqlCommand cmd = conexion.connection.CreateCommand();

                    if (id == 0)
                    {
                        cmd.CommandText = "INSERT INTO vuelo (origen, destino, idAvion, capacidad, fecha) VALUES (@origen, @destino, @idAvion, @capacidad, @fecha);";

                        cmd.Parameters.AddWithValue("@origen", origen);
                        cmd.Parameters.AddWithValue("@destino", destino);
                        cmd.Parameters.AddWithValue("@idAvion", idAvion);
                        cmd.Parameters.AddWithValue("@capacidad", capacidad);
                        cmd.Parameters.AddWithValue("@fecha", fecha);

                    }
                    else
                    {
                        cmd.CommandText = "UPDATE vuelo SET origen = @origen, destino = @destino, idAvion = @idAvion, capacidad = @capacidad, fecha = @fecha WHERE id = @id;";

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@origen", origen);
                        cmd.Parameters.AddWithValue("@destino", destino);
                        cmd.Parameters.AddWithValue("@idAvion", idAvion);
                        cmd.Parameters.AddWithValue("@capacidad", capacidad);
                        cmd.Parameters.AddWithValue("@fecha", fecha);

                    }

                    result = cmd.ExecuteNonQuery() == 1;
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
                    cmd.CommandText = "DELETE FROM vuelo WHERE id = @id;";
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


    }
}