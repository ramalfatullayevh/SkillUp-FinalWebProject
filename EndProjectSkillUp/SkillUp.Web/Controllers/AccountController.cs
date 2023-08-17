using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;

namespace SkillUp.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IWebHostEnvironment _env;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }

        //AppUser SignUp Get
        public IActionResult SignUp()
        {
            return View();
        }


        //AppUser SignUp Post
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);  
            AppUser user = await _userManager.FindByNameAsync(register.UserName);

            if (register.Image != null)
            {
                string imgresult = register.Image.CheckValidate("image/", 500);
                if (imgresult.Length > 0)
                {
                    ModelState.AddModelError("Image", imgresult);
                }

                user.ImageUrl = register.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "userimg"));
            }
            if (user is not null)
            {
                ModelState.AddModelError("UserName", "UserName already exist");
                return View(register);
            }
            user = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.UserName,
                Email = register.Email,

        };

            var result = await _userManager.CreateAsync(user,register.Password);
          
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            var role = await _userManager.AddToRoleAsync(user, "Student");

            return RedirectToAction(nameof(SignIn));
        }


        //AppUser SigIn Get
        public IActionResult SignIn()
        {
            return View();
        }


        //AppUser Signin Post
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM login, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Login or Password Wrong!");
                    return View();
                }
            }
          
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login or Password is wrong");
                return View();
            }
            if (returnUrl is not null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //SignOut
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();    
            return RedirectToAction("Index", "Home");
        }


        //Roles
        //public async Task AddRoles()
        //{
        //    foreach (var item in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(item.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });

        //        }
        //    }

        //}
    }
}
