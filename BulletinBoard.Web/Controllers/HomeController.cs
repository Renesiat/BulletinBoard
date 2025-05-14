using BulletinBoard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulletinBoard.Web.Controllers
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginModal", new LoginViewModel());
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                TempData["Message"] = "Login successful!";
                return RedirectToAction("Index");
            }

            return PartialView("_LoginModal", model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
