using Npgsql;
using System.Data;

namespace Integradora.Models
{

    public class Conexion{

        private string _connectionString;
        private NpgsqlConnection _connection = new();

        public Conexion()
        {
            _connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        }

        public DataTable GetQuery(string sql) {
            DataTable tb = new();
            NpgsqlDataAdapter adapter = new();
            using (_connection = new(_connectionString))
            {
                using(var command = new NpgsqlCommand())
                {
                    _connection.Open(); 
                    command.Connection = _connection; 
                    command.CommandText = sql;
                    adapter.SelectCommand = command;
                    adapter.Fill(tb);
                }
            }
            return tb;
        }

        public int ExecuteQuery(string sql, List<NpgsqlParameter> parameters)
        {

            try
            {
                using (_connection = new(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        _connection.Open();
                        command.Connection = _connection;
                        command.CommandText = sql;
                        command.Parameters.Clear();
                        foreach (NpgsqlParameter param in parameters)
                        {
                            command.Parameters.Add(param);
                        }
                        return command.ExecuteNonQuery();
                    }

                }
                return 0;

                using (_connection = new NpgsqlConnection(_connectionString))
                {
                    _connection.Open();
                    using (var command = new NpgsqlCommand(sql, _connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Npgsql.PostgresException ex) when (ex.SqlState == "23505")
            {
                return -1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetQuery(string sql, List<NpgsqlParameter> parameters)
        {
            DataTable tabla = new();
            NpgsqlDataAdapter adapter = new();
            using (_connection = new(_connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    _connection.Open();
                    command.Connection = _connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    foreach (NpgsqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(tabla);
                }
                return tabla;
            }
        }

    }
}
