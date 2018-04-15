namespace MovieManager.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Contracts.ViewModels;
    using Helpers;
    using Models;

    public class CommonDataViewModel : ICommonDataViewModel
    {
        private Movie _commonDataSelectedMovie;
        private List<Movie> _commonDataMovies;
        private List<Movie> _commonDataPossibleMatches;
        private Movie _commonDataSelectedPossibleMatch;
        private string _commonDataSearchTerm;

        public Movie CommonDataSelectedMovie
        {
            get => _commonDataSelectedMovie;
            set { PropertyChanged.ChangeAndNotify(ref _commonDataSelectedMovie, value, () => CommonDataSelectedMovie); }
        }

        public List<Movie> CommonDataMovies
        {
            get => _commonDataMovies ?? new List<Movie>();
            set { PropertyChanged.ChangeAndNotify(ref _commonDataMovies, value, () => CommonDataMovies); }
        }

        public List<Movie> CommonDataPossibleMatches
        {
            get => _commonDataPossibleMatches;
            set { PropertyChanged.ChangeAndNotify(ref _commonDataPossibleMatches, value, () => CommonDataPossibleMatches); }
        }

        public Movie CommonDataSelectedPossibleMatch
        {
            get => _commonDataSelectedPossibleMatch;
            set { PropertyChanged.ChangeAndNotify(ref _commonDataSelectedPossibleMatch, value, () => CommonDataSelectedPossibleMatch); }
        }

        public string CommonDataSearchTerm
        {
            get => _commonDataSearchTerm;
            set { PropertyChanged.ChangeAndNotify(ref _commonDataSearchTerm, value, () => CommonDataSearchTerm); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
