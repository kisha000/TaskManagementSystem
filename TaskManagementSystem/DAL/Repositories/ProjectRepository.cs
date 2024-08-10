using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DAL.Repositories
{
    public class ProjectRepository
    {
        #region Global Delcarations
        private readonly string connectionString;
        private SqlConnection connection;

        public ProjectRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }
        #endregion

        #region GetProjects
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlCommand command = new SqlCommand("GetAllProjects_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            ProjectName = reader["ProjectName"].ToString(),
                            Description = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Status = reader["Status"].ToString(),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            ClientName = reader["ClientName"].ToString(),
                        });
                    }
                }
            }

            return projects;
        }

        public IEnumerable<Employee> GetEmployeesByProjectId(int projectId)
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetEmployeesByProjectId_Surya", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProjectId", projectId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        EmployeeName = reader["EmployeeName"].ToString(),
                    };
                    employees.Add(employee);
                }

                reader.Close();
            }

            return employees;
        }

        public List<Project> GetAllOpenProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlCommand command = new SqlCommand("GetAllOpenProjects_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            ProjectName = reader["ProjectName"].ToString(),
                            Description = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Status = reader["Status"].ToString(),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            ClientName = reader["ClientName"].ToString(),
                        });
                    }
                }
            }

            return projects;
        }

        public List<Project> GetProjectsByEmployee(int employeeId)
        {
            List<Project> projects = new List<Project>();

            using (SqlCommand command = new SqlCommand("GetProjectsByEmployee_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            ProjectName = reader["ProjectName"].ToString(),
                            Description = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Status = reader["Status"].ToString(),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            ClientName = reader["ClientName"].ToString(),
                        });
                    }
                }
            }

            return projects;
        }

        public Project GetProjectById(int projectId)
        {
            SqlParameter parameter = new SqlParameter("@ProjectId", projectId);

            using (SqlCommand command = new SqlCommand("GetProjectById_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Project
                        {
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            ProjectName = reader["ProjectName"].ToString(),
                            Description = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Status = reader["Status"].ToString(),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                        };
                    }
                }
            }

            return null;
        }

        #endregion GetProjects

        public void AddProject(Project project)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectName", project.ProjectName),
                    new SqlParameter("@Description", project.Description),
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = Convert.ToDateTime(project.StartDate) },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = Convert.ToDateTime(project.EndDate) },
                    new SqlParameter("@ClientId", project.ClientId),
                    new SqlParameter("@PriorityLevel", project.PriorityLevel),
                    new SqlParameter("@CreatedDate", DateTime.Now),
                };

                using (SqlCommand command = new SqlCommand("InsertProject_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateProject(int projectId, Project updatedProject)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId", projectId),
                    new SqlParameter("@ProjectName", updatedProject.ProjectName),
                    new SqlParameter("@Description", updatedProject.Description),
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = Convert.ToDateTime(updatedProject.StartDate) },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = Convert.ToDateTime(updatedProject.EndDate) },
                    new SqlParameter("@ClientId", updatedProject.ClientId),
                    new SqlParameter("@Status", updatedProject.Status),
                };

                using (SqlCommand command = new SqlCommand("UpdateProject_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProject(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter parameter = new SqlParameter("@ProjectId", projectId);

                using (SqlCommand command = new SqlCommand("DeleteProject_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertProjectEmployees(int projectId, string employeeIds)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertProjectEmployees_Surya", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProjectId", projectId);
                command.Parameters.AddWithValue("@EmployeeIds", employeeIds);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}