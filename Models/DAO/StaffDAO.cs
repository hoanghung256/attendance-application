using fullstack_project.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace fullstack_project.Models.DAO
{
    public class StaffDAO
    {
        static string connString = "Data Source=localhost; Initial Catalog=AttendanceApplication; User ID=SA; Password=2562004; Integrated Security=True";      //a key to connect to a SQL server
        public List<Staff> SelectAll()
        {
            List<Staff> staffList = new List<Staff>();
            using (SqlConnection dbConnection = new SqlConnection(connString))      //initilize a new connection 
            {
                dbConnection.Open();        //open DB
                DbTransaction transaction = dbConnection.BeginTransaction();        //initilize a new DbTransaction object (DbTransaction class define behaviours of transactions toward DB)
                try
                {
                    int timeout = 30;       //wait time for SQL commands
                    string commandText = "SELECT * FROM Staff";     //contain SQL commands
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;       //set time out for command, if SQL commands execute beyound this time, it will be terminated and throw an exception

                    // load

                    // if select
                    using (SqlDataReader reader = command.ExecuteReader())      //initilize a reader
                    {
                        while (reader.Read())       //Read() return a boolean value
                        {
                            staffList.Add(new Staff((string)reader[0], (string)reader[1], (int)reader[2]));
                            // return staff list here
                            Console.WriteLine(String.Format("{0}", reader[0]));
                            Console.WriteLine(String.Format("{0}", reader[1]));
                            Console.WriteLine(String.Format("{0}", reader[2]));
                        }
                    }
                    // if insert, update, delete use:
                    // command.ExecuteNonQuery(); 

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
            }

            return staffList;
        }

        //public List<Staff> Add(Staff addStaff)
        //{
        //    List<Staff> staffList = new List<Staff>();
        //    using (SqlConnection dbConnection = new SqlConnection(connString))
        //    {
        //        dbConnection.Open();
        //        DbTransaction transaction = dbConnection.BeginTransaction();
        //        try
        //        {
        //            string commandText = "INSERT INTO [dbo].[Staff]" +
        //                "([Id],[Name],[Age])" +
        //                "VALUES('"+ addStaff.Id +"','"+ addStaff.Name +"', '"+ addStaff.Age +"')";
        //            SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);       //SqlCommand is a class represents a Transact-SQL statement or stored procedure to execute against db. This class cannot be inherited. 

        //            int insertResult = command.ExecuteNonQuery();       //ExecuteNonQuery() will return a int value, show status of excecution

        //            if (insertResult == 0) {
        //                Console.WriteLine("Insert failed!");
        //            } 
        //            else
        //            {
        //                transaction.Commit();       //commit change into db
        //                Console.WriteLine("Insert successfull!");
        //            }
        //        }
        //        catch (SqlException ex) 
        //        {
        //            // if exception occur => use rollback to prevent data lossing
        //            transaction.Rollback();
        //            Console.WriteLine(ex.Message);
        //        }
        //    }

        //    return staffList;
        //}
    }
}
