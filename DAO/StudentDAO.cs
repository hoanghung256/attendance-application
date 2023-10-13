using fullstack_project.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace fullstack_project.DAO
{
	public class StudentDAO
	{
		static string conn_string = "Data Source=localhost; Initial Catalog=MYDBNAME; User ID=sa; Password=2562004; Integrated Security=True";
		public List<Student> Select(string manv)
		{
			using (SqlConnection db_connection = new SqlConnection(conn_string))
			{
				db_connection.Open();
				DbTransaction transaction = db_connection.BeginTransaction();
				try
				{
					int timeout = 30;
					string command_text = "SELECT * FROM MYTABLE WHERE MaNV=@manv";
					SqlCommand command = new SqlCommand(command_text, transaction.Connection as SqlConnection, (SqlTransaction.));
					command.CommandTimeout = timeout;

					//	set parameters
					command.Parameters.Add("@id", SqlDbType.VarChar).Value = manv;
					//	load

					//	if select
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//return student list here
							Console.WriteLine(String.Format("{0", reader[0]));
						}
					}

					//	if insert, update, delete
					//	commad.ExecuteNonQuery();

					// cause select => db is unchange, after select use rollback to make sure
					transaction.Rollback();

					//	if insert, update, delete => use transaction.Commit();
				}
				catch
				{
					// if exception throwed, use rollback to prevent data lossing
					transaction.Rollback();
					throw;
				}
			}

			return null;
		}

		public List<Student> GetStudentList()
		{
			List<Student> students = new List<Student>();
			students.Add(new Student(1, "Nguyen A", 19));
			students.Add(new Student(2, "Nguyen B", 20));
			students.Add(new Student(3, "Nguyen C", 25));

			return students;
		}
	}
}
