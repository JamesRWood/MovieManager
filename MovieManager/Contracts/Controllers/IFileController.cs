namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IFileController
    {
        IList<Movie> FindLocalMovieFiles();
    }
}
