using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.Entities.Reviews;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.Entities
{
    public class Course:BaseNameableEntity
    {
        [Required, MinLength(5), MaxLength(450)]
        public string Description { get; set; }

        [Required, MinLength(5), MaxLength(450)]
        public string CourseOverview { get; set; }

        [Required, MinLength(5), MaxLength(450)]
        public string Requirement { get; set; }

        [Required, MinLength(5), MaxLength(450)]
        public string Certification { get; set; }

        [Required]
        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public int ViewCount { get; set; }

        public bool IsActive { get; set; }


        public string? ImageUrl { get; set; }

        public string? PreviewUrl { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;


        public string InstructorId { get; set; }

        public Instructor? Instructor { get; set; }


        public ICollection<Paragraph>? Paragraphs { get; set; }

        public ICollection<CourseCategory>? CourseCategories { get; set; }

        public ICollection<AppUserCourse>? AppUserCourses { get; set; }

        public ICollection<CourseReview> CourseReviews { get; set; }



    }
}
