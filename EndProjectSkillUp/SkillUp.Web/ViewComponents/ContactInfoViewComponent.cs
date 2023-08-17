using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;

namespace SkillUp.Web.ViewComponents
{
    public class ContactInfoViewComponent:ViewComponent
    {
        readonly AppDbContext _context;

        public ContactInfoViewComponent(AppDbContext context)
        {
            _context = context;
        }


        //ContactUs Setting
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.ContactInfos.ToDictionary(s => s.Key, s => s.Value));
        }
    }
}
