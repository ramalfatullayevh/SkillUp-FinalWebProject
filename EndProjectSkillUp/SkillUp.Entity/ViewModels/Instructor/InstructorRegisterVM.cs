using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.ViewModels
{
    public class InstructorRegisterVM
    {
        [Required, MaxLength(15)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Surname { get; set; }

        [Required, MinLength(50), MaxLength(300)]
        public string Description { get; set; }

        [Required, MaxLength(25)]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? FacebookUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? LinkedInUrl { get; set; }

        [Required]
        public byte Experience { get; set; }
        public List<int> ProfessionIds { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public IFormFile Preview { get; set; }

    }
}
