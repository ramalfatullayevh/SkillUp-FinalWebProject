using SkillUp.Core.Entities;

namespace SkillUp.Entity.Entities.Relations.ManyToMany
{
    public class AppUserProduct : BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        public int ProductId { get; set; }

        public Product Product { get; set; }


        public bool IsBuyed { get; set; }
    }
}
