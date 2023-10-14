namespace fullstack_project.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Staff() { }

        public Staff(int Id, string Name, int Age)
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
