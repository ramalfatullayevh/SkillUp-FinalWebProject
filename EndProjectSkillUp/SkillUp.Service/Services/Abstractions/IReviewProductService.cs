using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Reviews;
using SkillUp.Entity.ViewModels;

namespace SkillUp.Service.Services.Abstractions
{
    public interface IReviewProductService
    {
        Task<ICollection<ProductReview>> GetAllReviewAsync();

        Task CreateReviewAsync(CreateProductReviewVM reviewVM, string id);


    }
}
