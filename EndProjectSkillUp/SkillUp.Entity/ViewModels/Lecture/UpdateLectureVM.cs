using Microsoft.AspNetCore.Http;

namespace SkillUp.Entity.ViewModels
{
    public class UpdateLectureVM
    {
        public string Name { get; set; }

        public string VideoUrl { get; set; }

        public string Duration { get; set; }


        public IFormFile Video { get; set; }
    }
}
