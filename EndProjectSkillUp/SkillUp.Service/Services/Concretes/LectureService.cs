using Microsoft.AspNetCore.Hosting;
using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Helpers;
using SkillUp.Service.Services.Abstractions;


namespace SkillUp.Service.Services.Concretes
{
    public class LectureService : ILectureService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IWebHostEnvironment _env;

        public LectureService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }


        //Get All Lecture
        public async Task<ICollection<Lecture>> GetAllLectureAsync()
        {
            var lectures = await _unitOfWork.GetRepository<Lecture>().GetAllAsync(); 
            return lectures;
        }


        //Get Lecture By Id
        public async Task<Lecture> GetLectureById(int id)
        {
            var lecture =  await _unitOfWork.GetRepository<Lecture>().GetAsync(l => l.Id == id, l => l.Paragraph);
            return lecture;
        }


        //Create Lecture
        public async Task CreateLectureAsync(CreateLectureVM lectureVM, int id)
        {
            Lecture lecture = new Lecture
            {
                Name = lectureVM.Name,
                ParagraphId = id,
                IsWatched = false,
                VideoUrl = lectureVM.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "coursevideo")),
            };
            lecture.Duration = FileExtension.VideoDuration(Path.Combine(_env.WebRootPath, "user", "assets", "coursevideo", lecture.VideoUrl)).ConvertTime();
            await _unitOfWork.GetRepository<Lecture>().AddAsync(lecture);
            await _unitOfWork.SaveAsync();
        }
    

        //Delete Lecture
        public async Task DeleteLectureAsync(int id)
        {
            await _unitOfWork.GetRepository<Lecture>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }


        //Update Lecture By Id
        public async Task<UpdateLectureVM> UpdateLectureById(int id)
        {
            var lecture = _unitOfWork.GetRepository<Lecture>().GetByIdAsync(id);
            UpdateLectureVM lectureVM = new UpdateLectureVM
            {
                Name = lecture.Name,
                VideoUrl = lecture.VideoUrl
            };
            return lectureVM;
        }


        //Update Lecture
        public async Task<bool> UpdateLectureAsync(int id ,UpdateLectureVM lectureVM)
        {
            var lecture = _unitOfWork.GetRepository<Lecture>().GetByIdAsync(id);
            lecture.Name = lectureVM.Name;
            lecture.VideoUrl.DeleteFile(_env.WebRootPath, "user/assets/coursevideo");
            lecture.VideoUrl = lectureVM.Video.SaveFile(Path.Combine(_env.WebRootPath, "user", "assets", "coursevideo"));
            lecture.Duration = FileExtension.VideoDuration(Path.Combine(_env.WebRootPath, "user", "assets", "coursevideo", lecture.VideoUrl)).ConvertTime();
            await _unitOfWork.GetRepository<Lecture>().UpdateAsync(lecture);
            await _unitOfWork.SaveAsync();
            return true;    
        }

    }
}
