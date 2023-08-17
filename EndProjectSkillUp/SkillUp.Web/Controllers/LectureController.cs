using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Controllers
{
    public class LectureController : Controller
    {
        readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }


        //Lecture
        public async Task<IActionResult> Index(int id)
        {
            var lecture = await _lectureService.GetLectureById(id);
            lecture.IsWatched = true;
            return View(lecture);
        }
    }
}
