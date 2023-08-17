using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.CourseExtraProperities
{
    public class Paragraph : BaseNameableEntity
    {

        public int CourseId { get; set; }

        public Course Course { get; set; }


        public ICollection<Lecture> Lectures { get; set; }

    }
}
