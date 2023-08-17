using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;
using Product = SkillUp.Entity.Entities.Product;

namespace SkillUp.Web.Controllers
{
    public class ShopController : Controller
    {
        readonly IReviewProductService _reviewProductService;
        readonly IProductService _productService;
        readonly AppDbContext appDbContext;
        readonly UserManager<AppUser> _userManager;



        public ShopController(AppDbContext appDbContext, UserManager<AppUser> userManager, IReviewProductService reviewProductService, IProductService productService)
        {
            this.appDbContext = appDbContext;
            _userManager = userManager;
            _reviewProductService = reviewProductService;
            _productService = productService;
        }

        //Find Product
        public async Task<IActionResult> Products(string? query , int page = 1)
        {
            if (query!=null)
            {
                var products = await _productService.GetAllProductAsync();
                var search = products.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Product> paginationsearch = search.Skip((page - 1) * 3).Take(3);
                PaginationVM<Product> searchpaginationVM = new PaginationVM<Product>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 3),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);
            }
            else
            {
                var products = await _productService.GetAllProductAsync();
                IEnumerable<Product> pagination = products.Skip((page - 1) * 3).Take(3);
                PaginationVM<Product> paginationVM = new PaginationVM<Product>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal) products.Count / 3),
                    CurrentPage = page,
                    Items = pagination
                };
                return View(paginationVM);

            }
        }


        //Product Detail
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await appDbContext.Products.Include(pc=>pc.ProductCategories).ThenInclude(c=>c.Category).Include(up=>up.AppUserProducts).
              ThenInclude(u=>u.AppUser).Include(product=>product.ProductReviews).ThenInclude(u=>u.Appuser).Include(i=>i.Instructor).FirstOrDefaultAsync(p=>p.Id == id); 
            return View(product);
        }


        //Submit Review Product
        [HttpPost]
        public async Task<IActionResult> SubmitReview(CreateProductReviewVM reviewVM)
        {
            string userid = _userManager.GetUserId(HttpContext.User);
            if (!ModelState.IsValid) return View(reviewVM);
            AppUserProduct userProduct = new AppUserProduct();
            await _reviewProductService.CreateReviewAsync(reviewVM, userid);
            return RedirectToAction("Index", "Home");
        }
        
    }
}
