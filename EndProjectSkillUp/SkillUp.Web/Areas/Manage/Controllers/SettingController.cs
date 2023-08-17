using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillUp.DAL.Context;
using SkillUp.Entity.Entities.Settings;

namespace SkillUp.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class SettingController : Controller
    {
        readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }


        //Website Settings Get
        public async Task<IActionResult> WebsiteSettings()
        {
            var infos = _context.HomeInfos.ToDictionary(h => h.Key, h => h.Value);
            return View(infos); 
        }


        //Website Settings Post
        [HttpPost]
        public async Task<IActionResult> WebsiteSettings(Dictionary<string, string> homeInfos)
        {
            foreach (var updKeyValue in homeInfos)
            {
                var existingInfo = _context.HomeInfos.FirstOrDefault(h => h.Key == updKeyValue.Key);
                if (existingInfo != null)
                {
                    existingInfo.Value = updKeyValue.Value;
                }
                else
                {
                    _context.HomeInfos.Add(new Home { Key = updKeyValue.Key, Value = updKeyValue.Value });
                }
            }
            await _context.SaveChangesAsync();

            return View();
        }


        //Contact Settings Get
        public async Task<IActionResult> ContactSettings()
        {
            var contacts = _context.ContactInfos.ToDictionary(h => h.Key, h => h.Value);
            return View(contacts);
        }


        //Contact settings Post
        [HttpPost]
        public async Task<IActionResult> ContactSettings(Dictionary<string, string> contactInfos)
        {
            if (!ModelState.IsValid) return View();
            foreach (var updKeyValue in contactInfos)
            {
                var existingInfo = _context.ContactInfos.FirstOrDefault(h => h.Key == updKeyValue.Key);
                if (existingInfo != null)
                {
                    existingInfo.Value = updKeyValue.Value;
                }
                else
                {
                    _context.ContactInfos.Add(new ContactInfo { Key = updKeyValue.Key, Value = updKeyValue.Value });
                }
            }
            await _context.SaveChangesAsync();
            return View();
        }


        //About settings Get
        public async Task<IActionResult> AboutSettings()
        {
            var aboutinfos = _context.Abouts.ToDictionary(a => a.Key, a => a.Value);
            return View(aboutinfos);    
        }


        //About Settings Post
        [HttpPost]
        public async Task<IActionResult> AboutSettings(Dictionary<string, string> aboutInfos)
        {
            if (!ModelState.IsValid) return View();
            foreach (var updKeyValue in aboutInfos)
            {
                var existingInfo = _context.Abouts.FirstOrDefault(h => h.Key == updKeyValue.Key);
                if (existingInfo != null)
                {
                    existingInfo.Value = updKeyValue.Value;
                }
                else
                {
                    _context.Abouts.Add(new AboutUs { Key = updKeyValue.Key, Value = updKeyValue.Value });
                }
            }
            await _context.SaveChangesAsync();
            return View();
        }
    }

 }
