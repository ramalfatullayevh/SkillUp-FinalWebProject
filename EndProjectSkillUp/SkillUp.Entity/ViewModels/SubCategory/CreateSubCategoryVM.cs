using SkillUp.Entity.Entities.Relations.CourseExtraProperities;

namespace SkillUp.Entity.ViewModels
{
    public class CreateSubCategoryVM
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
