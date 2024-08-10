using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DAL.Repositories
{
    public class ClientRepository
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public ClientRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            using (SqlCommand command = new SqlCommand("GetAllClients_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            ClientName = reader["ClientName"].ToString(),
                            ContactPerson = reader["ContactPerson"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString()
                        });
                    }
                }
            }

            return clients;
        }

        public Client GetClientById(int clientId)
        {
            SqlParameter parameter = new SqlParameter("@ClientId", clientId);

            using (SqlCommand command = new SqlCommand("GetClientById_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Client
                        {
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            ClientName = reader["ClientName"].ToString(),
                            ContactPerson = reader["ContactPerson"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void AddClient(Client client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ClientName", client.ClientName),
                    new SqlParameter("@ContactPerson", client.ContactPerson),
                    new SqlParameter("@Email", client.Email),
                    new SqlParameter("@Phone", client.Phone),
                    new SqlParameter("@Address", client.Address)
                };

                using (SqlCommand command = new SqlCommand("InsertClient_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ClientId", client.ClientId),
                    new SqlParameter("@ClientName", client.ClientName),
                    new SqlParameter("@ContactPerson", client.ContactPerson),
                    new SqlParameter("@Email", client.Email),
                    new SqlParameter("@Phone", client.Phone),
                    new SqlParameter("@Address", client.Address)
                };

                using (SqlCommand command = new SqlCommand("UpdateClient_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteClient(int clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter parameter = new SqlParameter("@ClientId", clientId);

                using (SqlCommand command = new SqlCommand("DeleteClient_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}