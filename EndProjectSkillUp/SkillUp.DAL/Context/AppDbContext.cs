using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.Entities.Relations.InstructorExtraProperities;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.Entities.Reviews;
using SkillUp.Entity.Entities.Settings;

namespace SkillUp.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        //Main Properities
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Product> Products { get; set; }


        //Extra Relations
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Profession> Professions { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        //Contact
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ContactUs> ContactsUs { get; set; }


        //Page Info
        public DbSet<Home> HomeInfos { get; set; }
        public DbSet<AboutUs> Abouts{ get; set; }
        public DbSet<Faq> Faqs{ get; set; }
        public DbSet<ContactInfo> ContactInfos{ get; set; }


        //ManyToMany Relations
        public DbSet<AppUserCourse> AppUserCourses { get; set; }
        public DbSet<AppUserProduct> AppUserProducts { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<InstructorProfession> InstructorProfessions { get; set; }
        public DbSet<AppUserInstructor> AppUserInstructors { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }



    }
}
