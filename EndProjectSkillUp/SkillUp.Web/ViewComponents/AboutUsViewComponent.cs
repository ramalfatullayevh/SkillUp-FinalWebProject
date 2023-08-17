using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;

namespace SkillUp.Web.ViewComponents
{
	public class AboutUsViewComponent:ViewComponent
	{
        readonly AppDbContext _context;

        public AboutUsViewComponent(AppDbContext context)
        {
            _context = context;
        }


        //AboutUs Setting
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.Abouts.ToDictionary(s => s.Key, s => s.Value));
        }
    }
}
