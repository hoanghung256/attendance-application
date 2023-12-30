using System.Data.SqlClient;
using System.Data.Common;
using AttendanceApplication.Models;

namespace AttendanceApplication.DAO
{
    public class LoginDAO
    {
        private const string connString = "Data Source=localhost; Initial Catalog=AttendanceApplication; User ID=SA; Password=2562004; Integrated Security=True";
        public Login checkLogin(string username, string password)
        {
            Login loginModel = new Login();
            using (SqlConnection dbConnection = new SqlConnection(connString))      //initilize a new connection 
            {
                dbConnection.Open();        //open DB
                using (SqlTransaction transaction = dbConnection.BeginTransaction())        //initilize a new SqlTransaction object
                {
                    try
                    {
                        string query = "SELECT * FROM [AttendanceApplication].[dbo].[Account]" +
                            "WHERE username=@Username AND password=@Password";

                        using (SqlCommand command = new SqlCommand(query, dbConnection, transaction)) // Pass the transaction to the SqlCommand
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);

                            command.CommandTimeout = 30;

                            // When SELECT you need to use ExecuteReader    
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    loginModel.Username = (string)reader[0];
                                    loginModel.Password = (string)reader[1];
                                    loginModel.Role = (string)reader[2];
                                }
                            }
                        }

                        //  cause select => db unchange, after select use rollback to make sure
                        transaction.Rollback();
                        // if insert, update, delete => use: transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // if exception occur => use rollback to prevent data lossing
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }

            return loginModel;
        }
    }
}

