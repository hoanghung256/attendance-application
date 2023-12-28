namespace AttendanceApplication.Models
{
    public class AttendanceStatus
    {
        public string Username { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly FinishTime { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public string Position { get; set; }

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
