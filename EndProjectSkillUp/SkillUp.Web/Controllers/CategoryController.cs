using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;
using SkillUp.Service.Services.Concretes;
using Stripe;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SkillUp.Web.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly ICourseService _courseService;
        readonly AppDbContext _context;
        public CategoryController(ICategoryService categoryService, ICourseService courseService, AppDbContext context)
        {
            _categoryService = categoryService;
            _courseService = courseService;
            _context = context;
        }


        //Get All Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoryAsync(); 
            return View(categories);
        }


        //Course With Category
        public async Task<IActionResult> CourseCategory(string query, int? id, int page = 1)
        {
           var courses = await _context.CourseCategories.Where(c=>c.CategoryId == id).Include(c=>c.Course).ThenInclude(ac=>ac.AppUserCourses).ThenInclude(a=>a.AppUser)
           .Include(ctg=>ctg.Category).Include(c=>c.Course).ThenInclude(p=>p.Paragraphs).ThenInclude(l=>l.Lectures).Include(c=>c.Course).ThenInclude(i=>i.Instructor).ToListAsync();

            if (query != null)
            {
                var search = courses.Where(c => c.Course.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<CourseCategory> paginationsearch = search.Skip((page - 1) * 4).Take(4);
                PaginationVM<CourseCategory> searchpaginationVM = new PaginationVM<CourseCategory>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 2),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);
            }
            else
            {
                IEnumerable<CourseCategory> pagination = courses.Skip((page - 1) * 4).Take(4);
                PaginationVM<CourseCategory> paginationVM = new PaginationVM<CourseCategory>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)courses.Count / 4),
                    CurrentPage = page,
                    Items = pagination
                };
                return View(paginationVM);

            }

        }
    }
}
