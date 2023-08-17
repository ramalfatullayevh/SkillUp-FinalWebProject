using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class CourseController : Controller
    {
        readonly ICourseService _courseService;
        readonly ICategoryService _categoryService;
        readonly IInstructorService _instructorService;
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;
        readonly UserManager<Instructor> _userManager;



        public CourseController(ICourseService courseService, ICategoryService categoryService, IInstructorService instructorService, IWebHostEnvironment env, AppDbContext context, UserManager<Instructor> userManager)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
            _env = env;
            _context = context;
            _userManager = userManager;
        }


        //My Courses
        public async Task<IActionResult> MyCourses()
        {
            string id = _userManager.GetUserId(HttpContext.User);
            var instructor  = await _instructorService.GetInstructorById(id);
            return View(instructor);
        }


        //Add New Course Get
        public async Task<IActionResult> AddNewCourse()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));

            return View();
        }


        //Add New Course Post
        [HttpPost]
        public async Task<IActionResult> AddNewCourse(CreateCourseVM courseVM)
        {
             string id = _userManager.GetUserId(HttpContext.User);

            if (courseVM.Image != null)
            {
                string result = courseVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
            }
            if (courseVM.Preview != null)
            {
                string result = courseVM.Preview.CheckValidate("video/", 50000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Preview", result);
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                return View(courseVM);
            }
            await _courseService.CreateCourseAsync(courseVM, id);
            return RedirectToAction(nameof(MyCourses));
        }


        //Delete Course
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(MyCourses));
        }


        //Update Course Get
        public async Task<IActionResult> UpdateCourse(int id)
        {

            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
            var course = await _courseService.UpdateCourseById(id);
            return View(course);
        }


        //Update Course Post
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseVM courseVM)
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

                course.PreviewUrl.DeleteFile(_env.WebRootPath, "user/assets/coursepreview");
                course.PreviewUrl = courseVM.Preview.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "coursepreview"));
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                return View(courseVM);
            }

            await _courseService.UpdateCourseAsync(id, courseVM);
            return RedirectToAction(nameof(MyCourses));
        }
    }
}
