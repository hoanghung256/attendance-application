using Microsoft.AspNetCore.Mvc;
using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;

namespace AttendanceApplication.Controllers
{
    public class AccountController : Controller
    {
        //Get: Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            string username = model.Username;
            string password = model.Password;
            
            LoginDAO loginDAO = new LoginDAO(); 
            model = loginDAO.checkLogin(username, password);
            if (model.Username == null)
            {
                ViewBag.Error = "Login failed. Please try again!";
                return View();
            } 
            Response.Cookies.Append("userLogedIn", "true");
            Response.Cookies.Append("username", username);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login");
        }
    }
}
