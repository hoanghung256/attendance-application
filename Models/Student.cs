namespace fullstack_project.Models
{
    public class Student
    {
        public int Id;
        public string Name;
        public int Age; 

        public Student(int Id, string Name, int Age)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
        }
    }
}
