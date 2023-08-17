using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface ISubCategoryService
    {
        Task<ICollection<SubCategory>> GetAllSubCategoryAsync();

        Task CreateSubCategoryAsync(CreateSubCategoryVM subCategoryVM);

    }
}
