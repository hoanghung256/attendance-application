using AttendanceApplication.Models.DAO;
using AttendanceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AttendanceApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        public IActionResult Privacy()
        {
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
                return RedirectToAction("Login", "Account");

            }
            return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}