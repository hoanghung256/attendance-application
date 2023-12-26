using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace AttendanceApplication.Controllers
{
    public class AttendanceController : Controller
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
            DateTime currentDateTime = DateTime.Now;
            TimeOnly currentTime = TimeOnly.FromTimeSpan(currentDateTime.TimeOfDay);
            DateOnly currentDate = DateOnly.FromDateTime(currentDateTime);
            var username = HttpContext.Request.Cookies["username"];
            AttendanceStatus userStatus = new AttendanceStatus(username, currentTime, currentDate, position);
            if (!dao.InsertCheckInStatus(username, currentTime.ToString(@"hh\:mm\:ss"), currentDate.ToString("yyyy-MM-dd"), position))
            {
                return View("CheckInFailed");
            }
            //return PartialView("CheckInSucceed");
            return View(userStatus);
        }

        public IActionResult CheckOut() 
        {
            DateTime currentDateTime = DateTime.Now;
            string currentTime = currentDateTime.TimeOfDay.ToString(@"hh\:mm\:ss");
            var username = HttpContext.Request.Cookies["username"];
            if (!dao.UpdateCheckOutStatus(username, currentTime))
            {
                return View("CheckOutFailed");
            }
            return View("Index");
        }

    }
}
