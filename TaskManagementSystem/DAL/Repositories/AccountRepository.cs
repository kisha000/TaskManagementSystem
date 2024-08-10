using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TaskManagementSystem.DAL.Repositories
{
    public class AccountRepository
    {
        #region Global Declarations

        private readonly string connectionString;
        private SqlConnection connection;

        public AccountRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TaskMgmtSystemCon"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        #endregion

        #region ResetPassword

        public void UpdateEmployeePassword(string email, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateEmployeePassword_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }
        public bool IsPasswordResetValid(string email)
        {
            bool isValid = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("IsPasswordResetValid_Surya", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    isValid = Convert.ToBoolean(command.ExecuteScalar());
                }

                // Check link expiry
                if (isValid)
                {
                    DateTime linkGeneratedAt;
                    using (SqlCommand command = new SqlCommand("GetResetLinkGeneratedAt", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        linkGeneratedAt = Convert.ToDateTime(command.ExecuteScalar());
                    }

                    TimeSpan timeDifference = DateTime.Now - linkGeneratedAt;
                    if (timeDifference.TotalMinutes > 5) // 5 minutes expiry time
                    {
                        isValid = false; // Link expired
                    }
                }
            }

            return isValid;
        }



        public void SetPasswordResetStatus(string email, bool isReset)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdatePasswordResetStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@IsReset", isReset);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateResetLinkTimestamp(string email)
        {
            using (SqlCommand command = new SqlCommand("UpdateResetLinkTimestamp_Surya", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}