using System.Data.Common;
using System.Data.SqlClient;

namespace AttendanceApplication.Models.DAO
{
    public class AttendanceDAO
    {
        private const string connString = "Data Source=localhost; Initial Catalog=AttendanceApplication; User ID=SA; Password=2562004; Integrated Security=True";

        public AttendanceDAO() { }

        public AttendanceStatus InsertCheckInStatus(string username, string startTime, string currentDate, string position)
        {
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO [AttendanceApplication].[dbo].[Attendance] (username, start_time, attendance_date, position) " +
                         "VALUES (@Username, @StartTime, @CurrentDate, @Position)";

                        using (SqlCommand command = new SqlCommand(query, dbConnection))
                        {
                            command.Transaction = (SqlTransaction)transaction;
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@StartTime", startTime);
                            command.Parameters.AddWithValue("@CurrentDate", currentDate);
                            command.Parameters.AddWithValue("@Position", position);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }

                        // if insert, update, delete use Commit()
                        return new AttendanceStatus(username, TimeOnly.Parse(startTime), DateOnly.Parse(currentDate), position);
                    }
                    catch (Exception ex)
                    {
                        // if exception occur => use rollback to prevent data lossing
                        transaction.Rollback();
                        Console.WriteLine(ex.StackTrace);
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
        }

        public bool UpdateCheckOutStatus(string username, string finishTime)
        {
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        string query = "UPDATE [AttendanceApplication].[dbo].[Attendance] " +
                            "SET finish_time=@FinishTime " +
                            "WHERE username=@Username AND finish_time IS NULL";

                        using (SqlCommand command = new SqlCommand(query, dbConnection))
                        {
                            command.Transaction = (SqlTransaction)transaction;
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@FinishTime", finishTime);

                            command.CommandTimeout = 30;
                            command.ExecuteNonQuery();
                        }

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
}
