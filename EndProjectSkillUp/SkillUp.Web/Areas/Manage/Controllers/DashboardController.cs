using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class DashboardController : Controller
    {
        readonly ICourseService _courseService;
        readonly IProductService _productService;
        readonly IInstructorService _instructorService;
        readonly ILectureService _lectureService;   
        readonly IUserService _userService;
        readonly IContactService _contactService;
        readonly ICategoryService _categoryService;


        public DashboardController(ICourseService courseService, IProductService productService, IInstructorService instructorService, ILectureService lectureService, IUserService userService, IContactService contactService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _productService = productService;
            _instructorService = instructorService;
            _lectureService = lectureService;
            _userService = userService;
            _contactService = contactService;
            _categoryService = categoryService;
        }


        //Admin Dashboard
        public async Task<IActionResult> Index()
        {
            DashboardVM dashboardVM = new DashboardVM
            {
                Courses = await _courseService.GetAllCourseAsync(),
                Products = await _productService.GetAllProductAsync(),
                Instructors = await _instructorService.GetAllInstructorAsync(), 
                AppUsers = await _userService.GetAllUserAsync(),
                Lectures = await _lectureService.GetAllLectureAsync(),
                Contacts = await _contactService.GetAllMessageAsync(),
                Categories = await _categoryService.GetAllCategoryAsync(),  
            };

            return View(dashboardVM);
        }
    }
}
