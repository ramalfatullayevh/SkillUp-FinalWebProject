using SkillUp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.Entities.Reviews
{
    public class CourseReview:BaseEntity
    {
        [Required, MinLength(5), MaxLength(500)]
        public string ReviewContent { get; set; }

        public DateTime ReviewDate { get; set; }

        public bool Status { get; set; }


        public string AppUserId { get; set; }

        public AppUser? AppUser { get; set; }


        public int CourseId { get; set; }

        public Course? Course { get; set; }


    }
}
