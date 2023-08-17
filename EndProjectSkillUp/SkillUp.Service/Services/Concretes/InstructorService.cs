using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class InstructorService : IInstructorService
    {
        readonly AppDbContext _context;


        public InstructorService(AppDbContext context)
        {
            _context = context;
        }


        //GetAll Instructor
        public async Task<ICollection<Instructor>> GetAllInstructorAsync()
        {
            return await _context.Instructors.Include(c=>c.Courses).Include(ip=>ip.InstructorProfessions).ThenInclude(p=>p.Profession).ToListAsync();
        }


        //Get Instructor By Id
        public async Task<Instructor>  GetInstructorById(string id)
        {

            return await _context.Instructors.Include(c => c.Courses).ThenInclude(ap => ap.AppUserCourses).Include(c => c.Courses)
                .ThenInclude(c => c.CourseCategories).ThenInclude(c => c.Category).
                Include(c => c.Courses).ThenInclude(p => p.Paragraphs).ThenInclude(l=>l.Lectures).
                Include(p => p.Products).Include(ia => ia.AppUserInstructors).ThenInclude(a => a.AppUser).Include(p => p.Products).ThenInclude(pc => pc.ProductCategories)
                .ThenInclude(c=>c.Category).Include(p=>p.Products).ThenInclude(ap=>ap.AppUserProducts).Include(cr=>cr.Courses).ThenInclude(c=>c.CourseReviews).ThenInclude(u=>u.AppUser)
                .FirstOrDefaultAsync(i=>i.Id == id);
        }


        //Delete Instructor
        public async Task DeleteInstructorAsync(string id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();  
        }


        //Update Instructor By Id
        public async Task<UpdateInstructorVM> UpdateInstructorById(string id)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(u => u.Id == id);
            UpdateInstructorVM userVm = new UpdateInstructorVM
            {
                Name = instructor.Name,
                Surname = instructor.Surname,
                Email = instructor.Email,
                ImageUrl = instructor.ImageUrl,
                PreviewUrl = instructor.PreviewVideoUrl,
                LinkedInUrl = instructor.LinkedInUrl,
                FacebookUrl = instructor.FaceBookUrl,
                Description = instructor.Description,   
                InstagramUrl = instructor.InstagramUrl,
                TwitterUrl = instructor.TwitterUrl,
            };
            return userVm;
        }


        //Update Instructor
        public async Task<bool> UpdateInstructorAsync(string id, UpdateInstructorVM updateInstructorVM)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(u => u.Id == id);
            instructor.Name = updateInstructorVM.Name;
            instructor.Surname = updateInstructorVM.Surname;
            instructor.Email = updateInstructorVM.Email;
            instructor.Description = updateInstructorVM.Description;    
            instructor.TwitterUrl = updateInstructorVM.TwitterUrl;
            instructor.FaceBookUrl = updateInstructorVM.FacebookUrl;
            instructor.InstagramUrl = updateInstructorVM.InstagramUrl;
            instructor.LinkedInUrl = updateInstructorVM.LinkedInUrl;

            _context.Instructors.Update(instructor);
             _context.SaveChanges();
            return true;
        }

    }
}
