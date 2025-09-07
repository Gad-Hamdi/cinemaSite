using cinemaSite.Models;

namespace cinemaSite.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task CreateRangeAsync(List<Movie> movie);
    }
}
