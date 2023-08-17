using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class DashboardController : Controller
    {
        readonly ICourseService _courseService;
        readonly IProductService _productService;
        readonly IInstructorService _instructorService;
        readonly AppDbContext _appDbContext; //
        readonly UserManager<Instructor> _userManager;

        public DashboardController(ICourseService courseService, IProductService productService, AppDbContext appDbContext, IInstructorService instructorService, UserManager<Instructor> userManager)
        {
            _courseService = courseService;
            _productService = productService;
            _appDbContext = appDbContext;
            _instructorService = instructorService;
            _userManager = userManager;
        }


        //Dashboard
        public async Task<IActionResult> Index()
        {
            string id = _userManager.GetUserId(HttpContext.User);

            InstructorDashboardVM dashboardVM = new InstructorDashboardVM
            {
                Instructor = await  _instructorService.GetInstructorById(id),
            };
        
            return View(dashboardVM);
        }
    }
}
