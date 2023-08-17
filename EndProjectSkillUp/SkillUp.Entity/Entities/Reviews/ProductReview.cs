using SkillUp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.Entities.Reviews
{
    public class ProductReview:BaseEntity
    {
        [Required, MinLength(5), MaxLength(500)]
        public string ReviewContent { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public bool Status { get; set; }


        public string AppUserId { get; set; }

        public AppUser Appuser { get; set; }


        public int ProductId { get; set; }

        public Product Product { get; set; }


    }
}
