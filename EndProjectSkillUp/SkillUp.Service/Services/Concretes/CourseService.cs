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
    public class CourseService : ICourseService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;

        public CourseService(IUnitOfWork unitOfWork, IWebHostEnvironment env, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _context = context;
            
        }

        //Get All Course
        public async Task<ICollection<Course>> GetAllCourseAsync()
        {
             var course = await _context.Courses.Include(i=>i.Instructor).Include(p=>p.Paragraphs).ThenInclude(l=>l.Lectures)
                .Include(cc=>cc.CourseCategories).ThenInclude(ctg=>ctg.Category).Include(u=>u.AppUserCourses)
                .ThenInclude(a=>a.AppUser).Include(r=>r.CourseReviews).ToListAsync();

            return course;
        }


        //Get Course By Id
        public async Task<Course> GetCourseById(int id)
        {
            
            var course =  _unitOfWork.GetRepository<Course>().GetByIdAsync(id);
            return course;
        }


        //Create Course
        public async Task CreateCourseAsync(CreateCourseVM courseVM, string id)
        {
            var categories = _context.Categories.Where(ctg => courseVM.CategoryIds.Contains(ctg.Id));
            Course course = new Course
            {
                Name = courseVM.Name,
                Description = courseVM.Description,
                DiscountPrice = courseVM.DiscountPrice,
                Price = courseVM.Price,
                CourseOverview = courseVM.CourseOverview,
                Requirement = courseVM.Requirement,
                Certification = courseVM.Certification,
                InstructorId = id,
                IsActive = true,
                ImageUrl = courseVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "courseimg")),
                PreviewUrl = courseVM.Preview.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "coursepreview")),
                
            };

            foreach (var item in categories)
            {
                _context.CourseCategories.Add(new CourseCategory { Course = course, CategoryId = item.Id });
            }

            await _unitOfWork.GetRepository<Course>().AddAsync(course);
            await _unitOfWork.SaveAsync();
        }


        //Delete Course
        public async Task DeleteCourseAsync(int id)
        {
            await _unitOfWork.GetRepository<Course>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }


        //Update Course By Id
        public async Task<UpdateCourseVM> UpdateCourseById(int id)
        {
            var course =_context.Courses.Include(cc=>cc.CourseCategories).FirstOrDefault(c=>c.Id==id);
            UpdateCourseVM courseVM = new UpdateCourseVM
            {
                Name = course.Name,
                Description = course.Description,
                CourseOverview = course.CourseOverview,
                Certification = course.Certification,
                Requirement = course.Requirement,
                CategoryIds = new List<int>(),
                Price = course.Price,
                DiscountPrice = course.DiscountPrice,
                ImageUrl = course.ImageUrl,
                PreviewUrl = course.PreviewUrl,
            };
            foreach (var category in course.CourseCategories)
            {
                courseVM.CategoryIds.Add(category.CategoryId);
            }

            return courseVM;
        }


        //Update Course
        public async Task<bool> UpdateCourseAsync(int id, UpdateCourseVM courseVM)
        {
            var course = await _context.Courses.Include(cc => cc.CourseCategories).ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);
            course.Name = courseVM.Name;
            course.Description = courseVM.Description;
            course.Price = courseVM.Price;
            course.DiscountPrice = courseVM.DiscountPrice;
            course.CourseOverview = courseVM.CourseOverview;
            course.Requirement = courseVM.Requirement;
            course.Certification = courseVM.Certification;


            foreach (var category in course.CourseCategories)
            {
                if (courseVM.CategoryIds.Contains(category.CategoryId))
                {
                    courseVM.CategoryIds.Remove(category.CategoryId);
                }
                else
                {
                     _context.CourseCategories.Remove(category);
                }
            }
            foreach (var categoryId in courseVM.CategoryIds)
            {
                _context.CourseCategories.Add(new CourseCategory { Course = course, CategoryId = categoryId });
            }

            await _unitOfWork.GetRepository<Course>().UpdateAsync(course);
            await _unitOfWork.SaveAsync();

            return true;
        }

    }
}
