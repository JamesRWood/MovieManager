namespace MovieManager.cs.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IMovieController
    {
        ICollection<Movie> GetAllMovies();
    }
}
