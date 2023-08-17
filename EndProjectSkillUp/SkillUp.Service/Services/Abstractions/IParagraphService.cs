using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IParagraphService
	{
        Task<ICollection<Paragraph>> GetAllParagraphAsync();

        Task<Paragraph> GetParagraphById(int id);

        Task CreateParagraphAsync(CreateParagraphVM paragraphVM, int id);

        Task DeleteParagraphAsync(int id);

        Task<UpdateParagraphVM> UpdateParagraphById(int id);

        Task<bool> UpdateParagraphAsync(int id, UpdateParagraphVM paragraphVM);
    }
}
