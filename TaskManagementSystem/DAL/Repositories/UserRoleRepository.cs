using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DAL.Repositories
{
    public class UserRoleRepository
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public UserRoleRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public List<UserRole> GetAllRoles()
        {
            List<UserRole> roles = new List<UserRole>();

            using (SqlCommand command = new SqlCommand("GetAllRoles_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new UserRole
                        {
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            RoleName = reader["RoleName"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        });
                    }
                }
            }

            return roles;
        }

        public UserRole GetRoleById(int roleId)
        {
            SqlParameter parameter = new SqlParameter("@RoleId", roleId);

            using (SqlCommand command = new SqlCommand("GetRoleById_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserRole
                        {
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            RoleName = reader["RoleName"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        };
                    }
                }
            }

            return null;
        }

        public void AddRole(UserRole role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleName", role.RoleName),
                    new SqlParameter("@Description", role.Description)
                };

                using (SqlCommand command = new SqlCommand("AddRole_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}