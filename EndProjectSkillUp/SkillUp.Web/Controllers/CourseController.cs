using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Controllers
{
    public class CourseController : Controller
    {
        readonly AppDbContext _appDbContext;
        readonly UserManager<AppUser> _userManager;
        readonly ICourseService _courseService;
        readonly IReviewCourseService _reviewcourseService;

        public CourseController(AppDbContext appDbContext, UserManager<AppUser> userManager, IReviewCourseService reviewcourseService, ICourseService courseService)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _reviewcourseService = reviewcourseService;
            _courseService = courseService;
        }

        //Find Courses
        public async Task<IActionResult> FindCourses(string? query ,int page=1)
        {
            if (query!=null)
            {
                var course = await _courseService.GetAllCourseAsync();
                var search = course.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Course> paginationsearch = search.Skip((page - 1) * 2).Take(2);
                PaginationVM<Course> searchpaginationVM = new PaginationVM<Course>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 2),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query   
                    
                };
                return View(searchpaginationVM);
            }
            var courses = await _courseService.GetAllCourseAsync();
            IEnumerable<Course> pagination = courses.Skip((page-1) * 4).Take(4);
            PaginationVM<Course> paginationVM = new PaginationVM<Course>
            {
                MaxPageCount = (int)Math.Ceiling((decimal) courses.Count / 4),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }

        //CourseDetail Page
        public async Task<IActionResult> CourseDetail(int id)
        {
            var coursedetail =  _appDbContext.Courses.Include(p=>p.Paragraphs).ThenInclude(l=>l.Lectures)
            .Include(cc=>cc.CourseCategories).ThenInclude(ctg=>ctg.Category)
            .Include(a=>a.AppUserCourses).ThenInclude(u=>u.AppUser)
            .Include(i=>i.Instructor).Include(c=>c.CourseReviews).ThenInclude(u=>u.AppUser).FirstOrDefault(x=>x.Id == id);
             coursedetail.ViewCount ++ ;
             _appDbContext.SaveChanges();
             return View(coursedetail);
        }


        //submit Review Post
        [HttpPost]
        public async Task<IActionResult> SubmitReview(CreateCourseReviewVM review)
        {
            string userid =  _userManager.GetUserId(HttpContext.User);
            if (!ModelState.IsValid) return View(review);
             await  _reviewcourseService.CreateReviewAsync(review, userid);
            return RedirectToAction("Index", "Home");
        }

    }
}
