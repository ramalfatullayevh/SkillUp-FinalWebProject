using Microsoft.AspNetCore.Identity;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.Entities
{
    public class Instructor:IdentityUser
    {
        [Required, MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        public string Surname { get; set; }

        [Required, MinLength(10), MaxLength(450)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string PreviewVideoUrl { get; set; }

        public string? FaceBookUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public string? LinkedInUrl { get; set; }

        public byte Experince { get; set; }

        public double Wallet { get; set; }


        public ICollection<Course> Courses { get; set; }

        public ICollection<Product>? Products { get; set; }

        public ICollection<InstructorProfession> InstructorProfessions { get; set; }

        public ICollection<AppUserInstructor> AppUserInstructors { get; set; }





    }
}
