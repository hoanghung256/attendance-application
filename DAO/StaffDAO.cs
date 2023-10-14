using fullstack_project.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace fullstack_project.DAO
{
	public class StaffDAO
	{
		static string connString = "Data Source=localhost; Initial Catalog=myFullstackDB; User ID=SA; Password=2562004; Integrated Security=True";
        public List<Staff> SelectAll()
        {
            List<Staff> staffList = new List<Staff>();
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM Staff";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // load

                    // if select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staffList.Add(new Staff((int)reader[0], (string)reader[1], (int)reader[2]));
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
                catch
                {
                    // if exception occur => use rollback to prevent data lossing
                    transaction.Rollback();
                    throw;
                }
            }

            return staffList;
        }

  //      public List<Staff> GetStaffList()
		//{
		//	List<Staff> staffs = new List<Staff>();
  //          staffs.Add(new Staff(1, "Nguyen A", 19));
  //          staffs.Add(new Staff(2, "Nguyen B", 20));
  //          staffs.Add(new Staff(3, "Nguyen C", 25));

		//	return staffs;
		//}
	}
}
