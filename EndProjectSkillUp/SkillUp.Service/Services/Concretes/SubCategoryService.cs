using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class SubCategoryService:ISubCategoryService
    {
        readonly IUnitOfWork _unitOfWork;

        public SubCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Get All Sub Category
        public async Task<ICollection<SubCategory>> GetAllSubCategoryAsync()
        {
            var subcategories = await _unitOfWork.GetRepository<SubCategory>().GetAllAsync(null,s=>s.Category);
            return subcategories;   
        }


        //Create Sub Category
        public async Task CreateSubCategoryAsync(CreateSubCategoryVM subCategoryVM)
        {
            SubCategory subCategory = new SubCategory
            {
                Name = subCategoryVM.Name,
                CategoryId = subCategoryVM.CategoryId,
            };

            await _unitOfWork.GetRepository<SubCategory>().AddAsync(subCategory);
            await _unitOfWork.SaveAsync();
        }

      



      
    }
}
