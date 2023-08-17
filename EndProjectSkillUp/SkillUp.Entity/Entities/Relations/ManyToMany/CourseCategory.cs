using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class CourseCategory : BaseEntity
    {
        public int CourseId { get; set; }

        public Course Course { get; set; }


        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
