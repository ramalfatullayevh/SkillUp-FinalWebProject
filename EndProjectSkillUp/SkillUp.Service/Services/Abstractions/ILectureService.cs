using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface ILectureService
    {
        Task<ICollection<Lecture>> GetAllLectureAsync();

        Task<Lecture> GetLectureById(int id);

        Task CreateLectureAsync(CreateLectureVM lectureVM,int id);
        Task DeleteLectureAsync(int id);

        Task<UpdateLectureVM> UpdateLectureById(int id);

        Task<bool> UpdateLectureAsync(int id,UpdateLectureVM lectureVM);
    }
}
