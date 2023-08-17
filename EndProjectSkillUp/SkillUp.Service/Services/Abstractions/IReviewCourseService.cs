using SkillUp.Entity.Entities.Reviews;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IReviewCourseService
    {
        Task<ICollection<CourseReview>> GetAllCourseReviewAsync();

        Task CreateReviewAsync(CreateCourseReviewVM reviewVM, string id);

        Task DeleteReviewAsync(int id);

        Task<bool> ReadReviewAsync(int id);


    }
}
