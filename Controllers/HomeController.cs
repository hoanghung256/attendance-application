using fullstack_project.DAO;
using fullstack_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace fullstack_project.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            StaffDAO staffDAO = new StaffDAO();
            staffDAO.Add(new Staff(4, "Nguyen Van D", 22));
            return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}