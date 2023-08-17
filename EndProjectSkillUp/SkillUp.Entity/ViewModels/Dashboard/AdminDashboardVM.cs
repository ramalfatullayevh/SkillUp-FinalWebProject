using SkillUp.Entity.Entities;
using SkillUp.Entity.Entities.Settings;

namespace SkillUp.Entity.ViewModels
{
    public class AdminDashboardVM
    {
        public AppUser AppUser { get; set; }

        public ICollection<ContactUs> Contacts { get; set; }
    }
}
