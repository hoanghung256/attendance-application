using System.Data.SqlClient;
using System.Data.Common;

namespace fullstack_project.Models.DAO
{
    public class LoginDAO
    {
        private const string connString = "Data Source=localhost; Initial Catalog=AttendanceApplication; User ID=SA; Password=2562004; Integrated Security=True";
        public LoginModel checkLogin(string username, string password)
        {
            LoginModel loginModel = new LoginModel();
            using (SqlConnection dbConnection = new SqlConnection(connString))      //initilize a new connection 
            {
                dbConnection.Open();        //open DB
                DbTransaction transaction = dbConnection.BeginTransaction();        //initilize a new DbTransaction object (DbTransaction class define behaviours of transactions toward DB)
                try
                {
                    int timeout = 30;       //wait time for SQL commands
                    string commandText = "SELECT * FROM [AttendanceApplication].[dbo].[Account]" +
                        "WHERE username ='" + username + "' AND password = '" + password + "'";     //contain SQL commands
                    SqlCommand command = new SqlCommand(commandText, transaction.Connection as SqlConnection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;       //set time out for command, if SQL commands execute beyound this time, it will be terminated and throw an exception

                    // load

                    // if select
                    using (SqlDataReader reader = command.ExecuteReader())      //initilize a reader
                    {
                        while (reader.Read()) {
                            loginModel.Username = (string)reader[0];
                            loginModel.Password = (string)reader[1];
                            loginModel.Role = (string)reader[2];
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

            return loginModel;
        }
    }
}

