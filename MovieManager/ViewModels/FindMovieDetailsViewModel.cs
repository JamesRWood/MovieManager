namespace MovieManager.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Contracts.ViewModels;
    using Helpers;
    using Models;

    public class FindMovieDetailsViewModel : IFindMovieDetailsViewModel
    {
        private Movie _selectedMovie;
        private Movie _selectedMatchedMovie;
        private ObservableCollection<Movie> _possibleMatches;

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { PropertyChanged.ChangeAndNotify(ref _selectedMovie, value, () => SelectedMovie); }
        }

        public Movie SelectedMatchedMovie
        {
            get => _selectedMatchedMovie;
            set { PropertyChanged.ChangeAndNotify(ref _selectedMatchedMovie, value, () => SelectedMatchedMovie); }
        }

        public ObservableCollection<Movie> PossibleMatches
        {
            get => _possibleMatches;
            set { PropertyChanged.ChangeAndNotify(ref _possibleMatches, value, () => PossibleMatches); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
