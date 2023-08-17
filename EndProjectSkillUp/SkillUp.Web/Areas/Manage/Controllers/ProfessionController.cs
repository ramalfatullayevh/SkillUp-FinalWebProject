using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUp.Entity.Entities.Relations.ManyToMany;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;
using SkillUp.Service.Services.Concretes;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin, SuperAdmin")]

    public class ProfessionController : Controller
    {
        readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        //All Profession
        public async Task<IActionResult> InstructorProfession()
        {
            var professions = await _professionService.GetAllProfessionAsync();
            return View(professions);
        }

        
        //Add Profesiion Get
        public async Task<IActionResult> AddNewProfession()
        {
            return View();  
        }

        
        //Add Profession Post
        public async Task<IActionResult> AddNewProfession(CreateProfessionVM professionVM)
        {
            if (!ModelState.IsValid) return View(professionVM);
            if (professionVM is null) return NotFound();
            await _professionService.CreateProfessionAsync(professionVM);
            return RedirectToAction(nameof(InstructorProfession));
        }


        //Delete Profession
        public async Task<IActionResult> DeleteProfession(int id)
        {
            await _professionService.DeleteProfessionAsync(id);
            return RedirectToAction(nameof(InstructorProfession));
        }


        //Updsate Profession Get
        public async Task<IActionResult> UpdateProfession(int id)
        {
            var profession = await _professionService.UpdateProfessionById(id);
            return View(profession);
        }


        //Update Profession Post
        [HttpPost]
        public async Task<IActionResult> UpdateProfession(int id ,UpdateProfessionVM professionVM)
        {
            await _professionService.UpdateProfessionAsync(id,professionVM);
            return RedirectToAction(nameof(InstructorProfession));
        }
    }
}
