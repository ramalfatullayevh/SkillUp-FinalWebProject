using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IUserService userService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        //Manage Admins
        public async Task<IActionResult> ManageAdmin(string? query, int page = 1)
        {
            if (query!=null)
            {
                var admin = await _userManager.GetUsersInRoleAsync("Admin");
                var search = admin.Where(c => c.UserName.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<AppUser> paginationsearch = search.Skip((page - 1) * 4).Take(4);
                PaginationVM<AppUser> searchpaginationVM = new PaginationVM<AppUser>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 4),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);
            }
            else
            {
                var admins = await _userManager.GetUsersInRoleAsync("Admin");
                IEnumerable<AppUser> pagination = admins.Skip((page - 1) * 4).Take(4);
                PaginationVM<AppUser> paginationVM = new PaginationVM<AppUser>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)admins.Count / 4),
                    CurrentPage = page,
                    Items = pagination
                };

                return View(paginationVM);
            }
        }


        //Upgrade Role - SuperAdmin
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> UpgradeRole(string id)
        {
            var user = await _userService.GetUserById(id);
            
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            }

            return RedirectToAction("manageadmin", "admin");
        }


        //DownGrade Role - Student
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DowngradeRole(string id)
        {
            var user = await _userService.GetUserById(id);
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "Student");
            }

            return RedirectToAction("manageadmin", "admin");
        }


    }
}
