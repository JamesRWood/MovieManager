namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IMovieController
    {
        //IList<Movie> GetAllLocalMovies();

        IList<Movie> GetMovieDataFromLocalLibraryFile();

        IList<Movie> SearchForMoviesByTitle(string title);

        Movie GetMovieById(int id);

        void StoreMovieData(IList<Movie> movies);
    }
}
