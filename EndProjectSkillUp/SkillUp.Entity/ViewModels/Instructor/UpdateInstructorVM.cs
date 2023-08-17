using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.ViewModels
{
    public class UpdateInstructorVM
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Description { get; set; }

        public string FacebookUrl { get; set; }

        public string LinkedInUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }


        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }


        public IFormFile? Preview { get; set; }

        public string? PreviewUrl { get; set; }
    }
}
