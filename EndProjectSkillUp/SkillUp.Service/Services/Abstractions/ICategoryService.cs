using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAllCategoryAsync();

        Task<Category> GetCategoryById(int id);

        Task CreateCategoryAsync(CreateCategoryVM categoryVM);

        Task DeleteCategoryAsync(int id);

        Task<UpdateCategoryVM> UpdateCategoryById(int id);

        Task<bool> UpdateCategoryAsync(int id , UpdateCategoryVM categoryVM);
    }
}
