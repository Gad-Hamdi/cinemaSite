using System.ComponentModel.DataAnnotations;

namespace cinemaSite.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Cinema name must contain only letters and spaces.")]
        public string? Name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Description { get; set; } = string.Empty;
        [Required]
        
        public string CinemaLogo { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Address { get; set; } = string.Empty;
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}