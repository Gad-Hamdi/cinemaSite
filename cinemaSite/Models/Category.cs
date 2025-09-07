using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace cinemaSite.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Category name must contain only letters.")]
        public string Name { get; set; }= string.Empty;
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}