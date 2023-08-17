using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.Entities.Relations.CourseExtraProperities
{
    public class Category : BaseNameableEntity
    {

        [Required, MinLength(5), MaxLength(450)]
        public string Description { get; set; }


        [Required, MinLength(3), MaxLength(30)]
        public string IconUrl { get; set; }


        public ICollection<SubCategory> SubCategories { get; set; }

        public ICollection<CourseCategory> CourseCategories { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
