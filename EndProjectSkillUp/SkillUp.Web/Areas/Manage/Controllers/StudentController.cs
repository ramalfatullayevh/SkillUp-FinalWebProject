using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class StudentController : Controller
    {
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager; 
        readonly IWebHostEnvironment _env;


        public StudentController(IUserService userService, UserManager<AppUser> userManager, IWebHostEnvironment env, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _env = env;
            _signInManager = signInManager;
        }

        //All Student
        public async Task<IActionResult> ManageStudents(string? query , int page = 1)
        {
            if (query!=null)
            {
                var student = await _userService.GetAllUserAsync();
                var search = student.Where(c => c.UserName.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
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
                var students = await _userService.GetAllUserAsync();
                IEnumerable<AppUser> pagination = students.Skip((page - 1) * 4).Take(4);
                PaginationVM<AppUser> paginationVM = new PaginationVM<AppUser>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)students.Count / 4),
                    CurrentPage = page,
                    Items = pagination
                };

                return View(paginationVM);
            }
        }


        //Delete Student
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(ManageStudents));
        }


        //Update Student Get
        public async Task<IActionResult> UpdateStudent(string id)
        {
            var user = await _userService.UpdateUserById(id);
            return View(user);
        }


        //Update Student Post
        [HttpPost]
        public async Task<IActionResult> UpdateStudent(string id, UpdateUserVM userVM)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (userVM.Image != null)
            {
                string imgresult = userVM.Image.CheckValidate("image/", 500);
                if (imgresult.Length > 0)
                {
                    ModelState.AddModelError("Image", imgresult);
                }

                user.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/userimg");
                user.ImageUrl = userVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "userimg"));

            }
            if (user == null)
            {
                ModelState.AddModelError("", "Login or Password is wrong");
            }
            if (userVM.Password != null && userVM.CurrentPassword != null && userVM.ConfirmPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, userVM.CurrentPassword, userVM.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "PasswordMismatch")
                        {
                            ModelState.AddModelError(string.Empty, "The current password is incorrect.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                
                }

            }
            await _userService.UpdateUserAsync(id, userVM);
            return RedirectToAction(nameof(ManageStudents));
        }

        //Upgrade Role - Admin
        public async Task<IActionResult> UpgradeRole(string id)
        {
            var user = await _userService.GetUserById(id);
            var result = await _userManager.RemoveFromRoleAsync(user, "Student");
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction("manageadmin", "admin");
        }
    }
}
