using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.CourseExtraProperities
{
    public class SubCategory:BaseNameableEntity
    {

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
