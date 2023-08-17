using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IEnrollService
    {
        Task EnrollStudentAsync(EnrollStudentVM studentVM);

        Task EnrollProductAsync(EnrollProductVM productVM);
    }
}
