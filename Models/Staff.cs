namespace AttendanceApplication.Models
{
    public class Staff
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Dob { get; set; }
        public double SalaryScale { get; set; }
        public string Department { get; set; }
        public int AnnualLeave { get; set; }

        public Staff() { }

        public Staff(string username, string firstName, string lastName, DateOnly dob, double salaryScale, string department, int annualLeave) 
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
            SalaryScale = salaryScale;
            Department = department;
            AnnualLeave = annualLeave;
        }
    }
}
