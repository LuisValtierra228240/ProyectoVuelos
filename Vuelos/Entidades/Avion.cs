using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Vuelos.Base_de_datos;

namespace Vuelos.Entidades
{
    public class Avion
    {

        public int Id { get; set; }
        public string Placa { get; set; }
        public string Nombre { get; set; }

        public static Avion GetById(int id)
        {
            Avion avion = new Avion();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT id, placa, nombre FROM avion WHERE id = @id;";

                    MySqlCommand cmd = new MySqlCommand(query, conexion.connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        avion.Id = int.Parse(dataReader["id"].ToString());
                        avion.Placa = dataReader["placa"].ToString();
                        avion.Nombre = dataReader["nombre"].ToString();
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return avion;
        }

        public static List<Avion> GetAll()
        {
            List<Avion> aviones = new List<Avion>();
            try
            {
                Conexion conexion = new Conexion();
                if (conexion.OpenConnection())
                {
                    string query = "SELECT id, placa, nombre FROM avion;";
                    MySqlCommand command = new MySqlCommand(query, conexion.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    while(dataReader.Read())
                    {
                        Avion avion = new Avion();

                        avion.Id = int.Parse(dataReader["id"].ToString());
                        avion.Placa = dataReader["placa"].ToString();
                        avion.Nombre = dataReader["nombre"].ToString();

                        aviones.Add(avion);
                    }
                    dataReader.Close();
                    conexion.CloseConnection();
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            return aviones;
        }

        public static bool Guardar(int id, string placa, string nombre)
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
                        cmd.CommandText = "INSERT INTO avion (placa, nombre) VALUES (@placa, @nombre);";

                        cmd.Parameters.AddWithValue("@placa", placa);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE avion SET placa = @placa, nombre = @nombre WHERE id = @id;";

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@placa", placa);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
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
                    cmd.CommandText = "DELETE FROM avion WHERE id = @id;";
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