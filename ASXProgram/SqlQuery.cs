using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASXProgram
{
    public class SqlQuery
    {
        private readonly string _connectionString;
        private readonly string _sql;
        private readonly List<SqlParameter> _parameters;

        public SqlQuery(string connectionString, string sql)
        {
            _connectionString = connectionString;
            _sql = sql;
            _parameters = new List<SqlParameter>();
        }

        public void AddParameter(string name, object value)
        {
            _parameters.Add(new SqlParameter(name, value));
        }

        public int ExecuteNonQuery()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(_sql, connection))
                {
                    command.Parameters.AddRange(_parameters.ToArray());
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(_sql, connection))
                {
                    command.Parameters.AddRange(_parameters.ToArray());
                    return command.ExecuteScalar();
                }
            }
        }

        public SqlDataReader ExecuteReader()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(_sql, connection);
            command.Parameters.AddRange(_parameters.ToArray());
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
