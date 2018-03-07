namespace MovieManager.Contracts
{
    using System.Collections.Generic;
    using MovieManager.Models;

    public interface ICommonDataViewModel
    {
        List<string> MovieFileTypes { get; }

        List<Movie> LocalMovieFiles { get; set; }
    }
}
