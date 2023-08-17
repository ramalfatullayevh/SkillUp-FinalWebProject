using System.ComponentModel.DataAnnotations;

namespace SkillUp.Entity.ViewModels
{
    public class LoginVM
    {
        [Required,MaxLength(25)]
        public string UserNameOrEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}
