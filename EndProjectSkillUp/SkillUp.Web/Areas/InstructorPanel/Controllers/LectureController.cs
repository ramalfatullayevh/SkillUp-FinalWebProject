using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Web.Areas.InstructorPanel.Controllers
{
    [Area("InstructorPanel")]
    public class LectureController : Controller
    {
        readonly IParagraphService _paragraph;
        readonly ICourseService _courseService;
        readonly ILectureService _lectureService;
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public LectureController(IParagraphService paragraph, ICourseService course, ILectureService lectureService, AppDbContext context, IWebHostEnvironment env)
        {
            _paragraph = paragraph;
            _courseService = course;
            _lectureService = lectureService;
            _context = context;
            _env = env;
        }


        //All Paragraphs
        public  IActionResult Paragraphs(int id)
        {
            var paragraphs =  _context.Courses.Include(p=>p.Paragraphs).ThenInclude(l=>l.Lectures).FirstOrDefault(x=>x.Id == id);   
            return View(paragraphs);    
        }


        //Add New Paragraph Get
        public async Task<IActionResult> AddNewCourseParagraph(int id)
        {
            return View();
        }


        //Add New Paragraph Post
        [HttpPost]
        public async Task<IActionResult> AddNewCourseParagraph(int id, CreateParagraphVM paragraphVM)
        {
            if (!ModelState.IsValid) return View();
            await _paragraph.CreateParagraphAsync(paragraphVM,id);
            return RedirectToAction(nameof(AddNewCourseParagraph));
        }


        //Delete Paragraph
        public async Task<IActionResult> DeleteParagraph(int id)
        {
           await _paragraph.DeleteParagraphAsync(id);
            return RedirectToAction("MyCourses", "Course");
        }


        //Update Paragraph Get
        public async Task<IActionResult> UpdateParagraph(int id)
        {
            var paragraph = await _paragraph.UpdateParagraphById(id); 
            return View(paragraph);
        }


        //Update Paragraph Post
        [HttpPost]
        public async Task<IActionResult> UpdateParagraph(int id, UpdateParagraphVM paragraphVM)
        {
           await _paragraph.UpdateParagraphAsync(id, paragraphVM);
            return RedirectToAction("MyCourses", "Course");
        }


        //-------------------------------------Lecture----------------------------------------//


        //All Lectures
        public async Task<IActionResult> Lectures(int id)
        {
            var lectures = await _context.Paragraphs.Include(l=>l.Lectures).FirstOrDefaultAsync(x=>x.Id == id);
            return View(lectures);
        }


        //Add New Lecture Get
        public async Task<IActionResult> AddNewCourseLectures(int id)
        {
            return View();
        }


        //Add New Lecture Post
        [HttpPost]
        public async Task<IActionResult> AddNewCourseLectures(int id, CreateLectureVM lectureVM)
        {
            if (lectureVM.Video != null)
            {
                string result = lectureVM.Video.CheckValidate("video/", 50000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Video", result);
                }


            }

            await _lectureService.CreateLectureAsync(lectureVM,id);
            return RedirectToAction("MyCourses" , "Course");
        }


        //Delete Lecture
        public async Task<IActionResult> DeleteLecture(int id)
        {
            await _lectureService.DeleteLectureAsync(id);
            return RedirectToAction("MyCourses", "Course");
        }
        

        //Update Lecture Get
        public async Task<IActionResult> UpdateLecture(int id)
        {
            var lecture = await _lectureService.UpdateLectureById(id);
            return View(lecture);
        }


        //Update Lecture Post
        [HttpPost]
        public async Task<IActionResult> UpdateLecture(int id, UpdateLectureVM lectureVM)
        {
            Lecture lecture = await _lectureService.GetLectureById(id);
            if (lectureVM.Video != null)
            {
                string result = lectureVM.Video.CheckValidate("video/", 50000);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Preview", result);
                }
            }
            await _lectureService.UpdateLectureAsync(id, lectureVM);
            return RedirectToAction("MyCourses", "Course");
        }

    }
}
