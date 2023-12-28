namespace AttendanceApplication.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

        public Login() { }

        public Login(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
