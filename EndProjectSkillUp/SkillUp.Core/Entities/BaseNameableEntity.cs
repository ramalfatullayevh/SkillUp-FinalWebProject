using System.ComponentModel.DataAnnotations;

namespace SkillUp.Core.Entities
{
    public class BaseNameableEntity:BaseEntity
    {
        //BaseName
        [Required, MinLength(1), MaxLength(150)]
        public string Name { get; set; }
    }
}
