using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.ViewComponents
{
    public class WalletViewComponent:ViewComponent
    {
        readonly UserManager<AppUser> _userManager;
        readonly UserManager<Instructor> _instructorManager;
        readonly IUserService _userService; 
        readonly IInstructorService _instructorService;

        public WalletViewComponent(UserManager<AppUser> usermanageer, UserManager<Instructor> instructorManager, IUserService userService, IInstructorService instructorService)
        {
            _userManager = usermanageer;
            _instructorManager = instructorManager;
            _userService = userService;
            _instructorService = instructorService;
        }

        //Wallet
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? userid = _userManager.GetUserId(HttpContext.User);
            string? instructorid = _instructorManager.GetUserId(HttpContext.User);
            InfoVM info = new InfoVM
            {
                AppUser = await _userService.GetUserById(userid),
                Instructor = await _instructorService.GetInstructorById(instructorid)
            };
            return View(info);
        }
    }
}
