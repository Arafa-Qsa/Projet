using LearnHub.Data;
using LearnHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LearnHub.Models.ViewModels;
namespace LearnHub.Controllers
{
    //[Authorize]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }


        //  InstructorController.cs

        public async Task<IActionResult> Apply() // تم إضافة async Task<IActionResult>
        {
            // 1. جلب قائمة المدربين المعتمدين للعرض
            var approvedInstructors = await _context.InstructorApplications
                                                    .Where(a => a.IsApproved == true)
                                                    .Include(a => a.ApplicationUser)
                                                    .ToListAsync();

            // 2. إنشاء الـ ViewModel
            var viewModel = new InstructorApplicationViewModel
            {
                ApprovedInstructors = approvedInstructors,
                Application = new InstructorApplication() // نموذج فارغ لحقول الإدخال
            };

            return View(viewModel);
        }
        // في ملف InstructorController.cs

        // دالة جديدة لعرض قائمة المدربين (سواء تم اعتمادهم أو كل المستخدمين حسب رؤيتك)
        public async Task<IActionResult> Index()
        {
            // هذا الكود يستعرض فقط الطلبات التي تم اعتمادها (IsApproved = true)
            var approvedInstructors = await _context.InstructorApplications
                                                    .Where(a => a.IsApproved == true)
                                                    // 💡 Include لجلب بيانات المستخدم (الاسم، البريد) من جدول ApplicationUser
                                                    .Include(a => a.ApplicationUser)
                                                    .ToListAsync();

            return View(approvedInstructors);
        }
        // في ملف InstructorController.cs
        // ... (أضف هذا أسفل دالة Index)

        // 💡 يجب أن يكون المستخدم مسجل دخوله وبدور (Role) 'Admin' لكي يصل لهذه الصفحة
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewApplications()
        {
            // جلب جميع الطلبات التي لم يتم اعتمادها بعد (IsApproved = false)
            var pendingApplications = await _context.InstructorApplications
                                                    .Include(a => a.ApplicationUser)
                                                    .Where(a => a.IsApproved == false)
                                                    .ToListAsync();

            return View(pendingApplications);
        }

        // 💡 دالة POST لاعتماد الطلب
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var application = await _context.InstructorApplications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            application.IsApproved = true; // تعيين حالة الاعتماد إلى 'صحيح'
                                           // 💡 يمكنك إضافة منطق هنا لتعيين دور (Role) المستخدم كـ "Instructor" في نظام Identity

            _context.InstructorApplications.Update(application);
            await _context.SaveChangesAsync();

            TempData["Success"] = "تم اعتماد طلب المدرب بنجاح.";
            return RedirectToAction(nameof(ReviewApplications));
        }
        // داخل InstructorController.cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(InstructorApplicationViewModel viewModel) // 💡 تم تغيير نوع المدخل
        {
            // التحقق الآن على النموذج الداخلي
            if (ModelState.IsValid)
            {
                var application = viewModel.Application; // جلب نموذج الطلب من الـ ViewModel

                // 🛠️ الإصلاح الأمني القديم لتمكين الحفظ بدون تسجيل دخول
                application.ApplicationUserId = "UNAUTHENTICATED_APP_ID";

                application.IsApproved = false;
                application.ApplicationDate = DateTime.Now;

                _context.InstructorApplications.Add(application);
                await _context.SaveChangesAsync();

                TempData["Success"] = "تم استلام طلبك بنجاح. سنقوم بمراجعته قريباً.";

                return RedirectToAction("ApplicationConfirmation");
            }

            // إذا فشل التحقق (Validation)، يجب إعادة جلب قائمة المدربين قبل عرض الصفحة مرة أخرى
            viewModel.ApprovedInstructors = await _context.InstructorApplications
                                                         .Where(a => a.IsApproved == true)
                                                         .Include(a => a.ApplicationUser)
                                                         .ToListAsync();

            // يتم تمرير الـ ViewModel مع قائمة المدربين والأخطاء
            return View(viewModel);
        }

        // دالة (Action) لعرض صفحة النجاح (ApplicationConfirmation.cshtml)
        public IActionResult ApplicationConfirmation()
        {
            return View();
        }

    }
}