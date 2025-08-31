namespace CinemaTask.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int MovieStatus { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public List<string> MultiImageUrl { get; set; } = new List<string>();

        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}