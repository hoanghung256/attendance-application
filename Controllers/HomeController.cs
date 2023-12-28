using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApplication.Controllers
{
    public class HomeController : Controller
    {
        static AttendanceDAO dao = new AttendanceDAO();
        public IActionResult Index()
        {
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        public IActionResult CheckIn(string position)
        {
            if (HttpContext.Request.Cookies["workingStatus"] == "working")
            {
                SetViewBag("Check In failed!", "You need to Check Out before!");
                return PartialView("_CheckIn");
            }
            DateTime currentDateTime = DateTime.Now;
            string checkInTime = TimeOnly.FromTimeSpan(currentDateTime.TimeOfDay).ToString(@"hh\:mm\:ss");
            string checkInDate = DateOnly.FromDateTime(currentDateTime).ToString("yyyy-MM-dd");
            var username = HttpContext.Request.Cookies["username"];

            AttendanceStatus userStatus = dao.InsertCheckInStatus(username, checkInTime, checkInDate, position);

            if (userStatus == null)
            {
                SetViewBag("Check In failed!", "There are may occur a error, you need to Check In again!");
                return PartialView("_CheckIn");
            }
            Response.Cookies.Append("workingStatus", "working");
            Response.Cookies.Append("checkInTime", checkInTime);
            Response.Cookies.Append("checkInDate", checkInDate);
            SetViewBag("Check In successfully!", null);
            return PartialView("_CheckIn", userStatus);
        }

        public IActionResult CheckOut()
        {
            DateTime currentDateTime = DateTime.Now;
            string checkOutTime = TimeOnly.FromTimeSpan(currentDateTime.TimeOfDay).ToString(@"hh\:mm\:ss");
            string checkOutDate = DateOnly.FromDateTime(currentDateTime).ToString("yyyy-MM-dd");

            var checkInTime = HttpContext.Request.Cookies["checkInTime"];
            var checkInDate = HttpContext.Request.Cookies["checkInDate"];
            var workingStatus = HttpContext.Request.Cookies["workingStatus"];
            var username = HttpContext.Request.Cookies["username"];

            if (workingStatus == null || workingStatus == "off")
            {
                SetViewBag("Check Out failed!", "You need to Check-In first!");
                return PartialView("_CheckIn");
            }
            else if (checkInDate != checkOutDate)
            {
                Console.WriteLine(checkInDate.ToString());
                Console.WriteLine(checkOutDate);
                SetViewBag("Check Out failed!", "The date you check-in is different from the check-out date, system will not count for this session!");
                Response.Cookies.Append("workingStatus", "off");
                return PartialView("_CheckIn");
            }
            else if (!dao.UpdateCheckOutStatus(username, checkOutTime))
            {
                SetViewBag("Check Out failed!", "There may occur an error, you need to Check Out again!");
                return PartialView("_CheckIn");
            }
            var timeOfWork = TimeOnly.Parse(checkOutTime) - TimeOnly.Parse(checkInTime);
            Response.Cookies.Append("workingStatus", "off");
            SetViewBag("Check Out successfully!", "Time of work: " + timeOfWork.ToString());
            return PartialView("_CheckOut");
        }

        private void SetViewBag(string? status, string? message)
        {
            ViewBag.Status = status;
            ViewBag.Message = message;
        }

    }
}
