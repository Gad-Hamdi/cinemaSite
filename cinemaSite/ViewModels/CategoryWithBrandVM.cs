using Microsoft.AspNetCore.Mvc.Rendering;

namespace cinemaSite.ViewModels
{
    public class CategoryWithBrandVM
    {
        public List<Category> Categories { get; set; } = null!;
        public List<SelectListItem> Brands { get; set; } = null!;
        public Product? Product { get; set; }
    }
}
