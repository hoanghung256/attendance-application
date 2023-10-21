using Microsoft.AspNetCore.Mvc;
using fullstack_project.Models;
using fullstack_project.Models.DAO;

namespace fullstack_project.Controllers
{
    public class AccountController : Controller
    {
        //Get: Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string username = model.Username;
            string password = model.Password;
            
            LoginDAO loginDAO = new LoginDAO(); 
            model = loginDAO.checkLogin(username, password);
            if (model.Username != null)
            {
                Console.WriteLine(model.Username);
                Console.WriteLine(model.Password);
                return View("LoginSucceed");
            }
            return View("LoginFailed");
        }
    }
}
