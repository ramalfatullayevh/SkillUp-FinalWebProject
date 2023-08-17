using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

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


        //Enroll Course for Student Get
        public async  Task<IActionResult> EnrollStudent()
        {
            ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
            ViewBag.Courses = new SelectList(await _courseService.GetAllCourseAsync(), nameof(Course.Id), nameof(Course.Name));
            return View();
        }


        //Enroll course for Student Post
        [HttpPost]
        public async Task<IActionResult> EnrollStudent(EnrollStudentVM studentVM)
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
            return RedirectToAction(nameof(EnrollStudent));
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
                ViewBag.Products = new SelectList(await _productService.GetAllProductAsync(), nameof(Product.Id), nameof(Product.Name)); return View();
                return View(productVM);
            }
            if (productVM is null) return NotFound();
            var student = await _context.AppUsers.Include(u => u.AppUserProducts).ThenInclude(c => c.Product).FirstOrDefaultAsync(u => u.Id == productVM.AppUserId);
            foreach (var studentproduct in student.AppUserProducts)
            {
                if (studentproduct.Product.Id == productVM.ProductId)
                {
                    ViewBag.AppUsers = new SelectList(_context.AppUsers, nameof(AppUser.Id), nameof(AppUser.Name));
                    ViewBag.Courses = new SelectList(await _productService.GetAllProductAsync(), nameof(Product.Id), nameof(Product.Name));
                    ModelState.AddModelError("", $"{student.Name} has {studentproduct.Product.Name} product");
                    return View(productVM);
                }

            }
            await _enrollService.EnrollProductAsync(productVM);
            return RedirectToAction(nameof(EnrollProduct));
        }
       
    }
}
