using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskManagementSystem.DAL
{
    public class DbHelper : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public DbHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public void ExecuteNonQuery(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}