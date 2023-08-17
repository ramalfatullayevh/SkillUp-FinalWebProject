using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.CourseExtraProperities
{
    public class Lecture : BaseNameableEntity
    {

        public string Duration { get; set; }

        public string VideoUrl { get; set; }

        public bool? IsWatched { get; set; }


        public int ParagraphId { get; set; }

        public Paragraph Paragraph { get; set; }
    }
}
