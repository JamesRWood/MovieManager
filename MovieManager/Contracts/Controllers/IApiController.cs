namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IApiController
    {
        //List<Movie> EnrichMoviesMatchedByTitle(List<Movie> movies);

        Movie EnrichMovieMatchedByTitle(Movie movie);
    }
}
