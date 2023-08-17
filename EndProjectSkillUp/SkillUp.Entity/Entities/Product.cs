using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.Entities.Reviews;

namespace SkillUp.Entity.Entities
{
    public class Product:BaseNameableEntity
    {
        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; } = true;



        public string InstructorId { get; set; }

        public Instructor Instructor { get; set; }


        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public ICollection<AppUserProduct>?AppUserProducts { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }

    }
}
