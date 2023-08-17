using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IUserService
    {
        Task<ICollection<AppUser>> GetAllUserAsync();

        Task<AppUser> GetUserById(string id);

        Task DeleteUserAsync(string id);

        Task<UpdateUserVM> UpdateUserById(string id);

        Task<bool> UpdateUserAsync(string id, UpdateUserVM updateCourseVM);
    }
}
