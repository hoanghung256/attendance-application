using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace AttendanceApplication.Controllers
{
    public class AttendanceController : Controller
    {
        static AttendanceDAO dao = new AttendanceDAO();
        public IActionResult Dashboard()
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
            var username = HttpContext.Request.Cookies["username"];
            if (!dao.InsertCheckInStatus(username, position))
            {
                return View("CheckInFailed");
            }
            return View("Dashboard");
        }

        public IActionResult CheckOut() 
        {
            var username = HttpContext.Request.Cookies["username"];
            if (!dao.UpdateCheckOutStatus(username))
            {
                return View("CheckOutFailed");
            }
            return View("Dashboard");
        }

    }
}
