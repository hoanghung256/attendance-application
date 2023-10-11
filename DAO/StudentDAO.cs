using fullstack_project.Models;

namespace fullstack_project.DAO
{
	public class StudentDAO
	{
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
