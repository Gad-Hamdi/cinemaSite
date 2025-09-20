using Microsoft.EntityFrameworkCore;

namespace cinemaSite.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(MovieId))]
    public class Cart
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int MovieId { get; set; }
        public Movie movie { get; set; }

        public int Count { get; set; }
    }
}
