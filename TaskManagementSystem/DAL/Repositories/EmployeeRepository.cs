using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DAL.Repositories
{
    public class EmployeeRepository
    {
        #region Global Declarations

        private readonly string connectionString;
        private SqlConnection connection;

        public EmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        #endregion

        #region CRUD Operations

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlCommand command = new SqlCommand("GetAllEmployees_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Password = reader["Password"].ToString(),
                            RoleName = reader["RoleName"].ToString(),
                            ProjectName = reader["ProjectName"].ToString()
                        });
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            SqlParameter parameter = new SqlParameter("@EmployeeId", employeeId);

            using (SqlCommand command = new SqlCommand("GetAllEmployeeById_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Password = reader["Password"].ToString(),
                            ProjectId = reader["ProjectId"] != DBNull.Value ? Convert.ToInt32(reader["ProjectId"]) : 0,
                            ProjectName = reader["ProjectName"] != DBNull.Value ? reader["ProjectName"].ToString() : string.Empty,
                            RoleId = reader["RoleId"] != DBNull.Value ? Convert.ToInt32(reader["RoleId"]) : 0,
                            RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : string.Empty
                        };

                        return employee;
                    }

                }
            }

            return null;
        }


        public Employee GetEmployeeByEmail(string email)
        {
            SqlParameter parameter = new SqlParameter("@Email", email);

            using (SqlCommand command = new SqlCommand("GetEmployeeByEmail_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Password = reader["Password"].ToString(),
                            RoleName = reader["RoleName"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", employee.EmployeeName),
                    new SqlParameter("@Address", employee.Address),
                    new SqlParameter("@Email", employee.Email),
                    new SqlParameter("@PhoneNumber", employee.PhoneNumber),
                    new SqlParameter("@Password", employee.Password),
                    new SqlParameter("@RoleId", employee.RoleId),
                };

                using (SqlCommand command = new SqlCommand("InsertEmployee_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmployeeId", employee.EmployeeId),
                    new SqlParameter("@EmployeeName", employee.EmployeeName),
                    new SqlParameter("@Address", employee.Address),
                    new SqlParameter("@Email", employee.Email),
                    new SqlParameter("@PhoneNumber", employee.PhoneNumber),
                    new SqlParameter("@RoleId", employee.RoleId),
                    new SqlParameter("@ProjectId", employee.ProjectId.HasValue ? (object)employee.ProjectId.Value : DBNull.Value),
                };

                using (SqlCommand command = new SqlCommand("UpdateEmployee_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter parameter = new SqlParameter("@EmployeeId", employeeId);

                using (SqlCommand command = new SqlCommand("DeleteEmployee_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region CheckIfEmailExists

        public bool CheckEmailExists(string email)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Exists", SqlDbType.Bit) { Direction = ParameterDirection.Output }
            };

            using (SqlCommand command = new SqlCommand("CheckEmailExists_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                connection.Open();
                command.ExecuteNonQuery();
                bool exists = Convert.ToBoolean(parameters[1].Value);
                return exists;
            }
        }

        #endregion

    }
}
