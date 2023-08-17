using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class InstructorController : Controller
    {
        readonly IInstructorService _instructorService;


        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }


        //All Instructors
        public async Task<IActionResult> ManageInstructor(string? query , int page = 1)
        {
            if (query!=null)
            {
                var instructor = await _instructorService.GetAllInstructorAsync();
                var search = instructor.Where(c => c.UserName.ToLower().Trim().Contains(query.ToLower().Trim())).ToList();
                IEnumerable<Instructor> paginationsearch = search.Skip((page - 1) * 4).Take(4);
                PaginationVM<Instructor> searchpaginationVM = new PaginationVM<Instructor>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)search.Count / 4),
                    CurrentPage = page,
                    Items = paginationsearch,
                    Query = query

                };
                return View(searchpaginationVM);

            }
            else
            {
                var instructors = await _instructorService.GetAllInstructorAsync();
                IEnumerable<Instructor> pagination = instructors.Skip((page - 1) * 4).Take(4);
                PaginationVM<Instructor> paginationVM = new PaginationVM<Instructor>
                {
                    MaxPageCount = (int)Math.Ceiling((decimal)instructors.Count / 4),
                    CurrentPage = page,
                    Items = pagination
                };

                return View(paginationVM);
            }
           
        }


        //Delete Instructor
        public async Task<IActionResult> DeleteInstructor(string id)
        {
            await _instructorService.DeleteInstructorAsync(id);
            return RedirectToAction(nameof(ManageInstructor));
        }

      
    }
}
