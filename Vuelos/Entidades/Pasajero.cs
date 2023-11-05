using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Vuelos.Base_de_datos;

namespace Vuelos.Entidades
{
    public class Pasajero
    {

        public int Id { get; set; }

        public string Nombre  { get; set; }
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string FechaNacimiento { get; set; }

        public static Pasajero GetById(int id)
        {
            Pasajero pasajero = new Pasajero();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT id, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento FROM pasajero WHERE id = @id;";

                    MySqlCommand cmd = new MySqlCommand(query, conexion.connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        pasajero.Id = int.Parse(dataReader["id"].ToString());
                        pasajero.Nombre = dataReader["nombre"].ToString();
                        pasajero.ApellidoPaterno = dataReader["apellidoPaterno"].ToString();
                        pasajero.ApellidoMaterno = dataReader["apellidoMaterno"].ToString();
                        pasajero.FechaNacimiento = dataReader["fechaNacimiento"].ToString();
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pasajero;
        }

        public static List<Pasajero> GetAll()
        {
            List<Pasajero> pasajeros = new List<Pasajero>();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT id, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento FROM pasajero";
                    MySqlCommand command = new MySqlCommand(query, conexion.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    while(dataReader.Read())
                    {
                        Pasajero pasajero = new Pasajero();

                        pasajero.Id = int.Parse(dataReader["id"].ToString());
                        pasajero.Nombre = dataReader["nombre"].ToString();
                        pasajero.ApellidoPaterno = dataReader["apellidoPaterno"].ToString();
                        pasajero.ApellidoMaterno = dataReader["apellidoMaterno"].ToString();
                        pasajero.FechaNacimiento = dataReader["fechaNacimiento"].ToString();

                        pasajeros.Add(pasajero);
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            return pasajeros;
        }

        public static bool Guardar(int id, string nombre, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento)
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
                        cmd.CommandText = "INSERT INTO pasajero (nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento) VALUES (@nombre, @apellidoPaterno, @apellidoMaterno, @fechaNacimiento);";

                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE pasajero SET nombre = @nombre, apellidoPaterno = @apellidoPaterno, apellidoMaterno = @apellidoMaterno, fechaNacimiento = @fechaNacimiento WHERE id = @id;";

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
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
                    cmd.CommandText = "DELETE FROM pasajero WHERE id = @id;";
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