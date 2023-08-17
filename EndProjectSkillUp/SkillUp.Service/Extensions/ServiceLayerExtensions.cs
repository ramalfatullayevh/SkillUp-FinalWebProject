using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Service.Services.Abstractions;
using SkillUp.Service.Services.Concretes;

namespace SkillUp.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            services.AddScoped<ISubCategoryService, SubCategoryService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<IParagraphService, ParagraphService>();

            services.AddScoped<IInstructorService, InstructorService>();

            services.AddScoped<ILectureService, LectureService>();

            services.AddScoped<IEnrollService, EnrollService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IReviewCourseService, ReviewCourseService>();

            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IReviewProductService, ReviewProductService>();

            services.AddScoped<IProfessionService, ProfessionService>();

            services.AddScoped<SignInManager<Instructor>, SignInManager<Instructor>>(); 
            

            return services;
        }


       
    }
}
