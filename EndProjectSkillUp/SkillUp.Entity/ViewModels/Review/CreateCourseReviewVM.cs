using SkillUp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillUp.Entity.ViewModels
{
    public class CreateCourseReviewVM
    {
        public string ReviewContent { get; set; }

        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }
}
