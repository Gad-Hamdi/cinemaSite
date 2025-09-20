using Microsoft.EntityFrameworkCore;

namespace cinemaSite.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(MovieId))]
    public class FavMovie
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int MovieId { get; set; }
        public Movie movie { get; set; }
            }
}
