namespace MovieManager.Contracts.Controllers
{
    using System.Threading.Tasks;
    using Models;

    public interface IApiController
    {
        Task<Movie> EnrichMovieMatchedByTitle(Movie movie);
    }
}
