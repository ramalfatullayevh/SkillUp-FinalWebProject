using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class CourseController : Controller
    {
        readonly ICourseService _courseService;
        readonly ICategoryService _categoryService;
        readonly IInstructorService _instructorService;
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;

        public CourseController(ICourseService courseService, ICategoryService categoryService, IInstructorService instructorService, IWebHostEnvironment env, AppDbContext context)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
            _env = env;
            _context = context;
        }


        //All Courses
        public async Task<IActionResult> ManageCourses(string? query ,int page = 1)
        {
            if (query!=null)
            {
                var course = await _courseService.GetAllCourseAsync();
                var search = course.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Course> paginationsearch = search.Skip((page - 1) * 4).Take(4);
                PaginationVM<Course> searchpaginationVM = new PaginationVM<Course>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 4),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);

            }
            var courses = await _courseService.GetAllCourseAsync();
            IEnumerable<Course> pagination = courses.Skip((page - 1) * 4).Take(4);
            PaginationVM<Course> paginationVM = new PaginationVM<Course>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)courses.Count / 4),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }


        //Delete Course
        public async  Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(ManageCourses));
        }


        //Update Course Get
        public async Task<IActionResult> UpdateCourse(int id)
        {

            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.Instructor = new SelectList(await _instructorService.GetAllInstructorAsync(), nameof(Instructor.Id), nameof(Instructor.Name));
            var course = await _courseService.UpdateCourseById(id);
            return View(course);
        }


        //Update Course Post
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(int id,UpdateCourseVM courseVM )
        {
            Course course = await _courseService.GetCourseById(id);

            if (courseVM.Image != null)
            {
                string result = courseVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }

                course.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/courseimg");
                course.ImageUrl = courseVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "courseimg"));

            }
            if (courseVM.Preview != null)
            {
                string result = courseVM.Preview.CheckValidate("video/", 50000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Preview", result);
                }

                //course.PreviewUrl.DeleteFile(_env.WebRootPath, "user/assets/coursepreview");
                course.PreviewUrl = courseVM.Preview.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "coursepreview"));
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                ViewBag.Instructor = new SelectList(await _instructorService.GetAllInstructorAsync(), nameof(Instructor.Id), nameof(Instructor.Name));
                return View(courseVM);
            }

            await _courseService.UpdateCourseAsync(id,courseVM);
            return RedirectToAction(nameof(ManageCourses));
        }
    }
}
