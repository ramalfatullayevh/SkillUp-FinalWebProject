using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }


        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
