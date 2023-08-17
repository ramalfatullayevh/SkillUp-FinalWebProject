using SkillUp.Entity.Entities.Relations.InstructorExtraProperities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IProfessionService
    {
        Task<ICollection<Profession>> GetAllProfessionAsync();

        Task<Profession> GetProfessionById(int id);

        Task CreateProfessionAsync(CreateProfessionVM professionVM);

        Task DeleteProfessionAsync(int id);

        Task<UpdateProfessionVM> UpdateProfessionById(int id);

        Task<bool> UpdateProfessionAsync(int id ,UpdateProfessionVM updateProfessionVM);
    }
}
