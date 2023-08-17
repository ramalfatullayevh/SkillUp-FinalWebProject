using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.ViewComponents
{
    public class AdminInfoViewComponent:ViewComponent
    {
        readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService; 
        readonly IContactService _contactService;

        public AdminInfoViewComponent(UserManager<AppUser> userManager, IUserService userService, IContactService contactService)
        {
            _userManager = userManager;
            _userService = userService;
            _contactService = contactService;
        }

        //Admin Information ViewComponent
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string id = _userManager.GetUserId(HttpContext.User);
            var user = await _userService.GetUserById(id);
            var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            ViewBag.AdminRole = role;

            AdminDashboardVM dashboardVM = new AdminDashboardVM
            {
                AppUser = user,
                Contacts = await _contactService.GetAllMessageAsync(),
            };
            return View(dashboardVM);
        }
    }
}
