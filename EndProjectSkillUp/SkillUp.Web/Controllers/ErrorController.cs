using Microsoft.AspNetCore.Mvc;

namespace SkillUp.Web.Controllers
{
    public class ErrorController : Controller
    {
        //Error View
        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
