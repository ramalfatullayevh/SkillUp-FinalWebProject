﻿using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class AppUserCourse : BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        public int CourseId { get; set; }

        public Course Course { get; set; }


        public bool IsSold { get; set; } 
    }
}
