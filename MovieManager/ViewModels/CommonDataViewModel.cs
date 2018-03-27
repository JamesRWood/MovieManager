namespace MovieManager.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Contracts.ViewModels;
    using Helpers;
    using Models;
    using MovieManager.Contracts;

    public class CommonDataViewModel : ICommonDataViewModel
    {
        private List<Movie> _commonDataMovies;

        public List<string> MovieFileTypes => new List<string> {".mp4", ".avi"};

        public List<Movie> CommonDataMovies
        {
            get => _commonDataMovies ?? new List<Movie>();
            set { PropertyChanged.ChangeAndNotify(ref _commonDataMovies, value, () => CommonDataMovies); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
