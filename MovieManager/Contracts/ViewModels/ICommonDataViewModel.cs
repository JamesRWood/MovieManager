namespace MovieManager.Contracts.ViewModels
{
    using System.Collections.Generic;
    using Models;

    public interface ICommonDataViewModel : IViewModel
    {
        List<string> MovieFileTypes { get; }

        List<Movie> CommonDataMovies { get; set; }
    }
}
