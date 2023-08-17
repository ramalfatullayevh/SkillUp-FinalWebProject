using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Service.Services.Abstractions;
using Stripe;

namespace SkillUp.Web.Controllers
{
    public class PaymentController : Controller
    {
        UserManager<AppUser> _userManager;
        AppDbContext _appDbContext;
        readonly IInstructorService _instructorService;
        readonly IUserService _userService;


        public PaymentController(UserManager<AppUser> userManager, AppDbContext appDbContext, IUserService userService)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _userService = userService;
        }

        //Stripe Paymet 
        public async Task<IActionResult> Payment()
        {
           
            return View();
        }


        //Stripe Charge
        [HttpPost]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken, double wallet)
        {
           
            var customers = new CustomerService();
            var chargers = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            
           
            var charge = chargers.Create(new ChargeCreateOptions
            {
                Amount = (long) wallet,
                Description = "Add Balance",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"OrderId","111" },
                    {"PostCode","LEE111" }
                }

              

            });

            if (charge.Status == "succeeded")
            {
              
                string BalanceTransactionId = charge.BalanceTransactionId;
                string id = _userManager.GetUserId(HttpContext.User) ;
                AppUser user = _appDbContext.AppUsers.FirstOrDefault(x => x.Id == id);
                user.Wallet += charge.Amount;
                _appDbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index","Home");
        }


        //Buy Course
        public async Task<IActionResult> BuyCourse(int id)
        {
            var course = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            string userid = _userManager.GetUserId(HttpContext.User);
            AppUser user = await _userService.GetUserById(userid);
            if (course.DiscountPrice==0)
            {
                if (user.Wallet >= course.Price * 100)
                {
                    AppUserCourse userCourse = new AppUserCourse
                    {
                        AppUserId = user.Id,
                        CourseId = id,
                        IsSold = true,
                    };

                    user.Wallet = user.Wallet - course.Price * 100;

                    Instructor instructor = await _appDbContext.Instructors.Include(i => i.Courses).FirstOrDefaultAsync(i => i.Id == course.InstructorId);

                    instructor.Wallet = instructor.Wallet + (course.Price * 100) * 0.75;

                    await _appDbContext.AddAsync(userCourse);
                }
                else
                {
                    return RedirectToAction(nameof(Payment));
                }
            }
            else
            {
                    
                if (user.Wallet >= course.DiscountPrice * 100)
                {
                    AppUserCourse userCourse = new AppUserCourse
                    {
                        AppUserId = user.Id,
                        CourseId = id,
                        IsSold = true,
                    };

                    user.Wallet = user.Wallet - course.DiscountPrice * 100;

                    Instructor instructor = await _appDbContext.Instructors.Include(i => i.Courses).FirstOrDefaultAsync(i => i.Id == course.InstructorId);

                    instructor.Wallet = instructor.Wallet + (course.DiscountPrice * 100) * 0.75;

                    await _appDbContext.AddAsync(userCourse);
                }
                else
                {
                    return RedirectToAction(nameof(Payment));
                }
            }
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index" ,"Library");

        }


        //Buy Product
        public async Task<IActionResult> BuyProduct(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            string userid = _userManager.GetUserId(HttpContext.User);
            AppUser user = _appDbContext.AppUsers.FirstOrDefault(x => x.Id == userid);
            if (product.DiscountPrice == 0)
            {
                if (user.Wallet >= product.Price*100)
                {
                    AppUserProduct userProduct = new AppUserProduct
                    {
                        AppUserId = user.Id,
                        ProductId = id,
                        IsBuyed = true,
                    };

                    user.Wallet = user.Wallet - product.Price * 100;
                    Instructor instructor = await _appDbContext.Instructors.Include(i => i.Courses).FirstOrDefaultAsync(i => i.Id == product.InstructorId);
                    instructor.Wallet = instructor.Wallet + (product.DiscountPrice * 100) * 0.75;

                    await _appDbContext.AddAsync(userProduct);
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Payment));
                }
                
            }
            else
            {
                if (user.Wallet >= product.DiscountPrice *100)
                {
                    AppUserProduct userProduct = new AppUserProduct
                    {
                        AppUserId = user.Id,
                        ProductId = id,
                        IsBuyed = true,
                    };

                    user.Wallet = user.Wallet - product.DiscountPrice * 100;
                    Instructor instructor = await _appDbContext.Instructors.Include(i => i.Courses).FirstOrDefaultAsync(i => i.Id == product.InstructorId);
                    instructor.Wallet = instructor.Wallet + (product.DiscountPrice * 100) * 0.75;
                    await _appDbContext.AddAsync(userProduct);
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Payment));

                }
            }

            return RedirectToAction("Product", "Library");

        }
    }
}
