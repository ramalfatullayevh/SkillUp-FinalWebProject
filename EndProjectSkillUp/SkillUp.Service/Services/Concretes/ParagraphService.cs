using Microsoft.EntityFrameworkCore;
using SkillUp.DAL.Context;
using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Relations.CourseExtraProperities;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
	public class ParagraphService : IParagraphService
	{
		readonly IUnitOfWork _unitOfWork;
		readonly AppDbContext appDbContext;

		public ParagraphService(IUnitOfWork unitOfWork, AppDbContext appDbContext)
		{
			_unitOfWork = unitOfWork;
			this.appDbContext = appDbContext;
		}


		//Get All Paragraph
		public async Task<ICollection<Paragraph>> GetAllParagraphAsync()
		{
			var paragraph = appDbContext.Paragraphs.Include(x=>x.Lectures).ToList();
			return paragraph;

		}


		//Get Paragraph By Id
		public Task<Paragraph> GetParagraphById(int id)
		{
			var paragraph = _unitOfWork.GetRepository<Paragraph>().GetAsync(p => p.CourseId == id);
			return paragraph;
		}


		//Create Paragraph
		public async Task CreateParagraphAsync(CreateParagraphVM paragraphVM,int id)
		{
			Paragraph paragraph = new Paragraph
			{
				Name = paragraphVM.Name,
				CourseId = id
			};
			 await _unitOfWork.GetRepository<Paragraph>().AddAsync(paragraph);
			await _unitOfWork.SaveAsync();
		}


		//Delete Paragraph
		public async Task DeleteParagraphAsync(int id)
		{
			await _unitOfWork.GetRepository<Paragraph>().DeleteAsync(id);
			await _unitOfWork.SaveAsync();
		}


		//Update Paragraph By Id
		public async Task<UpdateParagraphVM> UpdateParagraphById(int id)
		{
			var paragraph =  _unitOfWork.GetRepository<Paragraph>().GetByIdAsync(id);
			UpdateParagraphVM paragraphVM = new UpdateParagraphVM
			{
				Name = paragraph.Name,
			};
			return paragraphVM;
		}


		//Update Paragraph
		public async Task<bool> UpdateParagraphAsync(int id ,UpdateParagraphVM paragraphVM)
		{
			var paragraph = _unitOfWork.GetRepository<Paragraph>().GetByIdAsync(id);
			paragraph.Name = paragraphVM.Name;	
			await _unitOfWork.GetRepository<Paragraph>().UpdateAsync(paragraph);
			await _unitOfWork.SaveAsync();
			return true;
		}

	}
}
