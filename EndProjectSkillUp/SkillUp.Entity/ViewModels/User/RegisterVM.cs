using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(15)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Surname { get; set; }

        [Required, MaxLength(25)]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IFormFile? Image { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
