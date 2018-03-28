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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
