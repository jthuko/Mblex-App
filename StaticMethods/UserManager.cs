using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp
{
    public static class UserManager
    {
        private static string connectionString = "Server=tcp:jtappserver.database.windows.net,1433;Initial Catalog=MblexDB;Persist Security Info=False;User ID=jthuko;Password=Jnzusyo77!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static bool LoginUser(string usernameOrEmail, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                bool loginSuccess = AttemptLogin(connection, usernameOrEmail, password);
                return loginSuccess;
            }
        }

        private static bool AttemptLogin(SqlConnection connection, string usernameOrEmail, string password)
        {
            using (var command = new SqlCommand("sp_GetPasswordHash", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
                command.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 128) { Direction = ParameterDirection.Output });

                command.ExecuteNonQuery();

                // Retrieve the password hash from the output parameter
                string storedPasswordHash = command.Parameters["@PasswordHash"].Value as string;

                if (!string.IsNullOrEmpty(storedPasswordHash) && VerifyPassword(password, storedPasswordHash))
                {
                    return true; // Login successful
                }
            }

            return false; // Login failed
        }

        // Rest of the code remains the same
        private static bool VerifyPassword(string password, string storedPasswordHash)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return storedPasswordHash == hashedPassword;
            }
        }

        public static bool CreateUser(string username, string email, string password)
        {
            // Hash the user's password
            string passwordHash = HashPassword(password);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the username or email is already in use (optional)
                if (IsUsernameOrEmailInUse(connection, username, email))
                {
                    return false; // Username or email is already in use
                }

                // Call the stored procedure to insert the user into the database
                using (var command = new SqlCommand("sp_InsertUser", connection))
                {
                    var isPremium = 0;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@IsPremium", isPremium);

                    command.ExecuteNonQuery();
                }

                return true; // User was successfully created
            }
        }

        private static bool IsUsernameOrEmailInUse(SqlConnection connection, string username, string email)
        {
            // Define a SELECT query to check if the username or email exists in the Users table.
            string selectQuery = "SELECT 1 FROM Users WHERE Username = @Username OR Email = @Email";

            using (var command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    // If the reader has any rows, that means the username or email is already in use.
                    return reader.HasRows;
                }
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }

}
