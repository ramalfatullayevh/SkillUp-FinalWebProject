using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class EnrollService : IEnrollService
    {
        readonly IUnitOfWork _unitOfWork;

        public EnrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Enroll Course for Student
        public async Task EnrollStudentAsync(EnrollStudentVM studentVM)
        {
            AppUserCourse userCourse = new AppUserCourse
            {
                AppUserId = studentVM.AppUserId,
                CourseId = studentVM.CourseId,
                IsSold = false
            };
            await _unitOfWork.GetRepository<AppUserCourse>().AddAsync(userCourse);
            await _unitOfWork.SaveAsync();
        }

       
        //Enroll Product for Student
        public async Task EnrollProductAsync(EnrollProductVM productVM)
        {
            AppUserProduct userProduct = new AppUserProduct
            {
                AppUserId = productVM.AppUserId,
                ProductId = productVM.ProductId,
                IsBuyed = false
                
            };
            await _unitOfWork.GetRepository<AppUserProduct>().AddAsync(userProduct);
            await _unitOfWork.SaveAsync();
        }

    }
}
