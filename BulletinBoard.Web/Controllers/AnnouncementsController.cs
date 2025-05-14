using BulletinBoard.Data.Models;
using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Web.Models;

namespace BulletinBoard.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnnouncementsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public JsonResult GetSubcategories(string category)
        {
            if (CategoryData.Categories.TryGetValue(category, out var subcategories))
            {
                return Json(subcategories);
            }
            return Json(new List<string>());
        }


        //public async Task<IActionResult> Index()
        //{
        //    var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
        //    var announcements = await client.GetFromJsonAsync<List<Announcement>>("announcements");

        //    return View(announcements);
        //}
        public async Task<IActionResult> Index(string? category)
        {
            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var announcements = await client.GetFromJsonAsync<List<Announcement>>("announcements") ?? [];

            if (!string.IsNullOrEmpty(category))
            {
                announcements = announcements.Where(a => a.Category == category).ToList();
            }

            ViewBag.Categories = CategoryData.Categories.Keys.ToList();
            ViewBag.SelectedCategory = category;

            return View(announcements);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new AnnouncementFormViewModel
            {
                Categories = CategoryData.Categories.Keys.ToList(),
                SelectedCategory = null,
                SubCategories = new List<string>(),
                Announcement = new Announcement()
            };

            return View(vm);
        }


        [HttpPost]

        public async Task<IActionResult> Create(AnnouncementFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = CategoryData.Categories.Keys.ToList();
                vm.SubCategories = vm.SelectedCategory != null && CategoryData.Categories.ContainsKey(vm.SelectedCategory)
                    ? CategoryData.Categories[vm.SelectedCategory]
                    : new List<string>();

                return View(vm);
            }

            vm.Announcement.Category = vm.SelectedCategory!;
            vm.Announcement.CreatedDate = DateTime.Now;

            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var response = await client.PostAsJsonAsync("announcements", vm.Announcement);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            // Fail — reload lists again
            vm.Categories = CategoryData.Categories.Keys.ToList();
            vm.SubCategories = vm.SelectedCategory != null && CategoryData.Categories.ContainsKey(vm.SelectedCategory)
                ? CategoryData.Categories[vm.SelectedCategory]
                : new List<string>();

            return View(vm);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id, string? category)
        {
            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var announcement = await client.GetFromJsonAsync<Announcement>($"announcements/{id}");

            if (announcement == null)
                return NotFound();

            var selectedCategory = category ?? announcement.Category;

            var viewModel = new AnnouncementFormViewModel
            {
                Announcement = announcement,
                Categories = CategoryData.Categories.Keys.ToList(),
                SelectedCategory = selectedCategory,
                SubCategories = CategoryData.Categories.ContainsKey(selectedCategory)
                    ? CategoryData.Categories[selectedCategory]
                    : new List<string>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnnouncementFormViewModel model)
        {
            if (id != model.Announcement.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                model.Categories = CategoryData.Categories.Keys.ToList();
                model.SubCategories = model.SelectedCategory != null && CategoryData.Categories.ContainsKey(model.SelectedCategory)
                    ? CategoryData.Categories[model.SelectedCategory]
                    : new List<string>();

                return View(model);
            }

            model.Announcement.Category = model.SelectedCategory!;

            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var response = await client.PutAsJsonAsync($"announcements/{id}", model.Announcement);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "An error occurred while updating the announcement.");

            model.Categories = CategoryData.Categories.Keys.ToList();
            model.SubCategories = model.SelectedCategory != null && CategoryData.Categories.ContainsKey(model.SelectedCategory)
                ? CategoryData.Categories[model.SelectedCategory]
                : new List<string>();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var announcement = await client.GetFromJsonAsync<Announcement>($"announcements/{id}");

            if (announcement == null)
                return NotFound();

            return View(announcement);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("BulletinBoardAPI");
            var response = await client.DeleteAsync($"announcements/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "Failed to delete the announcement.");
            return View();
        }
    }
}
