namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IApiController
    {
        Task<Movie> EnrichMovieMatchedByTitle(Movie movie);

        Task<List<Movie>> GetPossibleMatchesFromApi(string title);
    }
}
