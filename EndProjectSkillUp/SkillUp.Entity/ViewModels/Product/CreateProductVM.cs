using Microsoft.AspNetCore.Http;

namespace SkillUp.Entity.ViewModels
{
    public class CreateProductVM
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public List<int> CategoryIds { get; set; }


        public IFormFile  Image { get; set; }
    }
}
