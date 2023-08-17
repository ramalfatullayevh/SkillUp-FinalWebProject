using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Service.Services.Abstractions;
using SkillUp.Service.Services.Concretes;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class MessageController : Controller
    {
        readonly UserManager<Instructor> _userManager;
        readonly AppDbContext _context;
        readonly IReviewCourseService _reviewCourse;

        public MessageController(UserManager<Instructor> userManageer, AppDbContext context, IReviewCourseService reviewCourse)
        {
            _userManager = userManageer;
            _context = context;
            _reviewCourse = reviewCourse;
        }


        //All Messages
        public async Task<IActionResult> Index()
        {
            string id = _userManager.GetUserId(HttpContext.User);
            Instructor instructor = await _context.Instructors.Include(c => c.Courses).ThenInclude(cr => cr.CourseReviews.OrderByDescending(p => p.ReviewDate)).ThenInclude(u => u.AppUser).Include(iu => iu.AppUserInstructors).ThenInclude(u => u.AppUser).FirstOrDefaultAsync(i => i.Id == id);
            return View(instructor);
        }


        //Delete Message
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var review = _reviewCourse.DeleteReviewAsync(id);
            return RedirectToAction(nameof(Index));
        }


        //Read Message
        public async Task<IActionResult> ReadMessage(int id)
        {
            await _reviewCourse.ReadReviewAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
