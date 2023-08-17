using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.ManyToMany;

namespace SkillUp.Entity.Entities.Relations.InstructorExtraProperities
{
    public class Profession : BaseNameableEntity
    {

        public ICollection<InstructorProfession> InstructorProfessions { get; set; }
    }
}
