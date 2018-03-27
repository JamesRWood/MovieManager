namespace MovieManager.Contracts.ViewModels
{
    using System.Collections.ObjectModel;
    using Models;

    public interface IFindMovieDetailsViewModel : IViewModel
    {
        Movie SelectedMovie { get; set; }

        Movie SelectedMatchedMovie { get; set; }

        ObservableCollection<Movie> PossibleMatches { get; set; }
    }
}
