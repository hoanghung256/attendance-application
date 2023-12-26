using AttendanceApplication.Models;
using AttendanceApplication.Models.DAO;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApplication.Controllers	
{
	public class StaffController : Controller
	{
		// GET: StudentController
		public ActionResult Index()
		{
            var userLogedIn = HttpContext.Request.Cookies["userLogedIn"];
            if (userLogedIn == null || userLogedIn != "true")
            {
				return RedirectToAction("Login", "Account");
               
            }
            StaffDAO staffDAO = new StaffDAO();
            List<Staff> staffs = staffDAO.SelectAll();
            return View(staffs);
        }

		// GET: StudentController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: StudentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: StudentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: StudentController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: StudentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: StudentController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: StudentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
