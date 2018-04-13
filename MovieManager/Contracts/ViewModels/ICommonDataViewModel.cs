namespace MovieManager.Contracts.ViewModels
{
    using System.Collections.Generic;
    using Models;

    public interface ICommonDataViewModel : IViewModel
    {
        Movie CommonDataSelectedMovie { get; set; }

        List<Movie> CommonDataMovies { get; set; }

        List<Movie> CommonDataPossibleMatches { get; set; }
    }
}
