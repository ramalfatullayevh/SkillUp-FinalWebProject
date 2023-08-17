using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IInstructorService
    {
        Task<ICollection<Instructor>> GetAllInstructorAsync();

        Task<Instructor> GetInstructorById(string id);

        Task DeleteInstructorAsync(string id);
        
        Task<UpdateInstructorVM> UpdateInstructorById(string id);
        
        Task<bool> UpdateInstructorAsync(string id, UpdateInstructorVM updateInstructorVM);

    }
}
