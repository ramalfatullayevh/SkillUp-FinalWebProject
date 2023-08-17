using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;

namespace SkillUp.Web.ViewComponents
{
	public class HomeInfoViewComponent:ViewComponent
	{
		readonly AppDbContext _context;

		public HomeInfoViewComponent(AppDbContext context)
		{
			_context = context;
		}

		//Home Setting
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(_context.HomeInfos.ToDictionary(h=>h.Key, h =>h.Value));	
		}
	}
}
