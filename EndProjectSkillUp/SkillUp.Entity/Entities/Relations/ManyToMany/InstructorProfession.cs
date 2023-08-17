using SkillUp.Core.Entities;
using SkillUp.Entity.Entities.Relations.InstructorExtraProperities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class InstructorProfession : BaseEntity
    {
        public string InstructorId { get; set; }

        public Instructor Instructor { get; set; }


        public int ProfessionId { get; set; }

        public Profession Profession { get; set; }
    }
}
