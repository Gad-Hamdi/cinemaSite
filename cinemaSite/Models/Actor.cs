﻿namespace CinemaTask.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string News { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

        // Helper property for full name
        public string FullName => $"{FirstName} {LastName}";
    }
}