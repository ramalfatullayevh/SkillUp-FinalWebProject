using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.InstructorExtraProperities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;

namespace SkillUp.Web.Controllers
{
    public class AccountInstructorController : Controller
    {
        readonly UserManager<Instructor> _userManager;
        readonly SignInManager<Instructor> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;


        public AccountInstructorController(UserManager<Instructor> userManager, SignInManager<Instructor> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _context = context;
        }

        //Instructor SignUp Get
        public IActionResult SignUp()
        {
            ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
            return View();
        }


        //Instructor SignUp Post
        [HttpPost]
        public async Task<IActionResult> SignUp(InstructorRegisterVM registerVM)
        {

            if (registerVM.Image != null)
            {
                string imageresult = registerVM.Image.CheckValidate("image/", 500);
                if (imageresult.Length > 0)
                {
                    ModelState.AddModelError("Image", imageresult);
                }
            }
            if (registerVM.Preview != null)
            {
                string previewresult = registerVM.Preview.CheckValidate("video/", 50000);
                if (previewresult.Length > 0)
                {
                    ModelState.AddModelError("Preview", previewresult);
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Professions = new SelectList(_context.Professions, nameof(Profession.Id), nameof(Profession.Name));
                return View(registerVM);
            }
            Instructor user = await _userManager.FindByNameAsync(registerVM.UserName);
            if (user is not null)
            {
                ModelState.AddModelError("UserName", "UserName already exist");
                return View(registerVM);
            }
            user = new Instructor
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Description = registerVM.Description,
                LinkedInUrl = registerVM.LinkedInUrl,
                FaceBookUrl = registerVM.FacebookUrl,
                InstagramUrl = registerVM.InstagramUrl,
                TwitterUrl = registerVM.TwitterUrl,
                Experince = registerVM.Experience,
                ImageUrl = registerVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "instructorimg")),
                PreviewVideoUrl = registerVM.Preview.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "instructorpreview")),
            };
            var professions = _context.Professions.Where(p => registerVM.ProfessionIds.Contains(p.Id));
            foreach (var item in professions)
            {
                _context.InstructorProfessions.Add(new InstructorProfession { Instructor = user, ProfessionId = item.Id });
            }

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return RedirectToAction(nameof(SignIn));
        }


        //Instructor SignIn Get
        public IActionResult SignIn()
        {
            return View();
        }


        //Instructor SignIn Post
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM login, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(login);
            Instructor user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError("", "Login or Password is wrong");
                return View();
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
    }
}
