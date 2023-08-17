using Microsoft.AspNetCore.Http;

namespace SkillUp.Entity.ViewModels
{
    public class UpdateCourseVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CourseOverview { get; set; }

        public string Requirement { get; set; }

        public string Certification { get; set; }

        public List<int>? CategoryIds { get; set; }
        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public string? ImageUrl { get; set; }

        public string? PreviewUrl { get; set; }


        public IFormFile? Image { get; set; }
        public IFormFile? Preview { get; set; }
    }
}
