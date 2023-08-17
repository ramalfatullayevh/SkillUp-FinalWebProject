using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Reviews;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class ReviewCourseService : IReviewCourseService
    {
        readonly IUnitOfWork _unitOfWork;

        public ReviewCourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Get All Review
        public Task<ICollection<CourseReview>> GetAllCourseReviewAsync()
        {
           var reviews = _unitOfWork.GetRepository<CourseReview>().GetAllAsync(null,u=>u.AppUser);   
           return reviews;
        }


        //Create Review
        public async Task CreateReviewAsync(CreateCourseReviewVM reviewVM, string id)
        {
           CourseReview courseReview = new CourseReview
           {
               AppUserId = id,
               CourseId = reviewVM.CourseId,
               ReviewContent = reviewVM.ReviewContent,  
               ReviewDate = DateTime.Now,
               Status = false,
           };  
            await _unitOfWork.GetRepository<CourseReview>().AddAsync(courseReview);
            await _unitOfWork.SaveAsync();
        }


        //Delete Review
        public async Task DeleteReviewAsync(int id)
        {
            await _unitOfWork.GetRepository<CourseReview>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();  
        }


        //Read Review
        public async Task<bool> ReadReviewAsync(int id)
        {
            var review = _unitOfWork.GetRepository<CourseReview>().GetByIdAsync(id);
            review.Status = true;
            await _unitOfWork.GetRepository<CourseReview>().UpdateAsync(review);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
