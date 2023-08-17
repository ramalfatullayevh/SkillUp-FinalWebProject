using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.Entities.Relations.InstructorExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class ProfessionService : IProfessionService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly AppDbContext _context;

        public ProfessionService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        //Get All Profession
        public async Task<ICollection<Profession>> GetAllProfessionAsync()
        {
            var profession = _context.Professions.Include(i => i.InstructorProfessions).ToList();
            return profession;
        }


        //Get Profession By Id
        public async Task<Profession> GetProfessionById(int id)
        {
            var profession =  _unitOfWork.GetRepository<Profession>().GetByIdAsync(id);
            return profession;
        }


        //Create Profession
        public async Task CreateProfessionAsync(CreateProfessionVM professionVM)
        {
            Profession profession = new Profession
            {
                Name = professionVM.Name,
            };

            await _unitOfWork.GetRepository<Profession>().AddAsync(profession);
            await _unitOfWork.SaveAsync();
        }


        //Delete Profession
        public async Task DeleteProfessionAsync(int id)
        {
            await _unitOfWork.GetRepository<Profession>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }


        //Update Profession By Id
        public async Task<UpdateProfessionVM> UpdateProfessionById(int id)
        {
            var profession =  _unitOfWork.GetRepository<Profession>().GetByIdAsync(id);
            UpdateProfessionVM professionVM = new UpdateProfessionVM
            {
                Name = profession.Name,
            };
            return professionVM;
        }


        //Update Profession
        public async Task<bool> UpdateProfessionAsync(int id, UpdateProfessionVM updateProfessionVM)
        {
            var profession = _unitOfWork.GetRepository<Profession>().GetByIdAsync(id);
            profession.Name = updateProfessionVM.Name;
            await _unitOfWork.GetRepository<Profession>().UpdateAsync(profession);
            await _unitOfWork.SaveAsync();
            return true;
        }

    }
}
