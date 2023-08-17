using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;

namespace SkillUp.Web.Areas.InstructorPanel.ViewComponents
{
    public class InstructorInfoViewComponent:ViewComponent
    {
        readonly UserManager<Instructor> _userManager;
        readonly AppDbContext _context;

        public InstructorInfoViewComponent(UserManager<Instructor> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        //Instructor Info ViewComponent
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string id = _userManager.GetUserId(HttpContext.User);
            Instructor instructor = await _context.Instructors.Include(c => c.Courses).ThenInclude(cr=>cr.CourseReviews.OrderByDescending(p => p.ReviewDate)).ThenInclude(u=>u.AppUser)
                .Include(iu=>iu.AppUserInstructors).ThenInclude(u=>u.AppUser).Include(p=>p.Products).Include(ip=>ip.InstructorProfessions).ThenInclude(p=>p.Profession).FirstOrDefaultAsync(i => i.Id == id);
            return View(instructor);
        }
    }
}
