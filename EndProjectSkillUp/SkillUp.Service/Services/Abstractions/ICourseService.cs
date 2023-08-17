using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface ICourseService
    {
        Task<ICollection<Course>> GetAllCourseAsync();

        Task<Course> GetCourseById(int id);

        Task CreateCourseAsync(CreateCourseVM courseVM, string id);
        Task DeleteCourseAsync(int id);

        Task<UpdateCourseVM> UpdateCourseById(int id);

        Task<bool> UpdateCourseAsync(int id, UpdateCourseVM updateCourseVM);
    }
}
