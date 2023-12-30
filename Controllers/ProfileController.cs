using Microsoft.AspNetCore.Mvc;
using AttendanceApplication.DAO;
using AttendanceApplication.Models;

namespace AttendanceApplication.Controllers
{
    public class ProfileController : Controller
    {
        StaffDAO dao = new StaffDAO();
        public IActionResult Detail()
        {
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
                return RedirectToAction("Login", "Account");

            }
            var username = HttpContext.Request.Cookies["username"];
            Staff staff = dao.selectUserDetail(username);
            return PartialView("_Detail", staff);
        }
    }
}
