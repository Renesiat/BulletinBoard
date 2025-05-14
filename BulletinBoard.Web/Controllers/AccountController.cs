using BulletinBoard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace BulletinBoard.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Please fill all fields correctly.";
                return RedirectToAction("Index", "Home");
            }

            var httpClient = _httpClientFactory.CreateClient("BulletinBoardAPI");

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("users", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["LoginSuccess"] = "Registration successful!";
                return RedirectToAction("Index", "Announcements");
            }

            TempData["LoginError"] = "Registration failed. Try a different username.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "All fields are required.";
                return RedirectToAction("Index", "Home");
            }

            var httpClient = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("users/login", content);

            if (response.IsSuccessStatusCode)
            {

                HttpContext.Session.SetString("Username", model.UserName);

                TempData["LoginSuccess"] = "Login successful!";
                return RedirectToAction("Index", "Announcements");
            }

            TempData["LoginError"] = "Invalid username or password.";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
