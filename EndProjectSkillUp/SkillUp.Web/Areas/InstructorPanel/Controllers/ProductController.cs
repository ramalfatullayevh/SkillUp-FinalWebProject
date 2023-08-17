using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;
using SkillUp.Service.Helpers;
using Microsoft.AspNetCore.Identity;
using SkillUp.Service.Services.Concretes;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class ProductController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly IProductService _productService;
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        readonly UserManager<Instructor> _userManager;
        readonly IInstructorService _instructorService;

        public ProductController(ICategoryService categoryService, IProductService productService, AppDbContext context, IWebHostEnvironment env, UserManager<Instructor> userManager, IInstructorService instructorService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _context = context;
            _env = env;
            _userManager = userManager;
            _instructorService = instructorService;
        }


        //My Products
        public async Task<IActionResult> MyProducts()
        {
            string id = _userManager.GetUserId(HttpContext.User);
            var instructor = await _instructorService.GetInstructorById(id);
            return View(instructor);
        }


        //Add New Product Get
        public async Task<IActionResult> AddNewProduct()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));

            return View();
        }


        //Add New Product Post
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(CreateProductVM productVM)
        {
            string id = _userManager.GetUserId(HttpContext.User);

            if (productVM.Image != null)
            {
                string result = productVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));
                return View(productVM);
            }
            await _productService.CreateProductAsync(productVM,id);
            return RedirectToAction(nameof(MyProducts));

        }


        //Delete Product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(MyProducts));
        }


        //Update Product Get
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));
            var product = await _productService.UpdateProductById(id);
            return View(product);
        }


        //Update Product Post
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductVM productVM)
        {
            Product product = await _productService.GetProductById(id);
            if (productVM.Image != null)
            {
                string result = productVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
                product.ImageUrl.DeleteFile(_env.WebRootPath, "user/assets/productimg");
                product.ImageUrl = productVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "productimg"));

            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), nameof(Category.Id), nameof(Category.Name));
                ViewBag.Instructors = new SelectList(_context.Instructors, nameof(Instructor.Id), nameof(Instructor.Name));
            }
            await _productService.UpdateProductAsync(id, productVM);
            return RedirectToAction(nameof(MyProducts));
        }
    }
}
