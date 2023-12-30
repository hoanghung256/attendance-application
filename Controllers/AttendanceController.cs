using AttendanceApplication.DAO;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApplication.Controllers
{
    public class AttendanceController : Controller
    {
        AttendanceDAO dao = new AttendanceDAO();
        public IActionResult Index()
        {
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
                return RedirectToAction("Login", "Account");

            }
            return View();
        }
    }
}
