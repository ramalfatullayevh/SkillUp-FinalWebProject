using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class AppUserInstructor:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
