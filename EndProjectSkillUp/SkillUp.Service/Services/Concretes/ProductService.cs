using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class ProductService : IProductService
    {
        readonly IUnitOfWork _unitOfWork;   
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;

        public ProductService(IWebHostEnvironment env, IUnitOfWork unitOfWork, AppDbContext context)
        {
            _env = env;
            _unitOfWork = unitOfWork;
            _context = context;
        }


        //Get All Product
        public async Task<ICollection<Product>> GetAllProductAsync()
        {
            var product = await _context.Products.Include(pc=>pc.ProductCategories).ThenInclude(c=>c.Category)
               .Include(ap=>ap.AppUserProducts).ThenInclude(a=>a.AppUser).Include(p=>p.Instructor).ToListAsync();
            return product;
        }


        //Get Product By Id
        public async Task<Product> GetProductById(int id)
        {
            var product =  _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            return product;
        }


        //Create Product
        public async Task CreateProductAsync(CreateProductVM productVM, string instructorid)
        {
            var categories = _context.Categories.Where(ctg => productVM.CategoryIds.Contains(ctg.Id));
            Product product = new Product 
            { 
                Price = productVM.Price,
                DiscountPrice = productVM.DiscountPrice,
                SKU = productVM.SKU,
                Name = productVM.Name,
                InstructorId = instructorid,
                Description = productVM.Description,
                CreateDate = DateTime.Now,
                ImageUrl = productVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "productimg")),
            };
            foreach (var item in categories)
            {
                _context.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = item.Id });
            }

            await _unitOfWork.GetRepository<Product>().AddAsync(product);
            await _unitOfWork.SaveAsync();
        }


        //Delete Product
        public async  Task DeleteProductAsync(int id)
        {
            await _unitOfWork.GetRepository<Product>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }


        //Update Product By Id
        public async Task<UpdateProductVM> UpdateProductById(int id)
        {
            var product = _context.Products.Include(cc => cc.ProductCategories).FirstOrDefault(c => c.Id == id);
            UpdateProductVM productVM = new UpdateProductVM
            {
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,  
                CategoryIds = new List<int>(),
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                ImageUrl = product.ImageUrl,
            };
            foreach (var category in product.ProductCategories)
            {
                productVM.CategoryIds.Add(category.CategoryId);
            }
           

            return productVM;

        }


        //Update Product
        public async  Task<bool> UpdateProductAsync(int id ,UpdateProductVM productVM)
        {
            var product = await _context.Products.Include(cc => cc.ProductCategories).ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            product.Name = productVM.Name;
            product.Description = productVM.Description;
            product.Price = productVM.Price;
            product.DiscountPrice = productVM.DiscountPrice;
            product.SKU = productVM.SKU;
            
            foreach (var category in product.ProductCategories)
            {
                if (productVM.CategoryIds.Contains(category.CategoryId))
                {
                    productVM.CategoryIds.Remove(category.CategoryId);
                }
                else
                {
                    _context.ProductCategories.Remove(category);
                }
            }
            

            foreach (var categoryId in productVM.CategoryIds)
            {
                _context.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }

            await _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            await _unitOfWork.SaveAsync();

            return true;
        }

    }
}
