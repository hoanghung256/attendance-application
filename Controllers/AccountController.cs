using Microsoft.AspNetCore.Mvc;
using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;

namespace AttendanceApplication.Controllers
{
    public class AccountController : Controller
    {
        //Get: Login
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string username = model.Username;
            string password = model.Password;

            LoginDAO loginDAO = new(); 
            model = loginDAO.checkLogin(username, password);
            if (model.Username != null)
            {
                Console.WriteLine(model.Username);
                Console.WriteLine(model.Password);
                Console.WriteLine(model.Role);
                Response.Cookies.Append("userLogedIn", "true");
                Response.Cookies.Append("username", username);
                return RedirectToAction("Index", "Attendance");
            }
            return View();
        }
    }
}
