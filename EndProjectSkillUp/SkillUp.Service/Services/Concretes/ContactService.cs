using SkillUp.DAL.UnitOfWorks;
using SkillUp.Entity.Entities.Settings;
using SkillUp.Entity.ViewModels;
using SkillUp.Service.Services.Abstractions;

namespace SkillUp.Service.Services.Concretes
{
    public class ContactService : IContactService
    {
        readonly IUnitOfWork _unitOfWork;


        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Get All Messages
        public async Task<ICollection<ContactUs>> GetAllMessageAsync()
        {
            var contacts = await _unitOfWork.GetRepository<ContactUs>().GetAllAsync();
            return contacts;
        }


        //Create Message
        public async Task CreateContactAsync(CreateContactVM contactVM)
        {
            ContactUs contact = new ContactUs
            {
                Name = contactVM.Name,
                Surname = contactVM.Surname,
                Email = contactVM.Email,
                PhoneNumber = contactVM.PhoneNumber,
                Message = contactVM.Message,
                IsRead = false,
                CreateDate = DateTime.Now
            };
            await _unitOfWork.GetRepository<ContactUs>().AddAsync(contact);
            await _unitOfWork.SaveAsync();  
        }


        //Delete Message
        public async Task DeleteMessageAsync(int id)
        {
            await _unitOfWork.GetRepository<ContactUs>().DeleteAsync(id);  
            await _unitOfWork.SaveAsync();  
        }


        //Read Message
        public async Task<bool> ReadMessageAsync(int id)
        {
            var message =  _unitOfWork.GetRepository<ContactUs>().GetByIdAsync(id);
            message.IsRead = true;
            await _unitOfWork.GetRepository<ContactUs>().UpdateAsync(message);
            await _unitOfWork.SaveAsync();
            return true; 
        }
    }
}
