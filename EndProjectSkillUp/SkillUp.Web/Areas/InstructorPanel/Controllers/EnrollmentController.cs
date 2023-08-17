using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class EnrollmentController : Controller
    {
        readonly IEnrollService _enrollService;
        readonly ICourseService _courseService;
        readonly IProductService _productService;
        readonly AppDbContext _context;


        public EnrollmentController(IEnrollService enrollService, AppDbContext context, ICourseService courseService, IProductService productService)
        {
            _enrollService = enrollService;
            _context = context;
            _courseService = courseService;
            _productService = productService;
        }



        //Enroll Course Get
        public async Task<IActionResult> EnrollCourse()
        {
            ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
            ViewBag.Courses = new SelectList(await _courseService.GetAllCourseAsync(), nameof(Course.Id), nameof(Course.Name));
            return View();
        }


        //Enroll Course Post
        [HttpPost]
        public async Task<IActionResult> EnrollCourse(EnrollStudentVM studentVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
                ViewBag.Courses = new SelectList(await _courseService.GetAllCourseAsync(), nameof(Course.Id), nameof(Course.Name));
                return View();
            }
            if (studentVM is null) return NotFound();
            var student = await _context.AppUsers.Include(u => u.AppUserCourses).ThenInclude(c => c.Course).FirstOrDefaultAsync(u => u.Id == studentVM.AppUserId);
            foreach (var studentcourse in student.AppUserCourses)
            {
                if (studentcourse.Course.Id == studentVM.CourseId)
                {
                    ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
                    ViewBag.Courses = new SelectList(await _courseService.GetAllCourseAsync(), nameof(Course.Id), nameof(Course.Name));
                    ModelState.AddModelError("", $"{student.Name} has {studentcourse.Course.Name} course");
                    return View(studentVM);
                }

            }
            await _enrollService.EnrollStudentAsync(studentVM);
            return RedirectToAction(nameof(EnrollCourse));
        }


        //Enroll Product Get
        public async Task<IActionResult> EnrollProduct()
        {
            ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
            ViewBag.Products = new SelectList(await _productService.GetAllProductAsync(), nameof(Product.Id), nameof(Product.Name));
            return View();
        }


        //Enroll Product Post
        [HttpPost]
        public async Task<IActionResult> EnrollProduct(EnrollProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
                ViewBag.Products = new SelectList(await _productService.GetAllProductAsync(), nameof(Product.Id), nameof(Product.Name)); 
                return View(productVM);
            }
            if (productVM is null) return NotFound();
            var student = await _context.AppUsers.Include(u => u.AppUserProducts).ThenInclude(c => c.Product).FirstOrDefaultAsync(u => u.Id == productVM.AppUserId);
            foreach (var studentproduct in student.AppUserProducts)
            {
                if (studentproduct.Product.Id == productVM.ProductId)
                {
                    ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
                    ViewBag.Products = new SelectList(await _productService.GetAllProductAsync(), nameof(Course.Id), nameof(Course.Name));
                    ModelState.AddModelError("", $"{student.Name} has {studentproduct.Product.Name} product");
                    return View(productVM);
                }

            }
            await _enrollService.EnrollProductAsync(productVM);
            return RedirectToAction(nameof(EnrollProduct));
        }
    }
}
