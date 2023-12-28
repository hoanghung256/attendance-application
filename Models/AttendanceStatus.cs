namespace AttendanceApplication.Models
{
    public class AttendanceStatus
    {
        private string Username { get; set; }
        private TimeOnly StartTime { get; set; }
        private TimeOnly FinishTime { get; set; }
        private DateOnly AttendanceDate { get; set; }
        private string Position { get; set; }

        public AttendanceStatus() { }

        public AttendanceStatus(string username, TimeOnly startTime, TimeOnly finishTime, DateOnly attendanceDate, string position)
        {
            Username = username;
            StartTime = startTime;
            FinishTime = finishTime;
            AttendanceDate = attendanceDate;
            Position = position;
        }

        public AttendanceStatus(string username, TimeOnly startTime, DateOnly attendanceDate, string position)
        {
            Username = username;
            StartTime = startTime;
            AttendanceDate = attendanceDate;
            Position = position;
        }
    }
}
