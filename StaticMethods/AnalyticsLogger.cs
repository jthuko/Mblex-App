using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexPrep
{
   public static  class AnalyticsLogger
    {
        private static string connectionString = "Server=tcp:jtappserver.database.windows.net,1433;Initial Catalog=MblexDB;Persist Security Info=False;User ID=jthuko;Password=Jnzusyo77!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void LogPageVisit(string pageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DateTime visitTimestamp = DateTime.UtcNow;

                string insertQuery = "INSERT INTO PageVisits (PageName, VisitTimestamp) VALUES (@PageName, @VisitTimestamp)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@PageName", pageName);
                    cmd.Parameters.AddWithValue("@VisitTimestamp", visitTimestamp);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void LogInstall()
        {
            string installFlagKey = "IsInstallLogged";
            var settingsJson = SecureStorage.GetAsync(installFlagKey).Result;
            if (string.IsNullOrEmpty(settingsJson))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
                    connection.Open();

                    DateTime installTimestamp = DateTime.UtcNow;

                    // Get the device type
                    string deviceType = DeviceInfo.DeviceType.ToString();
                    string appName = AppInfo.Name;
                    string insertQuery = "INSERT INTO InstallLogs (AppName, InstallDate, DeviceType) VALUES (@AppName, @InstallDate, @DeviceType)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@AppName", appName);
                        cmd.Parameters.AddWithValue("@InstallDate", installTimestamp);
                        cmd.Parameters.AddWithValue("@DeviceType", deviceType);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Set the flag to indicate that the install has been logged
                SecureStorage.SetAsync(installFlagKey, "true");
            }
        }
    }
}
