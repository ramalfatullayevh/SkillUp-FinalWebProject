using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class ProductController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly IProductService _productService;
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;


        public ProductController(ICategoryService categoryService, IProductService productService, AppDbContext context, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _productService = productService;
            _context = context;
            _env = env;
        }


        //All Product
        public async Task<IActionResult> ManageProducts(string? query, int page = 1)
        {
            if(query!=null)
            {
                var product = await _productService.GetAllProductAsync();
                var search = product.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Product> paginationsearch = search.Skip((page - 1) * 4).Take(4);
                PaginationVM<Product> searchpaginationVM = new PaginationVM<Product>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 4),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query
                };
                return View(searchpaginationVM);
            }
            var products = await _productService.GetAllProductAsync();
            IEnumerable<Product> pagination = products.Skip((page - 1) * 4).Take(4);
            PaginationVM<Product> paginationVM = new PaginationVM<Product>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)products.Count / 4),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);   
        }


        //Delete Product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(ManageProducts));
        }


        //Update Product Get
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));
            var product = await _productService.UpdateProductById(id);
            return View(product);
        }


        //Update Product post
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductVM productVM)
        {
            Product product = new Product();
            if (productVM.Image != null)
            {
                string result = productVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
                //product.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/productimg");
                product.ImageUrl = productVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "productimg"));

            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));
            }
            await _productService.UpdateProductAsync(id, productVM);
            return RedirectToAction(nameof(ManageProducts));
        }

    }
}
