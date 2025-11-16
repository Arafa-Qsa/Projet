using LearnHub.Data;
using LearnHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await db.Categories.ToListAsync(); 
            return View(categories);
        }
    }
}