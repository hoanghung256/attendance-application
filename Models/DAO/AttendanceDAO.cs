using fullstack_project.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace AttendanceApplication.Models.DAO
{
    public class AttendanceDAO
    {
        private const string connString = "Data Source=localhost; Initial Catalog=AttendanceApplication; User ID=SA; Password=2562004; Integrated Security=True";

        public AttendanceDAO() { }  

        public bool InsertCheckInStatus(string username, string position)
        {
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();   
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    string commandText = "INSERT INTO [AttendanceApplication].[dbo].[Attendance] (username, start_time, attendance_date, position) " +
                        "VALUES ('" + username + "', CURRENT_TIMESTAMP, GETDATE(), '" + position + "')";  
                    SqlCommand command = new SqlCommand(commandText, transaction.Connection as SqlConnection, (SqlTransaction)transaction);
                    command.CommandTimeout = 30;

                    command.ExecuteNonQuery(); // Execute the command

                    // if insert, update, delete use Commit()
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // if exception occur => use rollback to prevent data lossing
                    transaction.Rollback();
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    return false;
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }

        public bool UpdateCheckOutStatus(string username)
        {
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    string commandText = "UPDATE [AttendanceApplication].[dbo].[Attendance] " +
                        "SET finish_time=CURRENT_TIMESTAMP " +
                        "WHERE username='" + username + "' AND finish_time IS NULL";
                    SqlCommand command = new SqlCommand(commandText, transaction.Connection as SqlConnection, (SqlTransaction)transaction);
                    command.CommandTimeout = 30;

                    command.ExecuteNonQuery(); // Execute the command

                    // if insert, update, delete use Commit()
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // if exception occur => use rollback to prevent data lossing
                    transaction.Rollback();
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    return false;
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
    }
}
