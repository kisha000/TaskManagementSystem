using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DAL.Repositories
{
    public class TaskRepository
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public TaskRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public List<Task> GetAllTasks()
        {
            List<Task> tasks = new List<Task>();

            using (SqlCommand command = new SqlCommand("GetAllTasks_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            TaskName = reader["TaskName"].ToString(),
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EstimateDate = Convert.ToDateTime(reader["EstimateDate"]),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            AttachmentFile = reader["AttachmentFile"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                            EmployeeId = reader["EmployeeId"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeId"]) : (int?)null,
                            EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                            ProjectName = reader["ProjectName"].ToString(),
                        });
                    }
                }
            }

            return tasks;
        }

        public List<Task> GetAllOpenTasks()
        {
            List<Task> tasks = new List<Task>();

            using (SqlCommand command = new SqlCommand("GetAllOpenedTasks_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            TaskName = reader["TaskName"].ToString(),
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EstimateDate = Convert.ToDateTime(reader["EstimateDate"]),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            AttachmentFile = reader["AttachmentFile"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                            EmployeeId = reader["EmployeeId"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeId"]) : (int?)null,
                            EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                            ProjectName = reader["ProjectName"].ToString(),
                        });
                    }
                }
            }

            return tasks;
        }
        public List<Task> GetTasksByEmployee(int employeeId)
        {
            List<Task> tasks = new List<Task>();

            using (SqlCommand command = new SqlCommand("GetTasksByEmployee_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            TaskName = reader["TaskName"].ToString(),
                            ProjectName = reader["ProjectName"].ToString(),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EstimateDate = Convert.ToDateTime(reader["EstimateDate"]),
                            Status = reader["Status"].ToString(),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                        });
                    }
                }
            }

            return tasks;
        }

        public Task GetTaskById(int taskId)
        {
            SqlParameter parameter = new SqlParameter("@TaskId", taskId);

            using (SqlCommand command = new SqlCommand("GetTaskById_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(parameter);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Task
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            TaskName = reader["TaskName"].ToString(),
                            ProjectId = Convert.ToInt32(reader["ProjectId"]),
                            TaskDescription = reader["TaskDescription"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EstimateDate = Convert.ToDateTime(reader["EstimateDate"]),
                            PriorityLevel = reader["PriorityLevel"].ToString(),
                            AttachmentFile = reader["AttachmentFile"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                            EmployeeId = reader["EmployeeId"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeId"]) : (int?)null,
                            EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : null,
                            ProjectName = reader["ProjectName"].ToString(),
                        };
                    }
                }
            }

            return null;
        }

        public void AddTask(Task task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@TaskName", task.TaskName),
                    new SqlParameter("@ProjectId", task.ProjectId),
                    new SqlParameter("@TaskDescription", task.TaskDescription),
                    new SqlParameter("@StartDate", task.StartDate),
                    new SqlParameter("@EstimateDate", task.EstimateDate),
                    new SqlParameter("@PriorityLevel", task.PriorityLevel),
                    new SqlParameter("@EmployeeId", task.EmployeeId),
                };

                using (SqlCommand command = new SqlCommand("InsertTask_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTask(int taskId, Task task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@TaskId", taskId),
                    new SqlParameter("@TaskName", task.TaskName),
                    new SqlParameter("@ProjectId", task.ProjectId),
                    new SqlParameter("@TaskDescription", task.TaskDescription),
                    new SqlParameter("@StartDate", task.StartDate),
                    new SqlParameter("@EstimateDate", task.EstimateDate),
                    new SqlParameter("@PriorityLevel", task.PriorityLevel),
                    new SqlParameter("@ModifiedDate", DateTime.Now),
                    new SqlParameter("@EmployeeId", task.EmployeeId),
                };

                using (SqlCommand command = new SqlCommand("UpdateTask_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter parameter = new SqlParameter("@TaskId", taskId);

                using (SqlCommand command = new SqlCommand("DeleteTask_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public int GetNextTaskId()
        {
            int nextTaskId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetNextTaskId", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    nextTaskId = Convert.ToInt32(reader["NextTaskId"]);
                }

                reader.Close();
            }

            return nextTaskId;
        }
    }
}