using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Reviews;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class ReviewProductService : IReviewProductService
    {
        readonly IUnitOfWork _unitOfWork;

        public ReviewProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Get All Review
        public async Task<ICollection<ProductReview>> GetAllReviewAsync()
        {
            var review = await _unitOfWork.GetRepository<ProductReview>().GetAllAsync();
            return review;
        }


        //Create Review
        public async Task CreateReviewAsync(CreateProductReviewVM reviewVM, string id)
        {
            ProductReview productReview = new ProductReview
            {
                AppUserId = id,
                ProductId = reviewVM.ProductId,
                ReviewContent = reviewVM.ReviewContent,
                Status = true,
            };
            await _unitOfWork.GetRepository<ProductReview>().AddAsync(productReview);
            await _unitOfWork.SaveAsync();
        }

    }
}
