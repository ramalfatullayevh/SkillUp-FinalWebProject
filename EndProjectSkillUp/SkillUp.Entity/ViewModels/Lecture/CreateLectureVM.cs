using Microsoft.AspNetCore.Http;

namespace SkillUp.Entity.ViewModels
{
    public class CreateLectureVM
    {
        public string Name { get; set; }

        public bool IsWatched { get; set; }

        public IFormFile Video { get; set; }

    }
}
