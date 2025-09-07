using System.ComponentModel.DataAnnotations;

namespace cinemaSite.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters.")]

        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters.")]

        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Bio { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string News { get; set; } = string.Empty;
        [Required]
        public string ProfilePicture { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

        // Helper property for full name
        public string FullName => $"{FirstName} {LastName}";
    }
}