using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;
using Product = SkillUp.Entity.Entities.Product;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class ReportController : Controller
    {
        readonly ICourseService _courseService;
        readonly IProductService _productService;

        public ReportController(ICourseService courseService, IProductService productService)
        {
            _courseService = courseService;
            _productService = productService;
        }


        //Admin Course Revenue
        public async Task<IActionResult> AdminCourseRevenue(string? query, int page = 1)
        {
            if (query!=null)
            {
                var course = await _courseService.GetAllCourseAsync();
                var search = course.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Course> paginationsearch = search.Skip((page - 1) * 5).Take(5);
                PaginationVM<Course> searchpaginationVM = new PaginationVM<Course>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);
            }
            var courses = await _courseService.GetAllCourseAsync();
            IEnumerable<Course> pagination = courses.Skip((page - 1) * 5).Take(5);
            PaginationVM<Course> paginationVM = new PaginationVM<Course>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)courses.Count / 5),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }


        //Admin Product Revenue
        public async Task<IActionResult> AdminProductRevenue(string? query, int page = 1)
        {
            if (query!=null)
            {
                var product = await _productService.GetAllProductAsync();
                var search = product.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Product> paginationsearch = search.Skip((page - 1) * 5).Take(5);
                PaginationVM<Product> searchpaginationVM = new PaginationVM<Product>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);

            }
            var products = await _productService.GetAllProductAsync();
            IEnumerable<Product> pagination = products.Skip((page - 1) * 5).Take(5);
            PaginationVM<Product> paginationVM = new PaginationVM<Product>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)products.Count / 5),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }


        //Instructor Course Revenue
        public async Task<IActionResult> InstructorCourseRevenue(string query, int page = 1)
        {
            if (query != null)
            {
                var course = await _courseService.GetAllCourseAsync();
                var search = course.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Course> paginationsearch = search.Skip((page - 1) * 5).Take(5);
                PaginationVM<Course> searchpaginationVM = new PaginationVM<Course>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);
            }
            var courses = await _courseService.GetAllCourseAsync();
            IEnumerable<Course> pagination = courses.Skip((page - 1) * 5).Take(5);
            PaginationVM<Course> paginationVM = new PaginationVM<Course>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)courses.Count / 5),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }


        //Instructor Product Revenue
        public async Task<IActionResult> InstructorProductRevenue(string query, int page = 1)
        {
            if (query != null)
            {
                var product = await _productService.GetAllProductAsync();
                var search = product.Where(c => c.Name.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Product> paginationsearch = search.Skip((page - 1) * 5).Take(5);
                PaginationVM<Product> searchpaginationVM = new PaginationVM<Product>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 5),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);

            }
            var products = await _productService.GetAllProductAsync();
            IEnumerable<Product> pagination = products.Skip((page - 1) * 5).Take(5);
            PaginationVM<Product> paginationVM = new PaginationVM<Product>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)products.Count / 5),
                CurrentPage = page,
                Items = pagination
            };
            return View(paginationVM);
        }
    }
}
