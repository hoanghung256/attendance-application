namespace AttendanceApplication.Models
{
    public class Staff
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Staff() { }

        public Staff(string Id, string Name, int Age)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
        }

        public override string? ToString()  
        {
            return "Staff{ID = " + Id + ", Name = " + Name + ", Age = " + Age + "}";
        }
    }
}
