using LearnHub.Data;
using LearnHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Controllers
{
    public class FaceController : Controller
    {
        private readonly ApplicationDbContext db;
        public FaceController(ApplicationDbContext _db)
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